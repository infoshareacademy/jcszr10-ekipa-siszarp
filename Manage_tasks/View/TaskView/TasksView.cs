using Manage_tasks.Model;
using Manage_tasks.View.TeamView;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using System;

namespace Manage_tasks.View.TaskView
{
    public static class TasksView
    {
        public static int ShowTasksList(Project project)
        {


            var optionsArray = new[] { "Nowe zadanie", "Wróć" };

            var namesArray = TasksServices.TasksNames(project);
            var menuArray = namesArray.Concat(optionsArray).ToArray();
            ManageMenu TasksListMenu = new ManageMenu("Lista zadań", menuArray);
            int index = TasksListMenu.Run();
            return index;
        }
        public static int ShowTaskDetails(Project project, int index)
        {
            var task = project.Tasks.PickTask(index);
            var taskDetails = task.TaskDetails();
            string[] options = new string[] { $"Nazwa zadania: {taskDetails[0]}", $"Opis zadania: {taskDetails[1]}", $"Status zadania: {taskDetails[2]}", $"Data zakończenia: {taskDetails[3]}", $"Przypisany użytkownik: {taskDetails[4]}", "Usuń zadanie", "Wróć" };

            ManageMenu TaskMenu = new ManageMenu($"Szczegóły zadania: {taskDetails[0]}", options);
            return TaskMenu.Run();

        }
        public static void ChangeTaskName(ProjectTask task, string taskName)
        {
            string[] options = new string[] { taskName, "Zapisz", "Wróć" };
            ManageMenu TaskNameMenu = new ManageMenu("Nazwa zadania", options);

            switch (TaskNameMenu.Run())
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
            ManageMenu TaskDescriptionMenu = new ManageMenu("Opis zadania", options);

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
            ManageMenu TaskStatusMenu = new ManageMenu("Wybierz obecny status", options);

            switch (TaskStatusMenu.Run())
            {
                case 0:
                    task.Status.ChangeStatus(0);
                    task.CheckIfFinished();
                    break;
                case 1:
                    task.Status.ChangeStatus(1);
                    task.CheckIfFinished();
                    break;
                case 2:
                    task.Status.ChangeStatus(2);
                    task.CheckIfFinished();
                    break;
            }
            Data.projectService.SaveProjectToJson();
        }

        public static void ChangeFinishDate(ProjectTask task)
        {
            string[] options = new string[] { "Ustaw obacną date zakończenia", "Wybierz inną date zakończenia", "Wróć" };
            ManageMenu finishDateMenu = new ManageMenu("Wybierz date zakończenia", options);

            switch (finishDateMenu.Run())
            {
                case 0:
                    task.Status.ChangeStatus(2);
                    task.CheckIfFinished();
                    break;
                case 1:
                    //data z palca
                    string[] newDate = new string[3];
                    Console.WriteLine("Podaj rok");
                    newDate[0] = Console.ReadLine();
                    Console.WriteLine("Podaj miesiąć");
                    newDate[1] = Console.ReadLine();
                    Console.WriteLine("Podaj dzień");
                    newDate[2] = Console.ReadLine();
                    TasksList editTaskFinishDate = new TasksList(new EditTaskFinishDate());
                    editTaskFinishDate.EditTask(string.Join(".", newDate), task);
                    task.Status.ChangeStatus(2);
                    break;
                default:
                    break;
            }
        }


        public static void ChangeAssignedUser(int taskIndex, Project project)
        {
            if (project.ProjectTeam == null)
            {
                string[] options = new string[] { "Przypisz zespół", "Wróć" };
                ManageMenu changeAssignedUserMenu = new ManageMenu("Brak przypisanego zespołu", options);

                switch (changeAssignedUserMenu.Run())
                {
                    case 0:
                        var projectTeamView = new ProjectTeamView(project);
                        projectTeamView.Run();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                var task = project.Tasks.PickTask(taskIndex);
                List<User> projectTeam = project.ProjectTeam.GetMembers().ToList();
                var projectTeamOptions = projectTeam.Select(x => x.FirstName).ToArray();

                var options = projectTeamOptions.Concat(new string[] { "Wróć" }).ToArray();
                string AssignedUser;
                if (task.AssignedUser != null)
                {
                    AssignedUser = task.AssignedUser.FirstName;
                }
                else
                {
                    AssignedUser = string.Empty;
                }
                ManageMenu changeAssignedUserMenu = new ManageMenu($"Przypisany użytkownik: {AssignedUser}", options);

                int index = changeAssignedUserMenu.Run();
                if (index == projectTeamOptions.Length)
                {

                }
                else
                {
                    project.Tasks.AssignUser(projectTeam[index], taskIndex);
                    Data.projectService.SaveProjectToJson();
                }
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
                default:
                    break;
            }
        }
    }
}
