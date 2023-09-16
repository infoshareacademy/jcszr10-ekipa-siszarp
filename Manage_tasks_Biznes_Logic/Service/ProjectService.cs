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
            return Projects;
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

        public void RemoveProject(int index)
        {

            try
            {
                Projects.RemoveAt(index);
                SaveProjectToJson();
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }
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
    }


}
