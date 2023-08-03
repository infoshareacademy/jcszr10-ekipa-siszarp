using Manage_tasks_Biznes_Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Manage_tasks.Service;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View
{
    public static class CreateProjectView
    {
        public static void Display()
        {
             Clear();
            
            OutputEncoding = Encoding.Unicode;
           
            ForegroundColor = ConsoleColor.Magenta;

            string title = "Stwórz nowy project";
            string descrition = "Podaj opis projektu";
            string name = "Podaj nazwę";
            
            Menu newobject = new Menu();
            newobject.SetCursorCenter(title);
            newobject.SetCursorCenter(name);
            SetCursorPosition(30, CursorTop);
            string projectName = ReadLine();  

            newobject.SetCursorCenter(descrition);
            SetCursorPosition(30, CursorTop);
            string projectDescrition = ReadLine();
            
            WriteLine();
            Data.projectService.CreateProject(projectName, projectDescrition);
            

        }
    }
    
}
