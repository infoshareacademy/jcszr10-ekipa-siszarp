using AutoMapper;
using AutoMapper.QueryableExtensions;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Manage_tasks_Biznes_Logic.Service;

public class TeamService : ITeamService
{
    private readonly DataBaseContext _dbContext;
    private readonly IMapper _mapper;

    public TeamService(DataBaseContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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

    public async Task<List<Team>> GetAllTeams()
    {
        var teamEntities = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .ToListAsync();

        var teams = teamEntities.Select(ConvertTeamEntity).ToList();

        return teams;
    }
    public async Task<ICollection<TeamBasicDto>> GetTeamList()
    {
        var teamsQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members);

        var teams = await _mapper.ProjectTo<TeamBasicDto>(teamsQuery).ToArrayAsync();

        return teams;
    }

    public async Task AddTeam(TeamAddDto dto)
    {
        var team = new TeamEntity
        {
            Name = dto.Name,
            Description = dto.Description
        };

        await _dbContext.TeamEntities.AddAsync(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTeam(Guid teamId)
    {
        var team = await _dbContext.TeamEntities.FindAsync(teamId);

        if (team is null)
        {
            return;
        }

        _dbContext.TeamEntities.Remove(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TeamDetailsDto> GetTeamDetails(Guid teamId)
    {
        var teamQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .Where(t => t.Id == teamId);

        var team = await _mapper.ProjectTo<TeamDetailsDto>(teamQuery).FirstAsync();

        return team;
    }

    public async Task<TeamBasicDto> GetTeamBasic(Guid teamId)
    {
        var teamQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .Where(t => t.Id == teamId);

        var team = await _mapper.ProjectTo<TeamBasicDto>(teamQuery).FirstAsync();

        return team;
    }

    public async Task EditTeam(TeamNameEditDto dto)
    {
        var team = await _dbContext.TeamEntities.FirstAsync(t => t.Id == dto.TeamId);

        team.Name = dto.Name;
        team.Description = dto.Description;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamLeaders(Guid teamId)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == teamId);

        var availableMembers = team.Members.AsEnumerable();

        if (team.Leader is not null)
        {
            availableMembers = availableMembers.Where(m => m.Id != team.LeaderId);
        }

        var dto = _mapper.Map<IEnumerable<UserEntity>, ICollection<TeamMemberDto>>(availableMembers);

        return dto;
    }

    public async Task ChangeTeamLeader(TeamChangeLeaderDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == dto.TeamId);

        if (team.Leader is not null && team.Leader.Id == dto.NewLeaderId)
        {
            throw new InvalidOperationException("New leader is the same as the current one.");
        }

        if (team.Members.All(m => m.Id != dto.NewLeaderId))
        {
            throw new InvalidOperationException("New leader is not a member of the team.");
        }

        team.Leader = team.Members.First(m => m.Id == dto.NewLeaderId);

        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveLeader(Guid teamId)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .FirstAsync(t => t.Id == teamId);

        if (team.Leader is null)
        {
            throw new InvalidOperationException("This team has no leader.");
        }

        team.Leader = null;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamMembers(Guid teamId)
    {
        var teamEntity = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == teamId);

        var users = await _dbContext.UserEntities.ToArrayAsync();

        var availableUsers = users.ExceptBy(teamEntity.Members.Select(m => m.Id), u => u.Id);

        var dto = _mapper.Map<IEnumerable<UserEntity>, ICollection<TeamMemberDto>>(availableUsers);

        return dto;
    }

    public async Task AddTeamMembers(TeamAddMembersDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == dto.TeamId);

        if (team.Members.Select(m => m.Id).Intersect(dto.NewMemberIds).Any())
        {
            throw new InvalidOperationException("Some new members are already in the team.");
        }

        var userEntities = await _dbContext.UserEntities.ToArrayAsync();
        var users = userEntities.IntersectBy(dto.NewMemberIds, u => u.Id).ToArray();

        if (users.Length < dto.NewMemberIds.Count)
        {
            throw new InvalidOperationException("Some members do not exist.");
        }

        team.Members.AddRange(users);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamRemoveMembers(Guid teamId)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == teamId);

        var availableRemoveMembers = team.Members.AsEnumerable();

        if (team.Leader is not null)
        {
            availableRemoveMembers = availableRemoveMembers.Where(m => m.Id != team.LeaderId);
        }

        var dto = _mapper.Map<IEnumerable<UserEntity>, ICollection<TeamMemberDto>>(availableRemoveMembers);

        return dto;
    }

    public async Task RemoveTeamMembers(TeamRemoveMembersDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstAsync(t => t.Id == dto.TeamId);

        if (team.Leader is not null && dto.RemoveMemberIds.Contains(team.Leader.Id))
        {
            throw new InvalidOperationException("One of the members to be removed is the leader.");
        }

        var membersToRemove = team.Members.IntersectBy(dto.RemoveMemberIds, m => m.Id);

        foreach (var member in membersToRemove)
        {
            team.Members.Remove(member);
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
