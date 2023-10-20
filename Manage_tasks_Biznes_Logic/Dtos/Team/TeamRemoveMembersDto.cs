namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamRemoveMembersDto
{
    public Guid TeamId { get; set; }

    public ICollection<Guid> RemoveMemberIds { get; set; }
}