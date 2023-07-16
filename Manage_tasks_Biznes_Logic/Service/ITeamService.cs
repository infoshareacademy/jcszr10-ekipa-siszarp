using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface ITeamService
{
    void SaveNewTeam(Team newTeam);

    void UpdateTeam(Team team);

    Team? GetTeamById(int id);

    Team? GetTeamByMember(User member);
}
