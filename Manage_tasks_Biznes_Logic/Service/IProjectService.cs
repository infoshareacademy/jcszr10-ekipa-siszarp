using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
	public interface IProjectService
	{
		void ChangeTeam(Guid projectId, Guid newProjectTeamId);
		void CreateProject(string name, string description);
		void EditNameAndDescription(Guid projectId, string newName, string newDescription);
		List<Project> GetAllProjects();
		Project GetProjectById(Guid projectId);
		List<Project> LoadProjectsFromJson();
		void RemoveProject(Guid id);
	}
}