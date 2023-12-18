namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamMemberDto
{
    public Guid MemberId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Position { get; set; }
}