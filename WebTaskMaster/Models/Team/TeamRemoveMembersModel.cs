using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamRemoveMembersModel
{
    [Required]
    public Guid TeamId { get; set; }

    [Required(ErrorMessage = "Select at least one member.")]
    [MinLength(1, ErrorMessage = "Select at least one member.")]
    public List<Guid> RemoveMemberIds { get; set; } = new();

    [Display(Name = "Available remove members")]
    public ICollection<TeamMemberModel>? AvailableRemoveMembers { get; set; }
}