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
    public TeamMemberModel? Leader { get; set; }

    [Display(Name = "Members")]
    public ICollection<TeamMemberModel> Members { get; set; }

    public bool CanRemoveLeader => Leader is not null;

    public bool CanChangeLeader => (Leader is null && Members.Count > 0) ||
                                   (Leader is not null && Members.Count > 1);

    public bool CanRemoveMembers => (Leader is not null && Members.Count > 1) ||
                                    (Leader is null && Members.Count > 0);
}