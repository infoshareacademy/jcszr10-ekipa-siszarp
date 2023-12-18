using System.ComponentModel.DataAnnotations;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Models.Project
{
    public class ProjectTeamModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Team Name")]
        public string Name { get; set; }
        [Display(Name = "Team Leader")]
        public TeamMemberModel? Leader { get; set; }
        public Guid LeaderId { get; set; }
    }
}

