using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
	public interface IProjectService
	{
		Task<List<Project>> GetAllProjects();
		Task<Project?> GetProjectById(Guid id);
        Task<Guid> CreateProject(string name, string description);
        Task RemoveProject(Guid id);
        Task EditNameAndDescription(Guid projectId, string newName, string newDescription);
        Task AddTeamToProject(Guid projectId, IEnumerable<Guid> newTeamsIds);
        Task DeleteTeamFromProject(Guid projectId, Guid teamIdToDelete);
        Task AddListToProject(string NewTasksListName, Guid ProjectId);
    }
}