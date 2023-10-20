namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamAddMembersDto
{
    public Guid TeamId { get; set; }

    public ICollection<Guid> NewMemberIds { get; init; }
}