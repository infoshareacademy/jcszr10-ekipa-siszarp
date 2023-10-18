using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;
namespace WebTaskMaster.ViewComponents
{
    public class SingleTask : ViewComponent
    {
        private readonly ITaskService _taskService;
        public SingleTask(ITaskService service)
        {
            _taskService = service;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid taskGuid)
       {
            
                  
            var taskModel = new WebTaskModel();
            taskModel.ProjectTask = await _taskService.GetTaskByGuid(taskGuid);
            return View(taskModel);
       }
    }
}
