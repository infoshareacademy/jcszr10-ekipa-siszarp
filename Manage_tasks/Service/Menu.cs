using Manage_tasks.Model;
using Manage_tasks.View;
using Manage_tasks.View.TeamView;
using Manage_tasks.View.UserView;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using System.Text;
using static System.Console;

namespace Manage_tasks.Service
{

    public class Menu
    {

        public void Start()
        {

            Data.projectService.LoadProjectsFromJson();
            Title = "Manage - tasks";
            RunMainMenu();

        }

        private void RunMainMenu()
        {
            
            OutputEncoding = Encoding.Unicode;
            string prompt = @"
████████╗ █████╗ ███████╗██╗  ██╗███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ 
╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗
   ██║   ███████║███████╗█████╔╝ ██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝
   ██║   ██╔══██║╚════██║██╔═██╗ ██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗
   ██║   ██║  ██║███████║██║  ██╗██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║
   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                                                     
Witam w TaskMasterze !
(Żeby kierować menu użyj strzałek na klawiaturze ↓ i ↑ lub enter żeby wybrac element menu.)";
            string[] options = { "Lista projektów", "Użytkownik", "O nas", "Exit" };


            ManageMenu mainMenu = new ManageMenu(prompt, options);
            int selectedIdex = mainMenu.Run();

            switch (selectedIdex)
            {
                case 0:
                    RunFirstListaProjectow();
                    break;
                case 1:
                    RunUsersMenu();
                    break;
                case 2:
                    DisplayAboutInfo();
                    break;
                case 3:
                    Exit();
                    break;

            }
        }

        private void Exit()
        {
            WriteLine("\n wcisnij jaki kolwiek klawisz by wyjsc");
            ReadKey(true);
            Environment.Exit(0);
        }

        private void DisplayAboutInfo()
        {
            BackgroundColor = ConsoleColor.White;
            Clear();

            ForegroundColor = ConsoleColor.Magenta;
            string aboutTitle = "== Ekipa - csharp ==";
            string about = @"
            Witaj w przestrzeni ekipy Siszarp! Skorzystaj 
            z naszej pierwszej zespołowej aplikacji, 
            którą razem stworzyliśmy. Każdy z nas zostawił tutaj
            swój ślad. Pamiętaj, że każda podróż zaczyna się
            od pierwszego kroku. – Lao Tzu.";
            string infoMessage = "====  Żeby zmienić kolor tekstu [Any Key], żeby wyjść [Escape]  ==== ";
            string obrazek = @"                                 
                                   /\
                              /\  //\\
                       /\    //\\///\\\        /\
                      //\\  ///\////\\\\  /\  //\\
         /\          /  ^ \/^ ^/^  ^  ^ \/^ \/  ^ \
        / ^\    /\  / ^   /  ^/ ^ ^ ^   ^\ ^/  ^^  \
       /^   \  / ^\/ ^ ^   ^ / ^  ^    ^  \/ ^   ^  \       *
      /  ^ ^ \/^  ^\ ^ ^ ^   ^  ^   ^   ____  ^   ^  \     /|\
     / ^ ^  ^ \ ^  _\___________________|  |_____^ ^  \   /||o\
    / ^^  ^ ^ ^\  /______________________________\ ^ ^ \ /|o|||\
   /  ^  ^^ ^ ^  /________________________________\  ^  /|||||o|\
  /^ ^  ^ ^^  ^    ||___|___||||||||||||___|__|||      /||o||||||\       
 / ^   ^   ^    ^  ||___|___||||||||||||___|__|||          | |           
/ ^ ^ ^  ^  ^  ^   ||||||||||||||||||||||||||||||oooooooooo| |ooooooo  
ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            ConsoleKeyInfo key;
            do
            {
                SetCursorCenter(aboutTitle);


                for (int i = 0; i < about.Length; i++)
                {

                    Random rand = new Random();
                    int numbercolor = rand.Next(6);
                    switch (numbercolor)
                    {
                        case 0: ForegroundColor = ConsoleColor.Magenta; break;
                        case 1: ForegroundColor = ConsoleColor.Blue; break;
                        case 2: ForegroundColor = ConsoleColor.Magenta; break;
                        case 3: ForegroundColor = ConsoleColor.Red; break;
                        case 4: ForegroundColor = ConsoleColor.Blue; break;
                        case 5: ForegroundColor = ConsoleColor.Red; break;
                    }

                    Write($"{about[i]}".PadLeft(2));

                }

                WriteLine(Environment.NewLine);
                SetCursorCenter(infoMessage);

                WriteLine(Environment.NewLine);
                SetBigTextCursorCenter(obrazek);
                key = ReadKey(true);
                Clear();
            }
            while (key.Key != ConsoleKey.Escape);
            BackgroundColor = ConsoleColor.Black;
            Clear();
            RunMainMenu();
        }

        private void RunFirstListaProjectow()
        {

            string promt = "Lista projektów";
            string[] options = { "Stwórz nowy project", "Wybierz project",  "Wróć" };
            ManageMenu FirstStepMenu = new ManageMenu(promt, options);
            int selectedIdex = FirstStepMenu.Run();

            switch (selectedIdex)
            {
                case 0:
                    //metoda do stworzenia nowego projectu                   
                    CreateProjectView.Display();
                    RunFirstListaProjectow();
                    break;
                case 1:
                    //Wyswietlamy wszystkie projecty  Wybierz project
                    int projectIndex = ProjectListView.DisplayListProject();

                    if(projectIndex < 0)
                    {
                        RunFirstListaProjectow();
                    }
                    else
                    {
                        RunOpcjeProjectu(projectIndex);
                    }

                    break;
                case 2:
                    RunMainMenu();
                    break;
            }

            Exit();
        }
        private void RunUsersMenu()
        {

            string prompt = "Użytkownik";
            string[] opcje = { "Pokaż wszytkich użytkowników ", "Pokaż zespoły", "Wróć" };
            ManageMenu UsersMenu = new ManageMenu(prompt, opcje);
            int index = UsersMenu.Run();
            switch (index)
            {
                case 0:
                    //Metoda pokazuje wszystkich uzytkownikow
                    var usersListView = new UsersListView();
                    usersListView.Run();
                    RunMainMenu();
                    break;
                case 1:
                    //Metoda pokazuje zespoly
                    var teamListView = new TeamsListView();
                    teamListView.Run();
                    RunMainMenu();
                    break;
                case 2:
                    RunMainMenu();
                    break;

            }
        }
        private void RunOpcjeProjectu(int index)
        {
            string title = "Informacja o projekcie";
            string[] options = { "Zadania", "Zespół", "Usuń project", "Wróć" };

            int selectedIndex = InfoAboutProject.RunPoziom(index, title, options, 0, 20);
            switch (selectedIndex)
            {
                case 0:
                    //Listy zadań
                    RunTaskMenu(Data.projectService.GetProject(index), TasksView.ShowTasksList(Data.projectService.GetProject(index)), index);
                    break;
                case 1:
                    //Przypisana Ekipa
                    var projectTeamView = new ProjectTeamView(Data.projectService.GetProject(index));
                    projectTeamView.Run();

                    RunOpcjeProjectu(index);
                    break;
                case 2:
                    Data.projectService.RemoveProject(index);
                    RunFirstListaProjectow();
                    break;
                
                case 3:
                    RunFirstListaProjectow();
                    break;
            }


        }

        private void RunTaskMenu(Project project, int index, int projectIndex)
        {
            int length = TasksServices.TasksNames(project).Length;
            if(index == length + 1)
            {
                RunOpcjeProjectu(projectIndex);
            }
            else if(index == length) 
            {
                CreateTaskView.CreateNewTask(project);
                RunOpcjeProjectu(projectIndex);
            }
            else
            {
                RunTaskDetails(project, TasksView.ShowTaskDetails(project, index), index, projectIndex);
            }
        }

        private void RunTaskDetails(Project project, int index, int prevIndex, int projectIndex)
        {
            switch(index)
            {
                case 0:
                    //nazwa
                    TasksView.ChangeTaskName(project.Tasks.Tasks[prevIndex], project.Tasks.Tasks[prevIndex].TaskDetails()[index]);
                    RunTaskMenu(project, prevIndex, projectIndex);
                    break;
                case 1:
                    //opis
                    TasksView.ChangeTaskDescription(project.Tasks.Tasks[prevIndex], project.Tasks.Tasks[prevIndex].TaskDetails()[index]);
                    RunTaskMenu(project, prevIndex, projectIndex);
                    break;
                case 2:
                    //status
                    TasksView.ChangeTaskStatus(project.Tasks.Tasks[prevIndex]);
                    RunTaskMenu(project, prevIndex, projectIndex);
                    break;
                case 3:
                    //data zakonczenia
                    TasksView.ChangeFinishDate(project.Tasks.Tasks[prevIndex]);
                    RunTaskMenu(project, prevIndex, projectIndex);
                    break;
                case 4:
                    //user
                    TasksView.ChangeAssignedUser(prevIndex, project);
                    RunTaskMenu(project, prevIndex, projectIndex);
                    break;
                case 5:
                    //kasacja
                    TasksView.DeleteTask(project.Tasks, project.Tasks.Tasks[prevIndex]);
                    RunTaskMenu(project, TasksView.ShowTasksList(project), projectIndex);
                    break;
                case 6:
                    //powrot
                    RunTaskMenu(project, TasksView.ShowTasksList(project),projectIndex);
                    break;

            }
        }

        public void SetCursorCenter(string message)
        {
            SetCursorPosition((WindowWidth - message.Length) / 2, CursorTop);
            WriteLine(message);
        }
        private void SetBigTextCursorCenter(string prompt)
        {
            string[] lines = prompt.Split(Environment.NewLine);


            int maxLength = 0;
            foreach (string line in lines)
            {
                int length = line.Length;
                if (length > maxLength)
                    maxLength = length;
            }

            int leftPadding = (WindowWidth - maxLength) / 2;
            foreach (string line in lines)
            {
                SetCursorPosition(leftPadding, CursorTop);
                WriteLine(line);
            }


        }
    }
}
