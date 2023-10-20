using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamBasicDto
{
    public Guid TeamId { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Leader")]
    public TeamMemberDto? Leader { get; set; }

    [Display(Name = "Number of members")]
    public int NumberOfMembers { get; set; }
}