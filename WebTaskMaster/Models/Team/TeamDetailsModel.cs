using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamDetailsModel
{
    public Guid TeamId { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Leader")]
    public TeamMemberModel Leader { get; set; }

    [Display(Name = "Members")]
    public ICollection<TeamMemberModel> Members { get; set; }

    public bool CanEditTeam { get; set; }

    public bool CanChangeLeader => Members.Count > 1;

    public bool CanRemoveMembers => Members.Count > 1;
}