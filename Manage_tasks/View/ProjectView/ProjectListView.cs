﻿using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;


namespace Manage_tasks.View.ProjectView
{
    public static class ProjectListView
    {
        public static int DisplayListProject()
        {

            var projects = Data.projectService.GetAllProject();

            if (projects.Count == 0)
            {
                return -1;
            }

            var title = "Wszystkie projekty";
            string[] allNameOfProject = projects.Select(a => a.Name).ToArray();
            if (allNameOfProject.Length < 1)
            {
                title = "Nie ma żadnych projektów";
            }
            ManageMenu AllProjects = new ManageMenu(title, allNameOfProject);
            int ChoiceIndex = AllProjects.Run();
            return ChoiceIndex;
        }
    }
}
