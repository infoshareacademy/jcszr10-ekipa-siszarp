using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamAddMembersModel
{
    [Required]
    public Guid TeamId { get; set; }

    [Required(ErrorMessage = "Select at least one member.")]
    [MinLength(1, ErrorMessage = "Select at least one member.")]
    public List<Guid> NewMemberIds { get; set; } = new();

    [Display(Name = "Available members")]
    public ICollection<TeamMemberModel>? AvailableMembers { get; set; }
}