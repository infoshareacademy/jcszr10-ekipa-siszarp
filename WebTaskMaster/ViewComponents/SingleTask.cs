using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Task;
namespace WebTaskMaster.ViewComponents
{
    public class SingleTask : ViewComponent
    {
        private readonly ITaskService _taskService;
        private readonly ITeamService _teamService;
        public SingleTask(ITaskService taskService, ITeamService teamService)
        {
            _taskService = taskService;
            _teamService = teamService;
        }
        public async Task<IViewComponentResult> InvokeAsync(Guid taskGuid, Guid teamGuid)
       {                  
            var taskModel = new WebTaskModel();
            taskModel.ProjectTask = await _taskService.GetTaskByGuid(taskGuid);
            taskModel.Team = await _teamService.GetTeamById(teamGuid);
            return View(taskModel);
       }
    }
}
