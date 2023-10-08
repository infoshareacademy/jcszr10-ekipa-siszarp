
using Manage_tasks_Biznes_Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Biznes_Logic.Service
{
    public interface ITaskService
    {
        string[] TasksNames(Project project);
        ProjectTask CreateNewTask(string taskName, string taskDescription);
        void AddTask(ProjectTask task);
        void RemoveTask(ProjectTask task);
        ProjectTask? GetTaskByGuid(Guid taskGuid);
        TasksList? GetAllTasks();
        void EditTask(string[] newValues, ProjectTask task);
    }



    public class TaskService :ITaskService
    {
        public static TasksList tempTasks = new TasksList();
        
        public string[] TasksNames(Project project)
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
        public ProjectTask CreateNewTask(string taskName, string taskDescription)
        {
            return new ProjectTask(taskName, taskDescription);
        }
        public void AddTask(ProjectTask task)
        {
            tempTasks.AddTask(task);
        }
        public void RemoveTask(ProjectTask task)
        {
            tempTasks.RemoveTask(task);
        }
        public ProjectTask? GetTaskByGuid(Guid taskGuid)
        {
            return tempTasks.GetTaskByGuid(taskGuid);            
        }
        public TasksList GetAllTasks()
        {
            return tempTasks;
        }
        public void EditTask(string[] newValues, ProjectTask task)
        {
            TasksList editTaskName = new TasksList(new EditTaskName());
            editTaskName.EditTask(newValues[0], task);
            TasksList editTaskDescription = new TasksList(new EditTaskDescription());
            editTaskDescription.EditTask(newValues[1], task);
            TasksList editTaskStatus = new TasksList(new EditTaskStatus());
            editTaskStatus.EditTask(newValues[2], task);
            if (newValues[2] == "0" || newValues[2] == "1")
            {
                newValues[3] = null;
                TasksList editTaskFinishDate = new TasksList(new EditTaskFinishDate());
                editTaskFinishDate.EditTask(newValues[3], task);
            }
            
            
        }
        
    }


}
