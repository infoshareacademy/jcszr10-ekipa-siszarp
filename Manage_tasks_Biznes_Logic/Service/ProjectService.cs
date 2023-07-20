using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
    public interface IProjectService
    {
        int CreateProject(string name, string description); //zwraca stworzone id projektu.
        Project GetProject(int projectId);
    }

    public class ProjectService  //: IProjectService
    // PROJEKT: WYŚWIETL LISTĘ ZADAŃ, PRZYPISZ ZESPÓŁ, LISTA SPRINTÓW PROJEKTU, USUŃ PROJEKT
    {
        private List<Project> Projects = new List<Project>();
        private List<ProjectTask> CurrentTasks = new List<ProjectTask>();//lista zadan przypisana do konkretnego projektu - nie wiem czy dobrze??
        private List<User> TeamUsers = new List<User>();//lista członków zespołu przypisanego do projektu
        public string DisplayTasks()
        {
            var taskEnumerator = CurrentTasks.GetEnumerator();
            if (taskEnumerator.MoveNext())
            {
                return $"- {taskEnumerator.Current.TaskName} Opis: {taskEnumerator.Current.TaskDescription} Status zadania: {taskEnumerator.Current.Status}";
            }
            else return "Brak utworzonych projektów.";
        }
        public void AssignTeam(User member)
        {
            foreach (Project project in Projects)
            {
                TeamUsers.Add(member);
            }
        }

        public void AssignTask(ProjectTask task)
        {
            CurrentTasks.Add(task);
        }

        public string DisplayProjectDetails()
        {
            var projectEnumerator = Projects.GetEnumerator();
            if (projectEnumerator.MoveNext())
            {
                return $"Nazwa Projektu:{projectEnumerator.Current.Name} Opis Projektu: {projectEnumerator.Current.Description}";
            }
            else return "Brak utworzonych projektów.";
        }
        public void RemoveProject(Project project)
        {
            Projects.Remove(project);
        }

        public void CreateProject(string name, string description)
        {
            Projects.Add(new Project(name, description));
        }

        public Project GetProject(int projectId)
        {
            throw new NotImplementedException();
        }
    }


    //Project: nowe pole int Id {get;set;}

    //ProjectService methods:
    //CreateProject(string name, string description);
    //Project GetProject(int projectId);
}
