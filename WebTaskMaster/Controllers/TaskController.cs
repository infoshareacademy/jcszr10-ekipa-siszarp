using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Model;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class TaskController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();         
            return View(tasks);
        }

        
        public IActionResult CreateNewTask() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewTask(NewTaskModel model)
        {
            _taskService.AddTask(_taskService.CreateNewTask(model.Name, model.Description));
            return RedirectToAction("Index");
        }       
        [HttpPost]
        public IActionResult EditTask(WebTaskModel model)
        {
            
            _taskService.EditTask(model.newValues, _taskService.GetTaskByGuid(model.ProjectTask.Id));
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteTask(ProjectTask model)
        {
            return RedirectToAction("Index");
        }
    }
}
