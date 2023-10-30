using Manage_tasks_Biznes_Logic.Service;
using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project
{
    public class ProjectAddTeamModel
    {
        [Required(ErrorMessage = "Select assigned team.")]
        public List<Guid> TeamsIdsToAdd { get; set; }

        [Display(Name = "Available teams")]
        public List<ProjectTeamModel>? AvailableTeams { get; set; }
    }
}
