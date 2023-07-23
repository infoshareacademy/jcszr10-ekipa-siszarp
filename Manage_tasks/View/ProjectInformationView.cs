using Manage_tasks_Biznes_Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks.View
{
    public static class ProjectInformationView
    {
        public static void DisplayInfo(int index)
        {
            Data.projectService.DisplayProjectDetails(index);
        }
    }
}
