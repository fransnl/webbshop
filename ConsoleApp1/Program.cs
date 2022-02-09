using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;

            do
            {
                choice = Menu.Run();

                if (choice < 5)
                {
                    List<Models.Produkt> productList;
                
                    Console.Clear();

                    productList = DapperDatabase.MenuChoice(choice);

                    int row = 5;

                    foreach (var item in productList)
                    {
                        Console.SetCursorPosition(20, row);
                        Console.WriteLine(item.Namn);
                        row++;
                    }

                    Console.ReadKey();
                }
                

            } while (choice != 5);
        }
    }
}
