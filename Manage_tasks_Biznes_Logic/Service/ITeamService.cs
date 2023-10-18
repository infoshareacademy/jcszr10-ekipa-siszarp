using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface ITeamService
{
    Task<Team?> GetTeamById(Guid id);
    Task<Guid> CreateTeam(string name, string description);
    Task DeleteTeam(Guid id);
    Task<List<Team>> GetAllTeams();
    Task AddMembersToTeam(Guid teamId, IEnumerable<Guid> newMembersIds);
    Task DeleteMemberFromTeam(Guid teamId, Guid memberIdToDelete);
    Task EditNameAndDescription(Guid teamId, string newName, string newDescription);
    Task ChangeTeamLeader(Guid teamId, Guid newLeaderId);
}