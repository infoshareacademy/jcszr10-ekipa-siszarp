using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;

namespace WebTaskMaster.ViewComponents
{
    public class TasksRow : ViewComponent
    {     
        private readonly ITasksListService _tasksListService;
        public TasksRow(ITasksListService tasksListService)
        {
            _tasksListService = tasksListService;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(TasksList list, int statusId)
        {
            TasksList tasksList = _tasksListService.GetListByStatus(list, statusId);
            return View(tasksList);
        }
    }
}
