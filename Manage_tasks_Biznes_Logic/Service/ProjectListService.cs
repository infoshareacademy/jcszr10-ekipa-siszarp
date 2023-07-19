using Manage_tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
    public class ProjectListService
    //LISTA PROJEKTÓW: WYBIERZ PROJEKT, NOWY PROJEKT
    {
        public Project PickProject(int indexOfProject)
        {
            Project project = new List<Project>()[indexOfProject]; //???

            return project;
        }
        public void AddNewProject(Project newProject)
        {
            new List<Project>().Add(newProject);
        }
        public void DisplayProjectsList(List<Project> Projects)
        {
            foreach (var project in Projects)
            {
                Console.WriteLine($"Lista projektów: {project.ProjectName}");
            }
        }
        
    }
}
