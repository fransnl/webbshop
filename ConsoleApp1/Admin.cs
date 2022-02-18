using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Admin
    {
        public static void run() 
        {
            int choice = 4;
            do
            {
                Console.Clear();
                Console.WriteLine("Om något ändras, tas bort eller läggs till behöver programmet startas om.");
                Console.WriteLine("1. Statistik");
                Console.WriteLine("2. Kategorier");
                Console.WriteLine("3. Produkter");
                Console.WriteLine("4. Gå tillbaks");
                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();
                    statistik();
                }
                else if (choice == 2)
                {
                    Console.Clear();
                    ändraKategorier();
                }
                else if(choice == 3)
                {
                    Console.Clear();
                    ändraProdukter();
                }

            } while (choice != 4);
        }

        private static void ändraKategorier() 
        {
            Console.Clear();
            List<Models.Kategorier> kategorier = new List<Models.Kategorier>();

            kategorier = DapperDatabase.Kategorier();

            int choice = 4;

            do
            {
                Console.Clear();
                Console.WriteLine("KATEGORIER");
                Console.WriteLine("1. Lägg till");
                Console.WriteLine("2. Ändra");
                Console.WriteLine("3. Ta bort");
                Console.WriteLine("4. Gå tillbaks");

                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();

                    Console.Write("Välj kategorinamn: ");
                    string kategorinamn = Console.ReadLine();
                    DapperDatabase.LäggTillKategori(kategorinamn);
                    Console.Clear();
                    foreach (var item in kategorier)
                    {
                        Console.WriteLine(item.Kategori);
                    }
                    Console.ReadKey();
                }
                else if (choice == 2)
                {
                    Console.Clear();

                    foreach (var item in kategorier)
                    {
                        Console.WriteLine(item.KategoriID + ". " + item.Kategori);
                    }
                    Console.WriteLine("Välj vilken att ändra");
                    int rchoice = int.Parse(Console.ReadLine());
                    Console.WriteLine("Välj nytt namn");
                    string nyNamn = Console.ReadLine();

                    DapperDatabase.UppdateraKategori(rchoice, nyNamn);
                }
                else if (choice == 3)
                {
                    Console.Clear();

                    foreach (var item in kategorier)
                    {
                        Console.WriteLine(item.KategoriID + ". " + item.Kategori);
                    }
                    Console.WriteLine("Välj vilken att ta bort");
                    int rchoice = int.Parse(Console.ReadLine());

                    DapperDatabase.TaBortKategori(rchoice);

                    Console.ReadKey();
                }

            } while (choice != 4);
        }

        private static void ändraProdukter()
        {
            Console.Clear();
            List<Models.Kategorier> kategorier = new List<Models.Kategorier>();
            List<Models.Leverantör> leverantörer = new List<Models.Leverantör>();
            List<Models.Produkt> smaker = new List<Models.Produkt>();
            List<Models.Produkt> produkter = new List<Models.Produkt>();
            produkter = DapperDatabase.allaProdukter();
            smaker = DapperDatabase.smaker();
            kategorier = DapperDatabase.Kategorier();
            leverantörer = DapperDatabase.Leverantörer();


            int choice = 4;

            do
            {
                Console.Clear();
                Console.WriteLine("PRODUKTER");
                Console.WriteLine("1. Lägg till");
                Console.WriteLine("2. Ändra");
                Console.WriteLine("3. Ta bort");
                Console.WriteLine("4. Gå tillbaks");

                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Clear();
                    foreach (var item in kategorier)
                    {
                        Console.WriteLine($"{item.KategoriID}: {item.Kategori}");
                    }
                    Console.WriteLine("Välj kategori nr:");
                    string kategori = Console.ReadLine();
                    
                    Console.Clear();
                    foreach (var item in leverantörer)
                    {
                        Console.WriteLine($"{item.leverantörID}: {item.leverantör}");
                    }
                    Console.WriteLine("Välj leverantör nr:");
                    string leverantör = Console.ReadLine();
                    
                    Console.Clear();

                    Console.Write("Välj produktnamn: ");
                    string produktnamn = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Välj pris: ");
                    string pris = Console.ReadLine();
                    Console.Write("Välj mängd i inventariet: ");
                    string inventarie = Console.ReadLine();
                    Console.WriteLine("Välj beskrivning: ");
                    string beskrivning = Console.ReadLine();
                    Console.WriteLine("Välj storlek: ");
                    string storlek = Console.ReadLine();
                    Console.WriteLine("Välj kalorier, protein, kolhydrater, fett: ");
                    string kalorier = Console.ReadLine();
                    string protein = Console.ReadLine();
                    string kolhydrater = Console.ReadLine();
                    string fett = Console.ReadLine();
                    Console.WriteLine("Ny smak eller existerande smak? (Välj 1 eller 2)");

                    string smak;

                    int nysmak = int.Parse(Console.ReadLine());
                    if (nysmak == 1)
                    {
                        Console.WriteLine("Välj smak: ");
                        smak = Console.ReadLine();
                    }
                    else
                    {
                        Console.Clear();
                        foreach (var item in smaker)
                        {
                            Console.WriteLine($"{item.SmakID}. {item.Smak}");
                        }

                        Console.WriteLine("Välj smak nr:");
                        int smakChoice = int.Parse(Console.ReadLine());

                        smak = DapperDatabase.getSmak(smakChoice);
                    }

                    if (produktnamn != "" && pris != "" && beskrivning != "" && storlek != "" && 
                        smak != "" && kalorier != "" && kolhydrater != "" && fett != "" && inventarie != "")
                    {
                        decimal.Parse(pris);
                        int.Parse(storlek);
                        int.Parse(kalorier);
                        float.Parse(protein);
                        float.Parse(kolhydrater);
                        float.Parse(fett);
                        int.Parse(inventarie);

                        DapperDatabase.LäggTillProduktnamn(produktnamn, int.Parse(kategori), int.Parse(leverantör), beskrivning);
                        int namnID = DapperDatabase.namnID(produktnamn);
                        DapperDatabase.LäggTillInventarie(int.Parse(inventarie), namnID);
                        DapperDatabase.LäggTillInnehåll(int.Parse(kalorier), float.Parse(kolhydrater), float.Parse(fett), float.Parse(protein), namnID);
                        if (nysmak == 1)
                        {
                            DapperDatabase.LäggTillSmak(smak);
                        }
                        DapperDatabase.LäggTillStorlek(int.Parse(storlek));
                        int smakID = DapperDatabase.smakID(smak);
                        int storlekID = DapperDatabase.storlekID(int.Parse(storlek));
                        DapperDatabase.LäggTillSmak_Produktnamn(smakID, namnID);
                        DapperDatabase.LäggTillStorlek_Produktnamn(storlekID, namnID);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Något blev ej ifyllt!");
                        Console.ReadKey();
                    }

                    Console.Clear();
                }
                if (choice == 2)
                {
                    Console.Clear();
                    Console.WriteLine("1. Ändra produktnamn");
                    Console.WriteLine("2. Ändra inventarie");

                    int rchoice = int.Parse(Console.ReadLine());

                    if (rchoice == 1)
                    {
                        Console.Clear();

                        foreach (var item in produkter)
                        {
                            Console.WriteLine($"{item.NamnID}. {item.Namn}");
                        }

                        Console.WriteLine("Välj produkt nr");
                        int namnID = int.Parse(Console.ReadLine());
                        Console.WriteLine("Välj nytt namn");
                        string namn = Console.ReadLine();

                        DapperDatabase.UppdateraProduktNamn(namnID, namn);

                    }
                    if (rchoice == 2)
                    {
                        Console.Clear();

                        foreach (var item in produkter)
                        {
                            Console.WriteLine($"{item.NamnID}. {item.Namn} {DapperDatabase.Inventarie(item.NamnID)}");
                        }

                        Console.WriteLine("Välj produkt nr");
                        int namnID = int.Parse(Console.ReadLine());

                        Console.WriteLine("Välj ny inventarie mängd");
                        int inventarie = int.Parse(Console.ReadLine());

                        DapperDatabase.UppdateraInventarie(namnID, inventarie);
                    }

                }
                if (choice == 3)
                {
                    Console.Clear();
                    foreach (var item in produkter)
                    {
                        Console.WriteLine($"{item.NamnID}. {item.Namn}");
                    }
                    int productChoice = int.Parse(Console.ReadLine());

                    DapperDatabase.TaBortProdukt(productChoice);
                    Console.ReadKey();
                    Console.Clear();
                }


            } while (choice != 4);
        }

        private static void statistik() 
        {
            bästsäljare();
            lagersaldo();
            Kalorier();
            totSmak();
            smakPProdukt();
            minMaxPris();
        }

        //queries
        private static void lagersaldo() 
        {
            Console.Clear();

            List<Models.Produkt> produkter = new List<Models.Produkt>();

            produkter = DapperDatabase.allaProdukter();

            foreach (var item in produkter)
            {
                Console.WriteLine($"{item.Namn}, {item.Pris}kr, {item.Inventarie}st");
            }

            int lagersaldo = DapperDatabase.lagersaldo();

            Console.WriteLine();
            Console.WriteLine($"Totalt lagersaldo: {lagersaldo}kr");

            Console.ReadKey();
        }

        private static void Kalorier() 
        {
            Console.Clear();

            int avgKalorier = DapperDatabase.AVGKalorier();
            int sumKalorier = DapperDatabase.SumKalorier();

            Console.WriteLine($"medel kalorier i produkter {avgKalorier}");
            Console.WriteLine($"summa kalorier av alla produkter {sumKalorier}");
            Console.ReadKey();
        }

        private static void totSmak()
        {
            Console.Clear();

            int totsmak = DapperDatabase.totSmaker();

            Console.WriteLine($"totalt antal smaker {totsmak}");
            Console.ReadKey();
        }

        private static void smakPProdukt()
        {
            Console.Clear();

            List<Models.Produkt> produkter = new List<Models.Produkt>();
            produkter = DapperDatabase.allaProdukter();

            Console.WriteLine($"Antal smaker per produkt");
            foreach (var item in produkter)
            {
                Console.WriteLine($"{item.Namn}: {DapperDatabase.antalSmak(item.NamnID)}");
            }

            Console.ReadKey();
        }

        private static void minMaxPris()
        {
            Console.Clear();

            int min = DapperDatabase.minPris();
            int max = DapperDatabase.maxPris();

            Console.WriteLine($"Billigaste produkten kostar {min}kr");
            Console.WriteLine($"Dyraste produkten kostar {max}kr");
            Console.ReadKey();
        }

        private static void bästsäljare()
        {
            Console.Clear();

            List<Models.Produkt> produkter = new List<Models.Produkt>();
            produkter = DapperDatabase.allaProdukter();
            List<Models.Bästsäljare> bästsäljare = new List<Models.Bästsäljare>();
            bästsäljare = DapperDatabase.top3PopulärastProdukt();


            Console.WriteLine($"Bäst säljande produkter");
            foreach (var item in bästsäljare)
            {
                foreach (var items in produkter)
                {
                    if (item.NamnID == items.NamnID)
                    {
                        Console.WriteLine($"{items.Namn}: {item.bästsäljare}st");
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
