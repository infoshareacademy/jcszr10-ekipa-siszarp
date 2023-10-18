using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic;
using WebTaskMaster.Models.Task;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITasksListService _tasksListService;
        
        
        public TaskController(ITasksListService tasksListService)
        {
            _tasksListService = tasksListService;
        }

       

        
       

        [HttpPost]
        public async Task<IActionResult> CreateNewTask(NewTaskModel model)
        {
            await _tasksListService.AddNewTask(model.Name, model.Description, model.TasksListId);
            return RedirectToAction("Index");
        }       
        [HttpPost]
        public async Task<IActionResult> EditTask(WebTaskModel model)
        {           
            await _tasksListService.EditTask(model.newValues, model.ProjectTask);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteTask(WebTaskModel model)
        {
           
            return RedirectToAction("Index");
        }
    }
}
