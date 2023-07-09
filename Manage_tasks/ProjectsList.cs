using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks
{
    public class ProjectsList
    {
        public Task SetTask { get; set; }
        List<Project> Projects { get; set; }

        public ProjectsList()
        {
            Projects = new List<Project>();

        }

        public void AddNewProject(Project newProject)
        {
            Projects.Add(newProject);
        }
        public void RemoveProject(Project project)
        {
            Projects.Remove(project);
        }
        public void DisplayProjectsList(List<Project> Projects)
        {
            foreach (var project in Projects)
            {
                Console.WriteLine($"Lista projektów: {project.ProjectName}");
            }
        }
        public Project PickProject(int indexOfProject)
        {
            Project project = Projects[indexOfProject];

            return project;
        }

    }
}
