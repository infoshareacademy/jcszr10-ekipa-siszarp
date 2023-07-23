using Manage_tasks.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Manage_tasks.Model
{
    public class ManageMenu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Promt;
        public ManageMenu(string prompt, string[] options)
        {
            Promt = prompt;
            Options = options;
             
        }

        public ManageMenu(string prompt, string[] options , int index)
        {
            Promt = prompt;
            Options = options;
            SelectedIndex = index;
        }
        public void DisplayOption()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            SetBigTextCursorCenter(Promt);
            ResetColor();
            WriteLine();

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
                SetCursorCenter($"{currentOption}{prefix}");
                WriteLine($"{currentOption}{prefix}");

            }
            ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;

            do
            {
                Clear();
                DisplayOption();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
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
        
        public void DisplayOptionPoziom(int Padding = 1)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            SetBigTextCursorCenter(Promt);
            ResetColor();
            WriteLine();

            

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
        public int RunPoziom(int Padding = 1)
        {
            ConsoleKey keyPressed;

            do
            {
                Clear();

                DisplayOptionPoziom(Padding);

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
        private void SetCursorCenter(string currentOption)
        {
            SetCursorPosition((WindowWidth - $"{currentOption}".Length) / 2, CursorTop);
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
