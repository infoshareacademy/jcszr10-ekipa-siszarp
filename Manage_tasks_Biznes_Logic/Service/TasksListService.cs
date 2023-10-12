using Manage_tasks_Biznes_Logic.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Biznes_Logic.Service
{
    public interface ITasksListService
    {
        public TasksList GetListByStatus(TasksList list, int statusId);
    }
    public class TasksListService : ITasksListService
    {
        public TasksList GetListByStatus(TasksList list, int statusId)
        {
            TasksList tasksList = new TasksList();

            tasksList.Tasks = list.Tasks.Where(t => t.Status.StatusID() == statusId).ToList();

            return tasksList;
        }
    }
}
