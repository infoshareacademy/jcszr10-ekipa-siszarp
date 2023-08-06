using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Manage_tasks.View.ProjectView
{
    public static class InfoAboutProject
    {
        public static void Display(int index)
        {
            Project currentProject = Data.projectService.GetProject(index);

            if (currentProject.ProjectTeam != null && currentProject.Tasks != null)
            {
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Project name");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Name}");
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Opis projectu");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Description}");

                SetCursorCenter($"\t\t\u001b[91mZespół: \u001b[93m{currentProject.ProjectTeam.Name}  \u001b[91mLeader: \u001b[93m{currentProject.ProjectTeam.Leader}\u001b[0m");
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("O zespole");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.ProjectTeam.Description}");
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Zadania");
                ForegroundColor = ConsoleColor.Yellow;
                foreach (var task in currentProject.Tasks.Tasks.Take(4))
                {
                    if (task.Status.CurrentStatus == "ToDo" || task.Status.CurrentStatus == "InProgress")
                    {
                        ForegroundColor = ConsoleColor.Red;
                        SetCursorCenter($"[{task.TaskName}]");
                        ForegroundColor = ConsoleColor.DarkYellow;
                        SetCursorCenter($" - {(task.Status.CurrentStatus == "ToDo" ? "Do zrobienia" : "W trakcie")}");
                        ForegroundColor = ConsoleColor.Yellow;
                        SetCursorCenter($"Wykonuje: {(task.AssignedUser == null ? "nikt" : task.AssignedUser.FirstName)}");
                    }


                }
            }
            else if (currentProject.ProjectTeam != null)
            {

                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Project name");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Name}");
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Opis projectu");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Description}");

                SetCursorCenter($"\t\t\u001b[91mZespół: \u001b[93m{currentProject.ProjectTeam.Name}  \u001b[91mLeader: \u001b[93m{currentProject.ProjectTeam.Leader}\u001b[0m");
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("O zespole");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.ProjectTeam.Description}");
            }
            else if (currentProject != null)
            {
                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Project name");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Name}");

                ForegroundColor = ConsoleColor.Blue;
                SetCursorCenter("Opis projectu");
                ForegroundColor = ConsoleColor.Yellow;
                SetCursorCenter($"{currentProject.Description}");
                ResetColor();
            }


        }
        public static void DisplayOptionPoziom(int index, string title, int SelectedIndex, string[] Options, int Padding = 1)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            SetCursorCenter(title);

            ResetColor();
            WriteLine();

            Display(index);

            SetCursorPosition(CursorLeft, 25);
            for (int i = 0; i < Options.Length; i++)
            {

                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {

                    prefix = "◁";

                    ForegroundColor = ConsoleColor.Blue;

                }
                else
                {
                    prefix = " ";
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }
                int offset = Padding;
                Write(new string(' ', offset));
                Write($"{currentOption}{prefix}  ");

            }
            ResetColor();
        }
        public static int RunPoziom(int index, string title, string[] Options, int SelectedIndex, int Padding = 1)
        {
            ConsoleKey keyPressed;

            do
            {
                Clear();

                DisplayOptionPoziom(index, title, SelectedIndex, Options, Padding);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.LeftArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.RightArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }

                }


            }
            while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
        private static void SetCursorCenter(string currentOption)
        {
            SetCursorPosition((WindowWidth - currentOption.Length) / 2, CursorTop);
            WriteLine(currentOption);
        }

    }
}
