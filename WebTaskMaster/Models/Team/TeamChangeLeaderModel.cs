using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamChangeLeaderModel
{
    [Required]
    public Guid TeamId { get; set; }

    [Required(ErrorMessage = "Select new leader.")]
    public Guid NewLeaderId { get; set; }

    [Display(Name = "Available leaders")]
    public ICollection<TeamMemberModel>? AvailableLeaders { get; set; }
}