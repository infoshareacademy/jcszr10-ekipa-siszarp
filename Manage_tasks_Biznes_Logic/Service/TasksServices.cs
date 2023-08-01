using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Biznes_Logic.Service
{
    public static class TasksServices
    {
        public static string[] TasksNames(Project project)
        {

            if (project.Tasks == null)
            {
                var TasksNameArray = new string[0];
                return TasksNameArray;
            }
            else
            {
                var TasksNamesArray = project.Tasks.Tasks.Select(x => x.TaskName).ToArray();
                return TasksNamesArray;
            }

        }
    }


}
