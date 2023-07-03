namespace Manage_tasks
{
    public interface IEdit
    {
        public void Edit(string value, ProjectTask task);
             
    }
    public class EditTaskName :IEdit
    {
        public void Edit(string newTaskName, ProjectTask task)
        {
            task.TaskName = newTaskName;
            
        }
    }
    public class EditTaskDescription :IEdit
    {
        public void Edit(string newTaskDescription, ProjectTask task)
        {
            task.TaskDescription = newTaskDescription;
        }
    }
    public class EditTaskDueDate :IEdit
    {
        public void Edit(string newTaskDueDate, ProjectTask task)
        {
            task.DueDate = DateTime.Parse(newTaskDueDate);
        }
    }
}
