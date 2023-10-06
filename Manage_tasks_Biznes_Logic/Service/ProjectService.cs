using Manage_tasks_Biznes_Logic.Model;
using System.Text.Json;


namespace Manage_tasks_Biznes_Logic.Service
{

    public interface IProjectService
    {
        List<Project> GetAllProjects();
        Project GetProjectById(Guid projectId);
        List<Project> LoadProjectsFromJson();
        void CreateProject(string name, string description);
        public void RemoveProject(Guid id);
        void ChangeTeam(Guid projectId, Guid newProjectTeamId);
        void EditNameAndDescription(Guid projectId, string newName, string newDescription);
    }

    public class ProjectService : IProjectService

    {
        private static List<Project> Projects = new();

        const string _nameJsonFile = "ListaProjectow.json";
        public void SaveProjectToJson()
        {

            string objectSerialized = JsonSerializer.Serialize(Projects);
            File.WriteAllText(_nameJsonFile, objectSerialized);
        }

        public List<Project> LoadProjectsFromJson()
        {
            if (!File.Exists(_nameJsonFile))
            {
                Projects = new List<Project>();
            }

            Projects = JsonSerializer.Deserialize<List<Project>>(File.ReadAllText(_nameJsonFile));

            return Projects;
        }

        public List<Project> GetAllProjects()
        {
            return LoadProjectsFromJson();
        }

        public Project GetProjectById(Guid projectId)
        {
            try
            {
                return GetAllProjects().FirstOrDefault(p => p.Id == projectId);
            }
            catch (Exception ex)
            {
                Project ExceptionProject = new Project();
                ExceptionProject.Crash();
                return ExceptionProject;

            }
        }

        public void RemoveProject(Guid id)
        {
            var projects = GetAllProjects();

            var projectInDatabase = projects.FirstOrDefault(p => p.Id == id);

            if (projectInDatabase is null)
            {
                return;
            }

            projects.Remove(projectInDatabase);

            SaveProjectToJson();
        }

        public void CreateProject(string name, string description)
        {
            Projects.Add(new Project(name, description));
            SaveProjectToJson();
        }

        public void UpdateProject(Project project)
        {
            var projects = GetAllProjects();

            var projectInDatabase = projects.FirstOrDefault(u => u.Id == project.Id);

            if (projectInDatabase is not null)
            {
                projects.Remove(projectInDatabase);
            }
            else
            {
                project.Id = Guid.NewGuid();
            }

            projects.Add(project);

            SaveProjectToJson();
        }
        public void EditNameAndDescription(Guid projectId, string newName, string newDescription)
        {
            var projects = GetAllProjects();
            var projectInDataBase = GetProjectById(projectId);

            if (projectInDataBase is null)
            {
                return;
            }
            else
            {
                projectInDataBase.Name = newName;
                projectInDataBase.Description = newDescription;
            }

            projects.Add(projectInDataBase);
            SaveProjectToJson();
        }
        public void ChangeTeam(Guid projectId, Guid newProjectTeamId)
        {
            var projects = GetAllProjects();
            var projectInDataBase = GetProjectById(projectId);
            if (projectInDataBase is null)
            {
                projectInDataBase.Id = new Guid();
                projectInDataBase.ProjectTeamId = newProjectTeamId;
            }
            else
            {
                projectInDataBase.ProjectTeamId = newProjectTeamId;
            }
            projects.Add(projectInDataBase);
            SaveProjectToJson();
        }
    }
}
