using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
	public interface IProjectService
	{
		Task<List<Project>> GetAllProjects();
		Task<Project?> GetProjectById(Guid id);
        Task<Guid> CreateProject(string name, string description , Guid ownerId);
        Task RemoveProject(Guid id);
        Task EditNameAndDescription(Guid projectId, string newName, string newDescription);
        Task ChangeProjectTeam(Guid projectId, Guid newTeamId);
        //Task DeleteTeamFromProject(Guid projectId, Guid teamIdToDelete);
    }
}