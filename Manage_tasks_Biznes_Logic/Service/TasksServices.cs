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
        public static string[] TasksNames(TasksList tasksList)
        {

            if (tasksList == null)
            {
                var TasksNameArray = new string[0];
                return TasksNameArray;
            }
            else
            {
                var TasksNamesArray = tasksList.Tasks.Select(x => x.TaskName).ToArray();
                return TasksNamesArray;
            }

        }
    }


}
