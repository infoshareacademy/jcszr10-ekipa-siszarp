using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;
namespace WebTaskMaster.ViewComponents
{
    public class SingleTask : ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync(Guid taskGuid)
       {
            var taskService = new TaskService();           
            var taskModel = new WebTaskModel();
            taskModel.ProjectTask = taskService.GetTaskByGuid(taskGuid);
            return View(taskModel);
       }
    }
}
