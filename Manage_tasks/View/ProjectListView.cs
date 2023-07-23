using System;
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
            var projects = Data.projectService.GetAllProject();
            var title = "Wszystkie projekty";
            string[] allNameOfProject = projects.Select(a=> a.Name).ToArray();
            ManageMenu AllProjects = new ManageMenu(title,allNameOfProject);
            int ChoiceIndex = AllProjects.Run();
            return ChoiceIndex;
        }
    }
}
