﻿using Manage_tasks_Biznes_Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using Manage_tasks.Service;

namespace Manage_tasks.View
{
    public static class CreateProjectView
    {
        public static void Display()
        {
            Console.Clear();
            
            OutputEncoding = Encoding.Unicode;
            BackgroundColor = ConsoleColor.White;
            ForegroundColor = ConsoleColor.Magenta;

            string title = @"
 .----------------. .----------------. .----------------. .----------------. .----------------.   .-----------------..----------------. .----------------. .----------------.   .----------------. .----------------. .----------------. .----------------. .----------------. .----------------. .----------------. 
| .--------------. | .--------------. | .--------------. | .--------------. | .--------------. | | .--------------. | .--------------. | .--------------. | .--------------. | | .--------------. | .--------------. | .--------------. | .--------------. | .--------------. | .--------------. | .--------------. |
| |    _______   | | |  _________   | | | _____  _____ | | |  _______     | | |   ________   | | | | ____  _____  | | |     ____     | | | _____  _____ | | |  ____  ____  | | | |   ______     | | |  _______     | | |     ____     | | |     _____    | | |  _________   | | |  ___  ____   | | |  _________   | |
| |   /  ___  |  | | | |  _   _  |  | | ||_   _||_   _|| | | |_   __ \    | | |  |  __   _|  | | | ||_   \|_   _| | | |   .'    `.   | | ||_   _||_   _|| | | |_  _||_  _| | | | |  |_   __ \   | | | |_   __ \    | | |   .'    `.   | | |    |_   _|   | | | |_   ___  |  | | | |_  ||_  _|  | | | |  _   _  |  | |
| |  |  (__ \_|  | | | |_/ | | \_|  | | |  | | /\ | |  | | |   | |__) |   | | |  |_/  / /    | | | |  |   \ | |   | | |  /  .--.  \  | | |  | | /\ | |  | | |   \ \  / /   | | | |    | |__) |  | | |   | |__) |   | | |  /  .--.  \  | | |      | |     | | |   | |_  \_|  | | |   | |_/ /    | | | |_/ | | \_|  | |
| |   '.___`-.   | | |     | |      | | |  | |/  \| |  | | |   |  __ /    | | |     .'.' _   | | | |  | |\ \| |   | | |  | |    | |  | | |  | |/  \| |  | | |    \ \/ /    | | | |    |  ___/   | | |   |  __ /    | | |  | |    | |  | | |   _  | |     | | |   |  _|  _   | | |   |  __'.    | | |     | |      | |
| |  |`\____) |  | | |    _| |_     | | |  |   /\   |  | | |  _| |  \ \_  | | |   _/ /__/ |  | | | | _| |_\   |_  | | |  \  `--'  /  | | |  |   /\   |  | | |    _|  |_    | | | |   _| |_      | | |  _| |  \ \_  | | |  \  `--'  /  | | |  | |_' |     | | |  _| |___/ |  | | |  _| |  \ \_  | | |    _| |_     | |
| |  |_______.'  | | |   |_____|    | | |  |__/  \__|  | | | |____| |___| | | |  |________|  | | | ||_____|\____| | | |   `.____.'   | | |  |__/  \__|  | | |   |______|   | | | |  |_____|     | | | |____| |___| | | |   `.____.'   | | |  `.___.'     | | | |_________|  | | | |____||____| | | |   |_____|    | |
| |              | | |              | | |              | | |              | | |              | | | |              | | |              | | |              | | |              | | | |              | | |              | | |              | | |              | | |              | | |              | | |              | |
| '--------------' | '--------------' | '--------------' | '--------------' | '--------------' | | '--------------' | '--------------' | '--------------' | '--------------' | | '--------------' | '--------------' | '--------------' | '--------------' | '--------------' | '--------------' | '--------------' |
 '----------------' '----------------' '----------------' '----------------' '----------------'   '----------------' '----------------' '----------------' '----------------'   '----------------' '----------------' '----------------' '----------------' '----------------' '----------------' '----------------' 
";
            string[] information = { "Nazwa projektu", "Opis projektu" };
            ConsoleKeyInfo key;
            do
            {
                //SetCursorCenter(title);
                WriteLine($"{title}".PadLeft(2));

                WriteLine(Environment.NewLine);
                //SetCursorCenter(information);
                WriteLine(information);
                WriteLine(Environment.NewLine);
                
                key = ReadKey(true);
                Clear();
            }
            while (key.Key != ConsoleKey.Escape);
            BackgroundColor = ConsoleColor.Black;
            Clear();
            //RunFirstListaProjectow();

            Console.WriteLine("Tworzenie nowego projektu");
            Console.WriteLine("Podaj nazwe projektu");
            string projectName = Console.ReadLine();
            Console.WriteLine("Podaj opis projektu");
            string projectDescrition = Console.ReadLine();

            Data.projectService.CreateProject(projectName, projectDescrition);

        }
    }
}
