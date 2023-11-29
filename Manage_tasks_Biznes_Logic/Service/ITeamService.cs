using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface ITeamService
{
    Task<Team?> GetTeamById(Guid id);

    Task<List<Team>> GetAllTeams();

    Task<TeamListForUserDto> GetTeamListForUser(Guid userId);

    Task<List<Guid>>GetAllTeamIdUserPartOfAsync(Guid userIdGuid);
	Task AddTeam(TeamAddDto dto);

    Task DeleteTeam(Guid teamId, Guid editorId);

    Task<TeamDetailsForUserDto> GetTeamDetailsForUser(Guid teamId, Guid userId);

    Task<TeamBasicDto> GetTeamBasic(Guid teamId);

    Task EditTeam(TeamNameEditDto dto);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamLeaders(Guid teamId);

    Task ChangeTeamLeader(TeamChangeLeaderDto dto);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamMembers(Guid teamId);

    Task AddTeamMembers(TeamAddMembersDto dto);

    Task<ICollection<TeamMemberDto>> GetAvailableTeamRemoveMembers(Guid teamId);

    Task RemoveTeamMembers(TeamRemoveMembersDto dto);
}