using Manage_tasks_Biznes_Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks.View
{
    public static class CreateProjectView
    {
        public static void Display()
        {
            Console.Clear();

            Console.WriteLine("Tworzenie nowego projektu");
            Console.WriteLine("Podaj nazwe projektu");
            string projectName = Console.ReadLine();
            Console.WriteLine("Podaj opis projektu");
            string projectDescrition = Console.ReadLine();

            Data.projectService.CreateProject(projectName, projectDescrition);

        }
    }
}
