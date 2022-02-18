using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class StorlekMenu
    {
        private static List<Models.Produkt> produkterStorlek;
        private static int option = 0;

        public static void StartMenu(int choice)
        {
            produkterStorlek = DapperDatabase.Storlek(choice);

            SetCursorPosition(15, 10);
            Console.WriteLine(produkterStorlek[0].Produktinfo);

            for (int i = 0; i < produkterStorlek.Count + 1; i++)
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
                if (i < produkterStorlek.Count)
                {
                    menuOption = $"  {produkterStorlek[i].Storlek}g".PadRight(18, ' ');
                }
                else
                {
                    menuOption = "  Gå tillbaks".PadRight(18, ' ');
                }
                Console.WriteLine(menuOption);
            }

            ResetColor();
        }

        public static int Run(int choice, int productChoice, int namnID)
        {
            ConsoleKey keyPressed;

            List<Models.Produkt> produkter = DapperDatabase.MenuChoice(choice);

            string info = SpliceText(produkter[productChoice].Produktinfo);

            do
            {
                Clear();

                SetCursorPosition(0, 5);
                Console.WriteLine(info);
                SetCursorPosition(43, 23);
                Console.WriteLine($"{produkter[productChoice].Namn} Pris: {produkter[productChoice].Pris} kr I lager: {produkter[productChoice].Inventarie}");
                SetCursorPosition(43, 22);
                Console.WriteLine($"Kalorier: {produkter[productChoice].Kalorier} Protein: {produkter[productChoice].Protein} Kolhydrater: {produkter[productChoice].Kolhydrater} Fett: {produkter[productChoice].Fett} Koffeinhalt: {produkter[productChoice].Styrka}");

                StartMenu(namnID);

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.DownArrow)
                {
                    option++;

                    if (option > produkterStorlek.Count)
                    {
                        option = 0;
                    }
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    option--;

                    if (option < 0)
                    {
                        option = produkterStorlek.Count;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            if (option == produkterStorlek.Count) return 0;
            return produkterStorlek[option].StorlekID;
        }

        public static string SpliceText(string text)
        {
            var charCount = 0;
            var lines = text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                            .GroupBy(w => (charCount += w.Length + 1) / 40)
                            .Select(g => string.Join(" ", g));

            return String.Join("\n", lines.ToArray());
        }
    }
}
