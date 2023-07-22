using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Manage_tasks.View
{
    public static class InfoAboutProject  
    {
        public static  void Display(int index)
        {
            Project currentProject = Data.projectService.GetProject(index);
            if (currentProject != null)
            {
                WriteLine("Project name");
                WriteLine($"{currentProject.ProjectName}");
                WriteLine("Opis projectu");
                WriteLine($"{currentProject.ProjectDescription}");
            }
            else if (currentProject.ProjectTeam != null) 
            {
                WriteLine($"Ekipa: {currentProject.ProjectTeam.Name}  Leader: {currentProject.ProjectTeam.Leader}");
                WriteLine("O ekipie");
                WriteLine($" {currentProject.ProjectTeam.Description}");
            }
            else
            {
                WriteLine("Project nie zostal wybrany");
            }
        }
    }
}
