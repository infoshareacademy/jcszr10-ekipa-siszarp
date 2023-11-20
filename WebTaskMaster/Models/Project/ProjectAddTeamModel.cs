using Manage_tasks_Biznes_Logic.Service;
using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project
{
    public class ProjectAddTeamModel
    {
        [Required(ErrorMessage = "Select assigned team.")]
        public Guid TeamIdToAdd { get; set; }

        [Display(Name = "Available teams")]
        public List<ProjectTeamModel>? AvailableTeams { get; set; }
    }
}
