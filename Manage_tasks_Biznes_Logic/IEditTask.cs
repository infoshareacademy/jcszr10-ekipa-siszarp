﻿namespace Manage_tasks
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
   //public class EditTaskDueDate :IEditTask
   //{
   //    public void EditTask(string newTaskDueDate, ProjectTask task)
   //    {
   //        task.DueDate = DateTime.Parse(newTaskDueDate);
   //    }
   //}
}