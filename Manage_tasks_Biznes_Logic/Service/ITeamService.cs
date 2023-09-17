using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface ITeamService
{
    Team? GetTeamById(Guid id);
    Guid CreateTeam(string name, string description);
    void DeleteTeam(Guid id);
    List<Team> GetAllTeams();
    void AddMembersToTeam(Guid teamId, IEnumerable<Guid> newMembersIds);
    void DeleteMemberFromTeam(Guid teamId, Guid memberIdToDelete);
    void EditNameAndDescription(Guid teamId, string newName, string newDescription);
    void ChangeTeamLeader(Guid teamId, Guid newLeaderId);
}