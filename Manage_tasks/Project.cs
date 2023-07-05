using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks
{
    public class Project
    {
        private List<Task> Tasks;
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        //public User NameOfTeam { get; set; } 
        //public User ResponsibleUser { get; set; } 
        //public Sprint SprintNumber { get; set; }

        public Project(string projectName, string projectDescription)
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            Tasks = new List<Task>();
            Project GetProject()
            {
                return new Project(projectName, projectDescription);
            }
        }
        public Project(string projectName)
        {
            ProjectName = projectName;
            Tasks = new List<Task>();
            Project GetProject()
            {
                return new Project(projectName);
            }
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }
        public void AssignMember(Member member)
        {
            foreach (Task task in Tasks)
            {
                if (task.AssignedMember == null)
                {
                    task.AssignedMember = member;
                    break;
                }
            }
        }

        public void DisplayProjectDetails()
        {
            Console.WriteLine($"Nazwa Projektu:{ProjectName}");
            Console.WriteLine($"Opis Projektu: {ProjectDescription}");
        }

        public void DisplayTasks()
        {
            Console.WriteLine($"===== Zadania projektu: {ProjectName} =====");
            foreach (Task task in Tasks)
            {
                Console.WriteLine($"- {task.TaskName}");
                Console.WriteLine($"  Opis: {task.TaskDescription}");
                Console.WriteLine($"  Przypisany członek: {task.AssignedMember?.Name ?? "Brak"}");
            }
        }
    }
}
