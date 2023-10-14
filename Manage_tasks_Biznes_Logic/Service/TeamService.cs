using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Biznes_Logic.Service;

public class TeamService : ITeamService
{
    private readonly DataBaseContext _dbContext;

    public TeamService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Team?> GetTeamById(Guid id)
    {
        var teamEntity = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (teamEntity == null)
        {
            return null;
        }

        var team = ConvertTeamEntity(teamEntity);

        return team;
    }

    public async Task<Guid> CreateTeam(string name, string description)
    {
        var team = new TeamEntity
        {
            Name = name,
            Description = description
        };

        await _dbContext.TeamEntities.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        return team.Id;
    }

    public async Task DeleteTeam(Guid id)
    {
        var team = await _dbContext.TeamEntities.FindAsync(id);

        if (team is null)
        {
            return;
        }

        _dbContext.TeamEntities.Remove(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Team>> GetAllTeams()
    {
        var teamEntities = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .ToListAsync();

        var teams = teamEntities.Select(ConvertTeamEntity).ToList();

        return teams;
    }

    public async Task AddMembersToTeam(Guid teamId, IEnumerable<Guid> newMembersIds)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        if (team is null)
        {
            return;
        }

        var possibleMembers = newMembersIds.Except(team.Members.Select(u => u.Id));

        var userEntities = await _dbContext.UserEntities.ToListAsync();

        var usersToAdd = userEntities.IntersectBy(possibleMembers, u => u.Id).ToList();

        foreach (var user in usersToAdd)
        {
            team.Members.Add(user);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMemberFromTeam(Guid teamId, Guid memberIdToDelete)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .Include(t => t.Leader)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        if (team is null)
        {
            return;
        }

        var memberToRemove = team.Members.FirstOrDefault(u => u.Id == memberIdToDelete);

        if (memberToRemove is null)
        {
            return;
        }

        team.Members.Remove(memberToRemove);

        if (team.LeaderId == memberToRemove.Id)
        {
            team.LeaderId = null;
            team.Leader = null;
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task EditNameAndDescription(Guid teamId, string newName, string newDescription)
    {
        var team = await _dbContext.TeamEntities.FirstOrDefaultAsync(t => t.Id == teamId);

        if (team is null)
        {
            return;
        }

        team.Name = newName;
        team.Description = newDescription;

        await _dbContext.SaveChangesAsync();
    }

    public async Task ChangeTeamLeader(Guid teamId, Guid newLeaderId)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .Include(t => t.Leader)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        if (team is null || team.LeaderId == newLeaderId)
        {
            return;
        }

        var newLeader = await _dbContext.UserEntities.FindAsync(newLeaderId);

        if (newLeader is null)
        {
            return;
        }

        team.LeaderId = newLeader.Id;
        team.Leader = newLeader;

        if (!team.Members.Contains(newLeader))
        {
            team.Members.Add(newLeader);
        }

        await _dbContext.SaveChangesAsync();
    }

    private static Team ConvertTeamEntity(TeamEntity teamEntity)
    {
        var members = teamEntity.Members
            .Select(u => new User
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Position = u.Position ?? string.Empty
            }).ToList();

        var team = new Team
        {
            Id = teamEntity.Id,
            Name = teamEntity.Name,
            Description = teamEntity.Description ?? string.Empty,
            Members = members
        };

        if (teamEntity.Leader is not null)
        {
            team.Leader = members.Find(u => u.Id == teamEntity.LeaderId);
        }

        return team;
    }
}
