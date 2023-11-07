namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamListForUserDto
{
    public ICollection<TeamBasicDto> TeamsLeader { get; set; }

    public ICollection<TeamBasicDto> TeamsMember { get; set; }
}