using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Manage_tasks_Biznes_Logic.Service;

public class TeamService
{
    private const string TeamsFileName = "Teams.json";

    public Team? GetTeamById(Guid id)
    {
        return GetAllTeams().Where(team => team.Id == id).FirstOrDefault();
    }

    public void UpdateTeam(Team team)
    {
        List<Team> teams = GetAllTeams();

        var userInDatabase = teams.Where(t => t.Id == team.Id).FirstOrDefault();

        if (userInDatabase is not null)
        {
            teams.Remove(userInDatabase);
        }

        teams.Add(team);

        SaveTeams(teams);
    }

    public void DeleteTeam(Team team)
    {
        List<Team> teams = GetAllTeams();

        var teamInDatabase = teams.Where(t => t.Id == team.Id).FirstOrDefault();

        if (teamInDatabase is null)
        {
            return;
        }

        teams.Remove(teamInDatabase);

        SaveTeams(teams);
    }

    public List<Team> GetAllTeams()
    {
        List<TeamAsJsonObject>? teamsObjects = null;

        if (File.Exists(TeamsFileName))
        {
            teamsObjects = JsonSerializer.Deserialize<List<TeamAsJsonObject>>(File.ReadAllText(TeamsFileName));
        }

        if (teamsObjects is null)
        {
            return new List<Team>();
        }

        var teams = new List<Team>();

        foreach (var teamObject in teamsObjects)
        {
            teams.Add(FromJsonObject(teamObject));
        }

        return teams;
    }

    private void SaveTeams(List<Team> teams)
    {
        var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        var teamsObjects = new List<TeamAsJsonObject>();

        foreach (var team in teams)
        {
            teamsObjects.Add(ToJsonObject(team));
        }

        File.WriteAllText(TeamsFileName, JsonSerializer.Serialize(teamsObjects, serializerOptions));
    }

    private TeamAsJsonObject ToJsonObject(Team team)
    {
        var membersIds = team.GetMembers().Select(member => member.Id).ToArray();

        return new TeamAsJsonObject(team.Id, team.Name, team.Description, team.Leader.Id, membersIds);
    }

    private Team FromJsonObject(TeamAsJsonObject teamObject)
    {
        var allUsers = Data.Data.UserService.GetAllUsers();

        var leader = allUsers.Where(user => user.Id == teamObject.LeaderId).FirstOrDefault();

        if (leader is null)
        {
            throw new Exception();
        }

        var members = allUsers.Join(teamObject.MembersIds, user => user.Id, memberId => memberId, (user, memberId) => user);

        if (!members.Any())
        {
            throw new Exception();
        }

        var team = new Team(teamObject.Name, teamObject.Description, leader, members)
        {
            Id = teamObject.ID
        };

        return team;
    }

    private record TeamAsJsonObject(Guid ID, string Name, string Description, Guid LeaderId, Guid[] MembersIds);
}
