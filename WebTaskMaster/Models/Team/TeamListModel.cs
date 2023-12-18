using Manage_tasks_Biznes_Logic.Dtos.Team;

namespace WebTaskMaster.Models.Team;

public class TeamListModel
{
    public ICollection<TeamBasicModel> TeamsLeader { get; set; }

    public ICollection<TeamBasicModel> TeamsMember { get; set; }
}