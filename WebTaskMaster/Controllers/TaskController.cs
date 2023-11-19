using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Model;
using Microsoft.CodeAnalysis;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class TaskController : Controller
    {
        private readonly ITasksListService _tasksListService;
        private readonly ITaskService _taskService;
        
        
        public TaskController(ITasksListService tasksListService, ITaskService taskService)
        {
            _tasksListService = tasksListService;
            _taskService = taskService;
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
