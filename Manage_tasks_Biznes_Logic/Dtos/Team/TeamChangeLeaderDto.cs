namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamChangeLeaderDto
{
    public Guid TeamId { get; set; }

    public Guid EditorId { get; set; }

    public Guid NewLeaderId { get; set; }
}