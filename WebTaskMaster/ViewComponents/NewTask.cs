using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;

namespace WebTaskMaster.ViewComponents
{
    public class NewTask : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            NewTaskModel model = new NewTaskModel();
            return View(model);
        }
    }
}
