using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamBasicDto
{
    public Guid TeamId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public TeamMemberDto Leader { get; set; }

    //public int NumberOfMembers { get; set; }
}