using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Manage_tasks
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
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string prompt = @"
████████╗ █████╗ ███████╗██╗  ██╗███╗   ███╗ █████╗ ███████╗████████╗███████╗██████╗ 
╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝████╗ ████║██╔══██╗██╔════╝╚══██╔══╝██╔════╝██╔══██╗
   ██║   ███████║███████╗█████╔╝ ██╔████╔██║███████║███████╗   ██║   █████╗  ██████╔╝
   ██║   ██╔══██║╚════██║██╔═██╗ ██║╚██╔╝██║██╔══██║╚════██║   ██║   ██╔══╝  ██╔══██╗
   ██║   ██║  ██║███████║██║  ██╗██║ ╚═╝ ██║██║  ██║███████║   ██║   ███████╗██║  ██║
   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝   ╚═╝   ╚══════╝╚═╝  ╚═╝
                                                                                     
Witam w TaskMasterze !
(Żeby kierować menu użyj strzałek na klawiaturze ↓ i ↑ lub enter żeby wybrac element menu.)";
            string[] options = { "Project's", "O nas", "Exit"};


            ManageMenu mainMenu = new ManageMenu(prompt, options );
            int selectedIdex = mainMenu.Run();

            switch (selectedIdex)
            {
               case 0:
                  //zrob to jezeli wybrany 0 element
                   break;
               case 1:
                   //zrob to jezeli wybrany 1 element
                   break;
               case 2:
                   //zrob to jezeli wybrany 2 element
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
            Clear();
            WriteLine("About us ");
            ReadKey(true);
            RunMainMenu();
        }

        private void RunFirstChoice()
        {
            string promt = "Hello ";
            string[] options = { "Hello " };
            ManageMenu FirstStepMenu = new ManageMenu(promt, options);
            int selectedIdex = FirstStepMenu.Run();

            switch (selectedIdex)
            {
                case 0:

                   
                    break;
                case 1:

                    
                    break;
                case 2:

                   
                    break;
                case 3:

                   
                    break;
                case 4:
                    RunMainMenu();

                    break;
            }
           
            Exit();
        }
    }
}
