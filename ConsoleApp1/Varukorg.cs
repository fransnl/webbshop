using System;
using System.Collections.Generic;
using static System.Console;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Varukorg
    {
        public static List<List<int>> varor;
        private static int option = 0;

        static Models.Varukorg varukorgen = new Models.Varukorg();
        static Models.Kund kund = new Models.Kund();
        static Models.Förnamn förnamnet = new Models.Förnamn();
        static Models.Efternamn efternamnet = new Models.Efternamn();
        static Models.Förnamn_Efternamn förnamnEfternamn = new Models.Förnamn_Efternamn();
        static Models.Kund_Förnamn kundFörnamn = new Models.Kund_Förnamn();

        static string adress;
        static int nummer;
        static string förnamn;
        static string efternamn;
        static int frakt;

        public static bool VarukorgMenu(List<List<int>> varukorg) 
        {
            varor = varukorg;
            bool hasBought = false;
            int choice = 0;

            do
            {
                if (varor.Count > 0)
                {
                    choice = Run();
                    if (choice < varor.Count)
                    {
                        varor.RemoveAt(choice);
                    }
                    else if (choice == varor.Count)
                    {
                        Console.Clear();
                        input();
                        if (adress != "" && nummer != 0 && förnamn != "" && efternamn != "" && frakt != 0)
                        {
                            kvitto(frakt);
                            hasBought = true;
                            choice = varor.Count + 1;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    SetCursorPosition(5, 15);
                    Console.WriteLine("Du har inget i varukorgen.");
                    Console.ReadKey();
                    choice = varor.Count+1;
                }


            } while (choice != varor.Count + 1);

            return hasBought;
        }

        private static void kvitto(int fraktID) 
        {
            Console.Clear();

            string namn;
            string smak;
            int storlek;
            decimal kostnad = 0;

            foreach (var item in varor)
            {
                (namn, smak, storlek) = DapperDatabase.varukorg(item[0], item[1], item[2]);
                kostnad += DapperDatabase.pris(item[0]);
                Console.WriteLine($"{namn}, {smak}, {storlek}g");
            }

            Console.WriteLine(förnamn);
            Console.WriteLine(efternamn);
            Console.WriteLine(adress);
            Console.WriteLine(nummer);

            Console.Write($"Total kostnad: {DapperDatabase.frakt(fraktID)} + {kostnad} = {DapperDatabase.frakt(fraktID) + kostnad}");

            Console.ReadKey();

            kund.Adress = adress;
            kund.Telefonnr = nummer;
            using(WebbshopDBContext db = new WebbshopDBContext()) 
            {
                db.kund.Add(kund);
                db.SaveChanges();
            }

            varukorgen.KundID = DapperDatabase.kundID();

            bool förnamnFinns = false;
            bool efternamnFinns = false;

            List<Models.Förnamn> förnamnen = DapperDatabase.Förnamnen();
            List<Models.Efternamn> efternamnen = new List<Models.Efternamn>();

            foreach (var item in förnamnen)
            {
                if (item.förnamn == förnamn) förnamnFinns = true;
            }
            foreach (var item in efternamnen)
            {
                if (item.efternamn == efternamn) efternamnFinns = true;

            }

            if (!förnamnFinns)
            {
                förnamnet.förnamn = förnamn;
                using (WebbshopDBContext db = new WebbshopDBContext())
                {
                    db.förnamn.Add(förnamnet);
                    db.SaveChanges();
                }
                if (!efternamnFinns)
                {
                    efternamnet.efternamn = efternamn;
                    using (WebbshopDBContext db = new WebbshopDBContext())
                    {
                        db.efternamn.Add(efternamnet);
                        db.SaveChanges();
                    }
                }
            }

            DapperDatabase.kund_förnamn(varukorgen.KundID, DapperDatabase.förnamn(förnamn));
            DapperDatabase.förnamn_efternamn(DapperDatabase.efternamn(efternamn),DapperDatabase.förnamn(förnamn));

            foreach (var item in varor)
            {
                varukorgen.PrisID = DapperDatabase.prisID(item[0]);
                varukorgen.FraktID = frakt;
                varukorgen.NamnID = item[0];
                varukorgen.SmakID = item[1];
                varukorgen.StorlekID = item[2];

                using (WebbshopDBContext db = new WebbshopDBContext())
                {
                    db.varukorg.Add(varukorgen);
                    db.SaveChanges();
                }
            }
        }

        private static void input() 
        {
            Console.Write("Adress: ");
            adress = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Telefon nummer: ");
            nummer = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Förnamn: ");
            förnamn = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Efternamn: ");
            efternamn = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Gratis frakt eller snabb frakt(50kr) välj 1 eller 2: ");
            frakt = int.Parse(Console.ReadLine());
            Console.WriteLine();
        }

        public static void DisplayItems() 
        {
            string smak;
            string namn;
            int storlek;

            Console.Clear();

            for (int i = 0; i < varor.Count + 2; i++)
            {
                SetCursorPosition(30, 5);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Välj en produkt om du vill ta bort den från listan, annars gå vidare");

                SetCursorPosition(50, i + 10);
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
                if (i < varor.Count)
                {
                    (namn, smak, storlek) = DapperDatabase.varukorg(varor[i][0], varor[i][1], varor[i][2]);
                    menuOption = $"  {namn}, {smak}, {storlek}g".PadRight(18, ' ');
                }
                else if (i == varor.Count)
                {
                    menuOption = "  Gå vidare".PadRight(18, ' ');
                }
                else
                {
                    menuOption = "  Gå tillbaks".PadRight(18, ' ');
                }
                Console.WriteLine(menuOption);
            }

            ResetColor();
        }

        public static int Run()
        {
            ConsoleKey keyPressed;

            adress = "";
            nummer = 0;
            förnamn = "";
            efternamn = "";
            frakt = 0;


            do
            {
                Clear();
                DisplayItems();

                SetCursorPosition(30, 20);
                Console.WriteLine("Om du lägger till mer än en produkt kraschar programmet.");
                SetCursorPosition(30, 21);
                Console.WriteLine("Om du slutfört ett köp måste programmet startas om innan nästa köp.");

                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.DownArrow)
                {
                    option++;

                    if (option > varor.Count + 1)
                    {
                        option = 0;
                    }
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    option--;

                    if (option < 0)
                    {
                        option = varor.Count + 1;
                    }
                }


            } while (keyPressed != ConsoleKey.Enter);

            return option;
        }
    }
}
