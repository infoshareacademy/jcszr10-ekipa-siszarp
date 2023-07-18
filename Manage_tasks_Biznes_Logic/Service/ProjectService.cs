using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
    public class ProjectService
    // PROJEKT: WYŚWIETL LISTĘ ZADAŃ, PRZYPISZ ZESPÓŁ, LISTA SPRINTÓW PROJEKTU, USUŃ PROJEKT
    {
        private List <Project> Projects;
        private List <ProjectTask> CurrentTasks;//lista zadan przypisana do konkretnego projektu - nie wiem czy dobrze??

        public void DisplayTasks()
        {
            Console.WriteLine($"===== Zadania projektu: {new Project().ProjectName} =====");
            
            var taskEnumerator = CurrentTasks.GetEnumerator();
            while (taskEnumerator.MoveNext())
            {
                Console.WriteLine($"- {taskEnumerator.Current.TaskName}");
                Console.WriteLine($"  Opis: {taskEnumerator.Current.TaskDescription}");
                Console.WriteLine($"  Status zadania: {taskEnumerator.Current.Status}");
            }
        }
         void AssignTeam(User member)
        {
            foreach (Project project in Projects)
            {
               //przypisanie zespołu do projektu  - nie wiem do końca jak ??
            }
        }
        
        public void AssignTaskToProject(Task task)
        {
            new List<Task>().Add(task);
        }

        public void DisplayProjectDetails()
        {
            Console.WriteLine($"Nazwa Projektu:{new Project().ProjectName}");
            Console.WriteLine($"Opis Projektu: {new Project().ProjectDescription}");
        }
        public void RemoveProject(Project project)
        {
            new List<Project>().Remove(project);
        }
    }
}
