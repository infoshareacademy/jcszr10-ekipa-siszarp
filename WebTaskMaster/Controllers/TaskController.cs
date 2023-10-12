using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Controllers
{
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
        public IActionResult DeleteTask(WebTaskModel model)
        {
            var task = _taskService.GetTaskByGuid(model.ProjectTask.Id);
            _taskService.RemoveTask(task);
            return RedirectToAction("Index");
        }
    }
}
