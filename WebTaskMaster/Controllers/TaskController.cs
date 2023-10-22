using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Model;
using Microsoft.CodeAnalysis;

namespace WebTaskMaster.Controllers
{
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
            string url = Url.Action("Details", "Project", new { projectId = model.ProjectId });
            return Redirect(url);
        }       
        [HttpPost]
        public async Task<IActionResult> EditTask(WebTaskModel model)
        {           
            await _tasksListService.EditTask(model.newValues, model.ProjectTask);
            string url = Url.Action("Details", "Project", new { projectId = model.ProjectId });
            return Redirect(url);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTask(WebTaskModel model)
        {
            await _tasksListService.DeleteTask(model.ProjectTask.Id);
            string url = Url.Action("Details", "Project", new { projectId = model.ProjectId });
            return Redirect(url);
        }
    }
}
