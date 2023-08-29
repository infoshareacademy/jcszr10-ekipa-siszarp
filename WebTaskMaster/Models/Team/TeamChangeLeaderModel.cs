using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team
{
    public class TeamChangeLeaderModel
    {
        [Required(ErrorMessage = "Select new leader.")]
        public Guid NewLeaderId { get; set; }

        [Display(Name = "Available leaders")]
        public List<TeamMemberModel>? AvailableLeaders { get; set; }
    }
}
