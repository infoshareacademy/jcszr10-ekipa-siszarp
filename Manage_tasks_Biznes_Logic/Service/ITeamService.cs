using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface ITeamService
{
    Task<Team?> GetTeamById(Guid id);

    Task<List<Team>> GetAllTeams();

    Task<ICollection<TeamBasicDto>> GetTeamList();

    Task AddTeam(TeamAddDto dto);

    Task DeleteTeam(Guid teamId);

    Task<TeamDetailsDto> GetTeamDetails(Guid teamId);

    Task<TeamBasicDto> GetTeamBasic(Guid teamId);

    Task EditTeam(TeamNameEditDto dto);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamLeaders(Guid teamId);

    Task ChangeTeamLeader(TeamChangeLeaderDto dto);

    Task RemoveLeader(Guid teamId);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamMembers(Guid teamId);

    Task AddTeamMembers(TeamAddMembersDto dto);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamRemoveMembers(Guid teamId);

    Task RemoveTeamMembers(TeamRemoveMembersDto dto);
}