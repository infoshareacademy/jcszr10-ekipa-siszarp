﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks.Model;
using Manage_tasks.Service;
using Manage_tasks_Biznes_Logic.Data;


namespace Manage_tasks.View
{
    public static class ProjectListView
    {
        public static int DisplayListProject()
        {
            Data.projectService.LoadProjectsFromJson();   //Pobieramy dane z jsone
            var projects = Data.projectService.GetAllProject();
            var title = "Wszystkie projekty";
            string[] allNameOfProject = projects.Select(a=> a.Name).ToArray();
            if (allNameOfProject.Length < 1)
            {
                title = "Nie ma żadnych projektów";
            }
            ManageMenu AllProjects = new ManageMenu(title,allNameOfProject);
            int ChoiceIndex = AllProjects.Run();
            return ChoiceIndex;
        }
    }
}
