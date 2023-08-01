using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Manage_tasks_Biznes_Logic.Data;

namespace Manage_tasks.View
{
    public static class CreateTaskView
    {
        public static void CreateNewTask(Project project)
        {
            string[] newTask = new string[2];
            Console.WriteLine("Podaj nazwe nowego zadania");
            newTask[0] = Console.ReadLine();
            Console.WriteLine("Podaj opis nowego zadania");
            newTask[1] = Console.ReadLine();
            var task = new ProjectTask(newTask[0], newTask[1]);
            
            project.Tasks.AddTask(task);
            Data.projectService.SaveProjectToJson();
        }
    }
}
