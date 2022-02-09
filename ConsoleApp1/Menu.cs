using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Menu
    {
        private static int option = 0;
        private static List<Models.Kategorier> kategorier = DapperDatabase.Kategorier();

        private static void StartMenu() 
        {

            Console.WriteLine(@"
                   ,▄▄,,
                ,ⁿ   '  `  ',
                '   ,  ▐ t ▄'▄
               ╜  ▐N▄█ ▐ ▐ ▐▄▀                                                       
              ▀  ┌'▐  ''▀▀▀▀                                                     
            ,▀   ' █                                      
           ▄▀      █                                     
          ▄▀       ▐▌             ,▄▄▄▄▄,,    ,▄  ______       _ _       
         ,▌         █  ,▄▄▄,  ▄█▀▀      `ⁿ▀▀▀▀    | ___ \     | | |      
         █          █▀      '▀▀▄                  | |_/ /_   _| | | ____ _ _ __ _ __   __ _ ___                       
         ▌          █            *¡               | ___ \ | | | | |/ / _` | '__| '_ \ / _` / __|      
        ¬▌         `            ▄─ ▄              | |_/ / |_| | |   < (_| | |  | | | | (_| \__ \
         █            ⁿ∞æ▄▄▄A▀▀▀`'  ▀█▄           \____/ \__,_|_|_|\_\__,_|_|  |_| |_|\__,_|___/                                 
         ▀█∞═^                    █    ▀█▄                           
            ▀ⁿ∞▄▄▄▄█           ,▄███    ███                      
                   ▀▀▀███████▀▀▀   ▀█▄   ███      ___  ____   _     _                
                                     ▀█▄  ████,   |  \/  (_) (_)   | |               
                                       ▀█▄██▌ ▀▀` | .  . | __ _ ___| |_ __ _ _ __ ___      
                                         ▀███     | |\/| |/ _` / __| __/ _` | '__/ _ \
                                           '▀▌    | |  | | (_| \__ \ || (_| | | |  __/
                                              ╕   \_|  |_/\__,_|___/\__\__,_|_|  \___|");




            for (int i = 0; i < kategorier.Count+1; i++)
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
                if (i < kategorier.Count)
                {
                    menuOption = $"  {kategorier[i].Kategori}".PadRight(18, ' ');
                }
                else
                {
                    menuOption = "  Exit".PadRight(18, ' ');
                }
                Console.WriteLine(menuOption);
            }

            ResetColor();
        }

        public static int Run() 
        {
            ConsoleKey keyPressed;

            do
            {
                Clear();
                StartMenu();

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.DownArrow)
                {
                    option++;

                    if (option > kategorier.Count)
                    {
                        option = 0;
                    }
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    option--;

                    if (option < 0)
                    {
                        option = kategorier.Count;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            return option;
        }
    }
}
