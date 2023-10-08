using System.Globalization;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic
{
    public interface IEditTask
    {
        public void EditTask(string value, ProjectTask task);
    }
    public class EditTaskName :IEditTask
    {
        public void EditTask(string newTaskName, ProjectTask task)
        {
            task.TaskName = newTaskName;          
        }
    }
    public class EditTaskDescription :IEditTask
    {
        public void EditTask(string newTaskDescription, ProjectTask task)
        {
            task.TaskDescription = newTaskDescription;
        }
    }
    public class EditTaskFinishDate : IEditTask
    {
        public void EditTask(string newTaskFinishDate, ProjectTask task)
        {
            var cultureInfo = new CultureInfo("pl-PL");
            if (newTaskFinishDate != null)
            {
                task.FinishDate = DateTime.Parse(newTaskFinishDate, cultureInfo);
                task.Status.ChangeStatus(2);
            }
            else
            {

            }
        }
    }
    public class EditTaskStatus : IEditTask
    {
        public void EditTask(string statusId, ProjectTask task)
        {                        
                task.Status.ChangeStatus(Int32.Parse(statusId));
                task.CheckIfFinished();          
        }
    }
}
