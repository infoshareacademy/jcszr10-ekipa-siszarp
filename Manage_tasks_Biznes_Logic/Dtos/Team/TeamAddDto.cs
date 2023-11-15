namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamAddDto
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid LeaderId { get; set; }
}