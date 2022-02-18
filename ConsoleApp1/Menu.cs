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

        public static void StartMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
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


            List<Models.Produkt> produkter = new List<Models.Produkt>();
            produkter = DapperDatabase.allaProdukter();
            List<Models.Bästsäljare> bästsäljare = new List<Models.Bästsäljare>();
            bästsäljare = DapperDatabase.top3PopulärastProdukt();

            List<string> b = new List<string>();

            foreach (var item in bästsäljare)
            {
                foreach (var items in produkter)
                {
                    if (item.NamnID == items.NamnID)
                    {
                        b.Add(items.Namn);
                    }
                }
            }

            SetCursorPosition(37, 25);
            Console.WriteLine($"Bästsäljare: {b[0]} | {b[1]} | {b[2]}");

            for (int i = 0; i < kategorier.Count + 3; i++)
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
                else if (i == kategorier.Count)
                {
                    menuOption = "  Varukorg".PadRight(18, ' ');
                }
                else if (i == kategorier.Count+1)
                {
                    menuOption = "  Admin".PadRight(18, ' ');
                }
                else
                {
                    menuOption = "  Avsluta".PadRight(18, ' ');
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

                    if (option > kategorier.Count+2)
                    {
                        option = 0;
                    }
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    option--;

                    if (option < 0)
                    {
                        option = kategorier.Count+2;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            return option;
        }
    }
}
