﻿using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.CodeAnalysis;

using Microsoft.AspNetCore.Authorization;
using Manage_tasks_Biznes_Logic.Model;
using WebTaskMaster.Extensions;
using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Task;

namespace WebTaskMaster.Controllers
{
    [Authorize(Roles = "User")]
    public class TaskController : Controller
    {
        private readonly ITasksListService _tasksListService;
        private readonly ITaskService _taskService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public TaskController(ITasksListService tasksListService, ITaskService taskService, IProjectService projectService, IMapper mapper)
        {
            _tasksListService = tasksListService;
            _taskService = taskService;
            _projectService = projectService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> MyTasksList()
        {
            HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId);

            var dto = await _taskService.GetTasksByUserId(userId);

            var model = _mapper
                .Map<List<TaskBasicForUserDto>, List<MyTasksListModel>>(dto);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewTasksList(NewTasksListModel model)
        {
            await _projectService.AddListToProject(model.TasksListName, model.ProjectId);

            return Redirect(model.url);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTasksList(Guid listId, string url)
        {
            await _tasksListService.DeleteTasksList(listId);

            return Redirect(url);
        }
        [HttpPost]
        public async Task<IActionResult> EditTasksListName(Guid listId, string newTasksListName, string url)
        {
            await _tasksListService.EditTasksListName(listId, newTasksListName);
            return Redirect(url);
        }



        [HttpPost]
        public async Task<IActionResult> MoveTasks(MoveTaskModel model)
        {
            if (model.TasksIds is null)
            {

            }
            else
            {
                List<String> taskIds = model.TasksIds.Split(",").ToList();
                List<Guid> ids = taskIds.Select(task => Guid.Parse(task)).ToList();
                await _tasksListService.MoveMultipleTasks(ids, model.DestinationId);
            }

            return Redirect(model.url);
        }



        [HttpPost]
        public async Task<IActionResult> CreateNewTask(NewTaskModel model)
        {
            await _tasksListService.AddNewTask(model.Name, model.Description, model.TasksListId);

            return Redirect(model.url);
        }
        [HttpPost]
        public async Task<IActionResult> EditTask(WebTaskModel model)
        {

            model.newValues[1] = HttpContext.Request.Form["Description"];
            await _tasksListService.EditTask(model.newValues, model.ProjectTask);

            return Redirect(model.url);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTask(WebTaskModel model)
        {
            await _tasksListService.DeleteTask(model.ProjectTask.Id);

            return Redirect(model.url);
        }
    }
}
