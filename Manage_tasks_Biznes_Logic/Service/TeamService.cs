using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public class TeamService
{
    private readonly List<Team> _teams = new();

    public Team? GetTeamById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Team? GetTeamByMember(User member)
    {
        throw new NotImplementedException();
    }

    public void UpdateTeam(Team team)
    {
        var userInDatabase = _teams.Where(t => t.Id == team.Id).FirstOrDefault();

        if (userInDatabase is null)
        {
            _teams.Add(team);
            return;
        }
    }

    public void DeleteTeam(Team team)
    {
        var userInDatabase = _teams.Where(t => t.Id == team.Id).FirstOrDefault();

        if (userInDatabase is not null)
        {
            _teams.Remove(userInDatabase);
            return;
        }
    }

    public IEnumerable<Team> GetAllTeams()
    {
        return _teams;
    }
}
