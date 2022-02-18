using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        //queries
        //kommentera

        /*
         * Grupp: Frans Nilsson Lidström, Josefine Sjögren
         * 
         * Github: https://github.com/fransnl/webbshop
         * 
         * I Dapperdatabase.cs finns vår connection till vår sql databas samt alla sql queries som används genom dapper.
         * I WebbshoppDBcontext.cs har vi vår dbcontext till entity framework
         * 
         */

        static void Main(string[] args)
        {
            int choice;
            int productChoice;
            
            int namnID;
            int smakID;
            int storlekID = 0;
            int kundID = 0;

            bool hasBought;

            List<List<int>> varukorg = new List<List<int>>();
            List<Models.Kategorier> kategorier = new List<Models.Kategorier>();
            kategorier = DapperDatabase.Kategorier();

            do
            {
                choice = Menu.Run();

                if (choice < kategorier.Count)
                {
                    Console.Clear();

                    (productChoice, namnID) = ProductMenu.Run(choice);

                    Console.Clear();

                    if (namnID != 0)
                    {
                        smakID = SmakMenu.Run(choice, productChoice, namnID);

                        if (smakID != 0)
                        {
                            storlekID = StorlekMenu.Run(choice, productChoice, namnID);


                            if (storlekID != 0)
                            {
                                List<int> produkt = new List<int>();

                                produkt.Add(namnID);
                                produkt.Add(smakID);
                                produkt.Add(storlekID);
                                produkt.Add(kundID);
                                
                                varukorg.Add(produkt);
                            }

                        }
                    }

                }
                if (choice == kategorier.Count)
                {
                    hasBought = Varukorg.VarukorgMenu(varukorg);
                    if (hasBought == true)
                    {
                        varukorg.Clear();
                    }
                }
                if (choice == kategorier.Count+1)
                {
                    Admin.run();
                }


            } while (choice != kategorier.Count+2);
        }
    }
}
