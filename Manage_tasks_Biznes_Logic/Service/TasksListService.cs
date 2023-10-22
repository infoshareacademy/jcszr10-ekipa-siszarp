﻿using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Manage_tasks_Biznes_Logic.Service
{
    public interface ITasksListService
    {
        TasksList GetListByStatus(TasksList list, int statusId);
        Task<TasksList> GetTasksListByGuid(Guid tasksListId);
        Task AddNewTask(string newTaskName, string newTaskDescription, Guid tasksListId);
        Task EditTask(string[] newValues, ProjectTask taskToEdit);
        Task CreateTasksList(string tasksListName, Guid projectId);
        TasksList EntityToModel(TaskListEntity entity);
        Task DeleteTask(Guid taskId);
    }
    public class TasksListService : ITasksListService
    {
        private readonly ITaskService _taskService;
        private readonly DataBaseContext _dbContext;
        public TasksListService(ITaskService taskService, DataBaseContext context)
        {
            _taskService = taskService;
            _dbContext = context;
        }
        public TasksList GetListByStatus(TasksList list, int statusId)
        {
            TasksList tasksList = new TasksList();

            tasksList.Tasks = list.Tasks == null? null : list.Tasks.Where(t => t.Status.StatusID() == statusId).ToList();

            return tasksList;
        }

        public async Task<TasksList> GetTasksListByGuid(Guid tasksListId)
        {
            var dbTasksList = await _dbContext.TaskListEntities
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(t => t.Id == tasksListId);

            if (dbTasksList == null)
            {
                return null;
            }
            TasksList tasksList = new TasksList();
            tasksList.TasksListName = dbTasksList.Name;
            return tasksList;
        }
        public async Task CreateTask(ProjectTask task, Guid tasksListId)
        {            
            var newTask = new TaskEntity()
            {
                Name = task.TaskName,
                Description = task.TaskDescription,
                TaskListId = tasksListId
            };
            await _dbContext.AddAsync(newTask);
            await _dbContext.SaveChangesAsync();                        
        }
        public async Task AddNewTask(string newTaskName, string newTaskDescription, Guid tasksListId)
        {
            await CreateTask(_taskService.CreateNewTask(newTaskName, newTaskDescription), tasksListId);
        }
        public async Task EditTask(string[] newValues, ProjectTask taskToEdit)
        {
            var task = _taskService.EditTask(newValues, taskToEdit);
            var newTaskDetails = await _dbContext.TaskEntities.FindAsync(task.Id);

            if (newTaskDetails is null)
            {
                return;
            }
            var statusId = task.Status.StatusID();
            newTaskDetails.Name = task.TaskName;
            newTaskDetails.Description = task.TaskDescription;
            newTaskDetails.StatusId = (Manage_tasks_Database.TaskStatus)statusId;
            newTaskDetails.FinishDate = task.FinishDate;

            await _dbContext.SaveChangesAsync();

        }
        public async Task CreateTasksList(string tasksListName, Guid projectId)
        {
            var newTasksList = new TaskListEntity()
            {
                Name = tasksListName,
                ProjectId = projectId,
                Id = Guid.NewGuid(),
            };
            await _dbContext.AddAsync(newTasksList);
            await _dbContext.SaveChangesAsync();
        }
        public TasksList EntityToModel(TaskListEntity entity) 
        {
            TasksList list = new TasksList()
            {
                TasksListName = entity.Name,
                Id = entity.Id,

                Tasks = entity.Tasks == null? null : entity.Tasks.Select(_taskService.EntityToModel).ToList(),
            };
            return list;
        }
        public async Task DeleteTask(Guid taskId)
        {
            var task = await _dbContext.TaskEntities.FindAsync(taskId);
            if (task is null)
            {
                return;
            }
            _dbContext.TaskEntities.Remove(task);
            await _dbContext.SaveChangesAsync();
        }
         
    }
}