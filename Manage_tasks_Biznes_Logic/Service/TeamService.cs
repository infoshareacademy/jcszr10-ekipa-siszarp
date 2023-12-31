﻿using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
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
    // Функция которая обращаеться к teamuser , вытаскивает все тимы где я учасник и возвращает id этих команд , потом я ищу проекты с этой командой и возвращаю их .


    public async Task<TeamListForUserDto> GetTeamListForUser(Guid userId)
    {
        var teamsQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .Where(t => t.Members.Any(m => m.Id == userId));

        var userTeams = await _mapper.ProjectTo<TeamBasicDto>(teamsQuery).ToListAsync();

        var teamsLeader = userTeams.Where(t => t.Leader.MemberId == userId).ToList();
        var teamsMember = userTeams.Except(teamsLeader).ToList();

        var dto = new TeamListForUserDto
        {
            TeamsLeader = teamsLeader,
            TeamsMember = teamsMember
        };

        return dto;
    }

    public async Task<List<Guid>> GetAllTeamIdUserPartOfAsync(Guid userId)
    {
        var UserPartOfteamListId = await _dbContext.TeamUserEntities
            .Where(a => a.UserId == userId).Select(a => a.TeamId).ToListAsync();


        return UserPartOfteamListId;

    }
    public async Task AddTeam(TeamAddDto dto)
    {
        var team = _mapper.Map<TeamAddDto, TeamEntity>(dto);

        var leader = await _dbContext.UserEntities.FindAsync(dto.LeaderId);

        if (leader is null)
        {
            throw new InvalidOperationException("Leader do not exist.");
        }

        team.Leader = leader;
        team.Members.Add(leader);

        await _dbContext.TeamEntities.AddAsync(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTeam(Guid teamId, Guid editorId)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        if (team.Leader.Id != editorId)
        {
            throw new UnauthorizedAccessException("Only the team leader can delete a team.");
        }

        _dbContext.TeamEntities.Remove(team);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<TeamDetailsForUserDto> GetTeamDetailsForUser(Guid teamId, Guid userId)
    {
        var teamQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .Where(t => t.Id == teamId)
            .Where(t => t.Members.Any(m => m.Id == userId));

        var team = await _mapper.ProjectTo<TeamDetailsForUserDto>(teamQuery).FirstOrDefaultAsync();

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist, or the user is not a member of the team.");
        }

        team.CanEditTeam = team.Leader.MemberId == userId;

        return team;
    }

    public async Task<TeamBasicDto> GetTeamBasic(Guid teamId)
    {
        var teamQuery = _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members);

        var team = await _mapper.ProjectTo<TeamBasicDto>(teamQuery)
            .FirstOrDefaultAsync(t => t.TeamId == teamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        return team;
    }

    public async Task EditTeam(TeamNameEditDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .FirstOrDefaultAsync(t => t.Id == dto.TeamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        if (team.Leader.Id != dto.EditorId)
        {
            throw new UnauthorizedAccessException("Only the team leader can edit team data.");
        }

        _mapper.Map(dto, team);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamLeaders(Guid teamId)
    {
        var availableUsersQuery = _dbContext.UserEntities
            .Where(u => u.Teams.Any(t => t.Id == teamId))
            .Where(u => u.TeamsLeader.All(t => t.Id != teamId));

        var dto = await _mapper.ProjectTo<TeamMemberDto>(availableUsersQuery).ToListAsync();

        return dto;
    }

    public async Task ChangeTeamLeader(TeamChangeLeaderDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstOrDefaultAsync(t => t.Id == dto.TeamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        if (team.Leader.Id != dto.EditorId)
        {
            throw new UnauthorizedAccessException("Only the team leader can change the team leader.");
        }

        if (team.Leader.Id == dto.NewLeaderId)
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

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamMembers(Guid teamId)
    {
        var availableUsersQuery = _dbContext.UserEntities
            .Where(u => u.Teams.All(t => t.Id != teamId));

        var dto = await _mapper.ProjectTo<TeamMemberDto>(availableUsersQuery).ToListAsync();

        return dto;
    }

    public async Task AddTeamMembers(TeamAddMembersDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Members)
            .Include(t => t.Leader)
            .FirstOrDefaultAsync(t => t.Id == dto.TeamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        if (team.Leader.Id != dto.EditorId)
        {
            throw new UnauthorizedAccessException("Only the team leader can add members to the team.");
        }

        if (team.Members.Select(m => m.Id).Intersect(dto.NewMemberIds).Any())
        {
            throw new InvalidOperationException("Some new members are already in the team.");
        }

        var users = await _dbContext.UserEntities
            .Where(u => dto.NewMemberIds.Any(nmId => nmId == u.Id))
            .ToListAsync();

        if (users.Count < dto.NewMemberIds.Count)
        {
            throw new InvalidOperationException("Some members do not exist.");
        }

        team.Members.AddRange(users);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<TeamMemberDto>> GetAvailableTeamRemoveMembers(Guid teamId)
    {
        var availableUsersQuery = _dbContext.UserEntities
            .Where(u => u.Teams.Any(t => t.Id == teamId))
            .Where(u => u.TeamsLeader.All(t => t.Id != teamId));

        var dto = await _mapper.ProjectTo<TeamMemberDto>(availableUsersQuery).ToListAsync();

        return dto;
    }

    public async Task RemoveTeamMembers(TeamRemoveMembersDto dto)
    {
        var team = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members
                .Where(u => dto.RemoveMemberIds.Any(id => id == u.Id)))
            .FirstOrDefaultAsync(t => t.Id == dto.TeamId);

        if (team is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        if (team.Leader.Id != dto.EditorId
            && dto.RemoveMemberIds.Count == 1
            && dto.RemoveMemberIds.First() != dto.EditorId)
        {
            throw new UnauthorizedAccessException("Member can remove only themselves. Leader can remove other members.");
        }

        if (dto.RemoveMemberIds.Contains(team.Leader.Id))
        {
            throw new InvalidOperationException("One of the members to be removed is the leader.");
        }

        if (team.Members.IntersectBy(dto.RemoveMemberIds, m => m.Id).Count() < dto.RemoveMemberIds.Count)
        {
            throw new InvalidOperationException("Some members are not part of the team.");
        }

        var membersNotDeleted = team.Members.ExceptBy(dto.RemoveMemberIds, m => m.Id);
        team.Members = membersNotDeleted.ToList();

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
