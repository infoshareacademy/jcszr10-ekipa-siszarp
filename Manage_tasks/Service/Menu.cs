using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks.Model;
using static System.Console;


namespace Manage_tasks.Service
{

    public class Menu
    {

        public void Start()
        {

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
                WriteLine($"{aboutTitle}");

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
                WriteLine(infoMessage);
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
            string[] options = { "Wybierz project", "Nowy project", "Wróć" };
            ManageMenu FirstStepMenu = new ManageMenu(promt, options);
            int selectedIdex = FirstStepMenu.Run();

            switch (selectedIdex)
            {
                case 0:
                    //Co sie dzieje gdy wybierzsz "Wybierz project"
                    break;
                case 1:
                    //metoda do stworzenia nowego projectu
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
            string[] opcje = { "Pokaż wszytkich użytkowników ", "Pokaż Team", "Wróć" };
            ManageMenu UsersMenu = new ManageMenu(prompt, opcje);
            int index = UsersMenu.Run();
            switch (index)
            {
                case 0:
                    //Metoda pokazuje wszystkich uzytkownikow
                    break;
                case 1:
                    //Metoda pokazuje teamsy
                    break;
                case 2:
                    RunMainMenu();
                    break;

            }
        }
        private void RunOpcjeProjectu()
        {
            string promt = "Hello ";
            string[] options = { "Listy zadań", "Przypisana Ekipa", "Usuń project", "Lista sprintów", "Wróć" };
            ManageMenu FirstStepMenu = new ManageMenu(promt, options);
            int selectedIdex = FirstStepMenu.Run();
            switch (selectedIdex)
            {
                case 0:
                    //Listy zadań
                    break;
                case 1:
                    //Przypisany Ekipa
                    break;
                case 2:
                    //Usun project
                    break;
                case 3:
                    //Lista sprintów
                    break;
                case 4:
                    RunMainMenu();
                    break;
            }
        }
        public void SetCursorCenter(string message)
        {
            SetCursorPosition((WindowWidth - message.Length) / 2, CursorTop);
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
                Console.WriteLine(line);
            }
           

        }
    }
}
