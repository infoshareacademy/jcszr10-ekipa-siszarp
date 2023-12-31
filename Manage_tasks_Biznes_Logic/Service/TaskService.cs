﻿
using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Manage_tasks_Biznes_Logic.Dtos.Task;
using AutoMapper;

namespace Manage_tasks_Biznes_Logic.Service
{
    public interface ITaskService
    {
        ProjectTask CreateNewTask(string taskName, string taskDescription);
        Task<ProjectTask?> GetTaskByGuid(Guid taskGuid);
        ProjectTask EditTask(string[] newValues, ProjectTask task);
        ProjectTask EntityToModel(TaskEntity taskEntity);
        Task<List<TaskBasicForUserDto>> GetTasksByUserId(Guid userId);

    }



    public class TaskService : ITaskService
    {
        private readonly DataBaseContext _dbContext;
        private readonly IMapper _mapper;

        public TaskService(DataBaseContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public ProjectTask CreateNewTask(string taskName, string taskDescription)
        {
            return new ProjectTask(taskName, taskDescription);
        }

        public async Task<ProjectTask?> GetTaskByGuid(Guid taskGuid)
        {
            var dbTask = await _dbContext.TaskEntities.FindAsync(taskGuid);
            if (dbTask == null)
            {
                return null;
            }
            return EntityToModel(dbTask);
        }

        public async Task<List<TaskBasicForUserDto>> GetTasksByUserId(Guid userId)
        {
            var query = _dbContext.TaskEntities
                .Where(t => t.TaskListId != null)
                .Include(t => t.TaskList)
                    .ThenInclude(tl => tl!.Project)
                .Where(t => t.AssignedUserId == userId);

            var tasks = await _mapper
                .ProjectTo<TaskBasicForUserDto>(query)
                .ToListAsync();

            return tasks;
        }

        public ProjectTask EditTask(string[] newValues, ProjectTask task)
        {
            TasksList editTaskName = new TasksList(new EditTaskName());
            editTaskName.EditTask(newValues[0], task);
            TasksList editTaskDescription = new TasksList(new EditTaskDescription());
            editTaskDescription.EditTask(newValues[1], task);
            TasksList editTaskStatus = new TasksList(new EditTaskStatus());
            editTaskStatus.EditTask(newValues[2], task);

            if (Int32.Parse(newValues[2]) == 2)
            {
                TasksList editTaskFinishDate = new TasksList(new EditTaskFinishDate());
                editTaskFinishDate.EditTask(newValues[3], task);
            }

            return task;
        }
        public ProjectTask EntityToModel(TaskEntity taskEntity)
        {
            Status status = new Status();
            status.ChangeStatus(((byte)taskEntity.StatusId));
            User user = new User();
            if (taskEntity.AssignedUser != null)
            {
                user.Id = taskEntity.AssignedUser.Id;
                user.FirstName = taskEntity.AssignedUser.FirstName;
                user.LastName = taskEntity.AssignedUser.LastName;
                user.Position = taskEntity.AssignedUser.Position ?? string.Empty;
            }
            else
            {
                user = null;
            }

            ProjectTask task = new ProjectTask()
            {
                TaskName = taskEntity.Name,
                TaskDescription = taskEntity.Description,
                Id = taskEntity.Id,
                FinishDate = taskEntity.FinishDate,
                Status = status,
                AssignedUser = user
            };
            return task;
        }

    }


}
