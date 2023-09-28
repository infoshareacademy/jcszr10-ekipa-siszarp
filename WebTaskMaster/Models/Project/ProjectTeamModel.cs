using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project
{
    public class ProjectTeamModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Team Name")]
        public string TeamName { get; set; }
    }
}

