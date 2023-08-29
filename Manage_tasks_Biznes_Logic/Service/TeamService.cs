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
        return GetAllTeams().FirstOrDefault(team => team.Id == id);
    }

    public Guid CreateTeam(string name, string description)
    {
        var newTeam = new Team
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description
        };

        UpdateTeam(newTeam);

        return newTeam.Id;
    }

    public void DeleteTeam(Guid id)
    {
        var teams = GetAllTeams();

        var teamInDatabase = teams.FirstOrDefault(t => t.Id == id);

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

        return teamsObjects.Select(FromJsonObject).ToList();
    }

    public void AddMembersToTeam(Guid teamId, IEnumerable<Guid> newMembersIds)
    {
        var teamInDataBase = GetTeamById(teamId);

        if (teamInDataBase is null)
        {
            return;
        }

        var teamAsJsonObj = ToJsonObject(teamInDataBase);

        var membersIdsUnion = teamAsJsonObj.MembersIds.Union(newMembersIds).ToArray();

        teamAsJsonObj = teamAsJsonObj with { MembersIds = membersIdsUnion };

        UpdateTeam(FromJsonObject(teamAsJsonObj));
    }

    public void DeleteMemberFromTeam(Guid teamId, Guid memberIdToDelete)
    {
        var teamInDataBase = GetTeamById(teamId);

        if (teamInDataBase is null)
        {
            return;
        }

        var teamAsJsonObj = ToJsonObject(teamInDataBase);

        var notDeletedMembersIds = teamAsJsonObj.MembersIds.Where(id => id != memberIdToDelete).ToArray();

        teamAsJsonObj = teamAsJsonObj with { MembersIds = notDeletedMembersIds };

        if (teamAsJsonObj.LeaderId == memberIdToDelete)
        {
            teamAsJsonObj = teamAsJsonObj with { LeaderId = null };
        }

        UpdateTeam(FromJsonObject(teamAsJsonObj));
    }

    public void EditNameAndDescription(Guid teamId, string newName, string newDescription)
    {
        var teamInDataBase = GetTeamById(teamId);

        if (teamInDataBase is null)
        {
            return;
        }

        var teamAsJsonObj = ToJsonObject(teamInDataBase);

        teamAsJsonObj = teamAsJsonObj with { Name = newName, Description = newDescription };

        UpdateTeam(FromJsonObject(teamAsJsonObj));
    }

    public void ChangeTeamLeader(Guid teamId, Guid newLeaderId)
    {
        var teamInDataBase = GetTeamById(teamId);

        if (teamInDataBase is null)
        {
            return;
        }

        var teamAsJsonObj = ToJsonObject(teamInDataBase);

        teamAsJsonObj = teamAsJsonObj with { LeaderId = newLeaderId };

        UpdateTeam(FromJsonObject(teamAsJsonObj));
    }

    private void UpdateTeam(Team team)
    {
        var teams = GetAllTeams();

        var teamInDatabase = teams.FirstOrDefault(t => t.Id == team.Id);

        if (teamInDatabase is not null)
        {
            teams.Remove(teamInDatabase);
        }
        else
        {
            team.Id = Guid.NewGuid();
        }

        teams.Add(team);

        SaveTeams(teams);
    }

    private void SaveTeams(List<Team> teams)
    {
        var serializerOptions = new JsonSerializerOptions { WriteIndented = true };

        var teamsObjects = new List<TeamAsJsonObject>();

        foreach (var team in teams)
        {
            teamsObjects.Add(ToJsonObject(team));
        }

        File.WriteAllText(TeamsFileName, JsonSerializer.Serialize(teamsObjects, serializerOptions));
    }

    private TeamAsJsonObject ToJsonObject(Team team)
    {
        var membersIds = team.Members.Count > 0 ? team.Members.Select(member => member.Id).ToArray() : Array.Empty<Guid>();

        return new TeamAsJsonObject(team.Id, team.Name, team.Description, team.Leader?.Id, membersIds);
    }

    private Team FromJsonObject(TeamAsJsonObject teamObject)
    {
        var allUsers = Data.Data.UserService.GetAllUsers();

        var leader = allUsers.FirstOrDefault(user => user.Id == teamObject.LeaderId);

        var members = allUsers.Join(teamObject.MembersIds, user => user.Id, memberId => memberId, (user, memberId) => user).ToList();

        var team = new Team
        {
            Id = teamObject.Id,
            Name = teamObject.Name,
            Description = teamObject.Description,
            Leader = leader,
            Members = members
        };

        return team;
    }

    private record TeamAsJsonObject(Guid Id, string Name, string Description, Guid? LeaderId, Guid[] MembersIds);
}
