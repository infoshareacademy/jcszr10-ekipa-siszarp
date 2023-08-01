using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
namespace Manage_tasks.View
{
    public static class TasksView
    {
        public static int ShowTasksList(Project project)
        {

            var title = "Lista zadań";
            var optionsArray = new[] {"Nowe zadanie", "Wróć"};

                var namesArray = TasksServices.TasksNames(project);
                var menuArray = namesArray.Concat(optionsArray).ToArray();
                ManageMenu TasksListMenu = new ManageMenu(title, menuArray);
                int index = TasksListMenu.Run();
                return index;
        }
        public static int ShowTaskDetails(Project project, int index)
        {
            var task = project.Tasks.PickTask(index);
            var taskDetails = task.TaskDetails();
            string[] options = new string[] { $"Nazwa zadania: {taskDetails[0]}", $"Opis zadania: {taskDetails[1]}", $"Status zadania: {taskDetails[2]}", $"Data zakończenia: {taskDetails[3]}", $"Użytkownik: {taskDetails[4]}", "Usuń zadanie", "Wróć" };

            ManageMenu TaskMenu = new ManageMenu(taskDetails[0], options);
            return TaskMenu.Run();

        }
        public static void ChangeTaskName(ProjectTask task, string taskName)
        {
            string[] options = new string[] {taskName, "Zapisz", "Wróć" };
            ManageMenu TaskNameMenu = new ManageMenu("", options);

            switch(TaskNameMenu.Run())
            {
                case 0:
                    Console.WriteLine("Wprowadź nową nazwę zadania");
                    var newTaskName = Console.ReadLine();
                    ChangeTaskName(task, newTaskName);
                    break;
                case 1:
                    TasksList editTaskName = new TasksList(new EditTaskName());
                    editTaskName.EditTask(taskName, task);
                    Data.projectService.SaveProjectToJson();
                    break;
                default:
                    break;
            }
        }
        public static void ChangeTaskDescription(ProjectTask task, string taskDescription)
        {
            string[] options = new string[] { taskDescription, "Zapisz", "Wróć" };
            ManageMenu TaskDescriptionMenu = new ManageMenu("", options);

            switch (TaskDescriptionMenu.Run())
            {
                case 0:
                    Console.WriteLine("Wprowadź nowy opis zadania");
                    var newTaskDescription = Console.ReadLine();
                    ChangeTaskDescription(task, newTaskDescription);
                    break;
                case 1:
                    TasksList editTaskDescription = new TasksList(new EditTaskDescription());
                    editTaskDescription.EditTask(taskDescription, task);
                    Data.projectService.SaveProjectToJson();
                    break;
                default:
                    break;
            }
        }
        public static void ChangeTaskStatus(ProjectTask task)
        {
            string[] options = new string[] { "Do zrobienia", "W trakcie", "Zrobiony" };
            ManageMenu TaskStatusMenu = new ManageMenu("", options);

            switch (TaskStatusMenu.Run())
            {
                case 0:
                    task.Status.ChangeStatus(0);
                    Data.projectService.SaveProjectToJson();
                    break;
                case 1:
                    task.Status.ChangeStatus(1);
                    Data.projectService.SaveProjectToJson();
                    break;
                case 2:
                    task.Status.ChangeStatus(2);
                    Data.projectService.SaveProjectToJson();
                    break;
            }
        }




        public static void DeleteTask(TasksList taskList, ProjectTask task)
        {
            string[] options = new string[] { "Tak", "Nie" };
            ManageMenu deleteMenu = new ManageMenu("Czy jesteś pewnien?", options);

            switch (deleteMenu.Run())
            {
                case 0:
                    taskList.RemoveTask(task);
                    Data.projectService.SaveProjectToJson();
                    break;
                default : break;   
            }
        }
    }
}
