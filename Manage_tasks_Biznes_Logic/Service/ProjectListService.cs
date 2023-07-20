using Manage_tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks_Biznes_Logic.Model;
using System.ComponentModel.Design;

namespace Manage_tasks_Biznes_Logic.Service
{
    public class ProjectListService
    //LISTA PROJEKTÓW: WYBIERZ PROJEKT, NOWY PROJEKT
    {
        private List<Project> Projects = new List<Project>();
        public Project PickProject(int indexOfProject)
        {
            Project project = Projects[indexOfProject]; //???

            return project;
        }
        public void AddNewProject(Project newProject)
        {
            Projects.Add(newProject);
        }
        public string DisplayProjectsList()
        {
            var projectEnumerator = Projects.GetEnumerator();
            if (projectEnumerator.MoveNext())
            {
                return $"Lista projektów: {projectEnumerator.Current.Name}";
            }
            else return "Brak utworzonych projektów.";
        }

    }
}
