using Manage_tasks_Biznes_Logic.Service;
using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project
{
    public class ProjectChangeTeamModel
    {
        [Required(ErrorMessage = "Select assigned team.")]
        public Guid NewTeamId { get; set; }

        [Display(Name = "Available teams")]
        public List<ProjectTeamModel>? AvailableTeams { get; set; }
    }
}
