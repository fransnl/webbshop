using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class ProductMenu
    {
        private static int option = 0;
        private static List<Models.Produkt> produkter;

        public static void StartMenu(int choice)
        {
            produkter = DapperDatabase.MenuChoice(choice);

            for (int i = 0; i < produkter.Count + 1; i++)
            {
                SetCursorPosition(50, i + 27);
                if (i == option)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    ForegroundColor = ConsoleColor.White;
                    BackgroundColor = ConsoleColor.Black;
                }

                string menuOption;
                if (i < produkter.Count)
                {
                    menuOption = $"  {produkter[i].Namn}".PadRight(18, ' ');
                }
                else
                {
                    menuOption = "  Exit".PadRight(18, ' ');
                }
                Console.WriteLine(menuOption);
            }

            ResetColor();
        }

        public static (int, int) Run(int choice)
        {
            ConsoleKey keyPressed;

            do
            {
                Clear();
                StartMenu(choice);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.DownArrow)
                {
                    option++;

                    if (option > produkter.Count)
                    {
                        option = 0;
                    }
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    option--;

                    if (option < 0)
                    {
                        option = produkter.Count;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            if (option == produkter.Count) return (0, 0);
            return (option, produkter[option].NamnID);
        }
    }
}
