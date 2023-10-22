using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;

namespace WebTaskMaster.ViewComponents
{
    public class NewTask : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid tasksListId)
        {
            NewTaskModel model = new NewTaskModel();
            model.TasksListId = tasksListId;
            return View(model);
        }
    }
}
