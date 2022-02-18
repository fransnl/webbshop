using System;
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ConsoleApp1
{
    class DapperDatabase
    {
        static string connString = "Server=tcp:newtonwebbshop.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=josefine;Password=jerevienS93;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //data till menyer
        public static List<Models.Produkt> MenuChoice(int choice)
        {
            var sql = @$"select namn.namn, Innehåll.Kalorier, Innehåll.Protein, Innehåll.Kolhydrater, Innehåll.Fett,
                         Pris.Pris, Styrka.Styrka, Namn.NamnID, Namn.Produktinfo
                         from Namn
                         full join Innehåll on namn.NamnID = Innehåll.NamnID
                         full join Pris on namn.NamnID = Pris.NamnID
                         full outer join Styrka on namn.NamnID = Styrka.NamnID
                         where Namn.KategoriID = {choice+1}";
            var namn = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.Query<Models.Produkt>(sql).ToList();
            }

            return namn;
        }

        #region allt med produkter
        public static List<Models.Produkt> allaProdukter()
        {
            var sql = @$"select namn.namn, Innehåll.Kalorier, Innehåll.Protein, Innehåll.Kolhydrater, Innehåll.Fett,
                         Pris.Pris, Styrka.Styrka, Namn.NamnID
                         from Namn
                         full join Innehåll on namn.NamnID = Innehåll.NamnID
                         full join Pris on namn.NamnID = Pris.NamnID
                         full outer join Styrka on namn.NamnID = Styrka.NamnID";
            var namn = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.Query<Models.Produkt>(sql).ToList();
            }

            return namn;
        }

        public static List<Models.Produkt> smaker()
        {
            var sql = @$"select * from smak";
            var namn = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.Query<Models.Produkt>(sql).ToList();
            }

            return namn;
        }

        public static decimal pris(int namnID)
        {
            var sql1 = $"SELECT Pris FROM Pris where NamnID = {namnID}";

            decimal frakt;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                frakt = decimal.Parse(connection.QuerySingle<string>(sql1));
            }

            return frakt;
        }

        public static int prisID(int namnID)
        {
            var sql1 = $"SELECT PrisID FROM Pris where NamnID = {namnID}";

            int prisID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                prisID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return prisID;
        }

        public static int kundID()
        {
            var sql1 = $"SELECT TOP 1 KundID FROM kund ORDER BY KundID DESC";

            int kundID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                kundID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return kundID;
        }

        public static int namnID(string namn)
        {
            var sql1 = $"SELECT namnID from namn where namn.namn = '{namn}'";

            int namnID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namnID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return namnID;
        }

        public static int smakID(string smak)
        {
            var sql1 = $"SELECT smakID from smak where smak.smak = '{smak}'";

            int smakID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                smakID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return smakID;
        }

        public static int storlekID(int storlek)
        {
            var sql1 = $"SELECT storlekID from storlek where storlek.storlek = {storlek}";

            int storlekID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                storlekID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return storlekID;
        }

        public static int frakt(int fraktID)
        {
            var sql1 = $"SELECT Kostnad FROM Frakt where FraktID = {fraktID}";

            int frakt;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                frakt = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return frakt;
        }

        public static string getSmak(int smakID)
        {
            var sql1 = $"SELECT Smak FROM Smak where SmakID = {smakID}";

            string smak;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                smak = connection.QuerySingle<string>(sql1);
            }

            return smak;
        }

        public static List<Models.Produkt> Smak(int choice)
        {
            var sql = $@"Select Smak, Smak.SmakID
                      from Smak
                      Inner Join Smak_Produktnamn
                      ON Smak.SmakID = Smak_Produktnamn.SmakID
                      where Smak_Produktnamn.NamnID = {choice}";
            var Smak = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                Smak = connection.Query<Models.Produkt>(sql).ToList();
            }

            return Smak;
        }

        public static List<Models.Produkt> Storlek(int choice)
        {
            var sql = $@"Select Storlek, Storlek.StorlekID
                      from Storlek
                      Inner Join Storlek_Produktnamn
                      ON Storlek.StorlekID = Storlek_Produktnamn.StorlekID
                      where Storlek_Produktnamn.NamnID = {choice}";
            var Smak = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                Smak = connection.Query<Models.Produkt>(sql).ToList();
            }

            return Smak;
        }
        #endregion

        #region allt med kunder
        public static void kund_förnamn(int KundID, int FörnamnID)
        {
            var sql = @$"insert into Kund_Förnamn (KundID, FörnamnID) 
                        values ({KundID}, {FörnamnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static void förnamn_efternamn(int EfternamnID, int FörnamnID)
        {
            var sql = @$"insert into Förnamn_Efternamn (FörnamnID, EfternamnID) 
                        values ({FörnamnID}, {EfternamnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }
        
        public static List<Models.Förnamn> Förnamnen()
        {
            var sql = @$"Select * from Förnamn";
            var förnamnen = new List<Models.Förnamn>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                förnamnen = connection.Query<Models.Förnamn>(sql).ToList();
            }

            return förnamnen;
        }

        public static int förnamn(string förnamn)
        {
            var sql1 = $"SELECT FörnamnID FROM Förnamn where Förnamn = '{förnamn}'";

            int förnamnID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                förnamnID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return förnamnID;
        }

        public static int efternamn(string efternamn)
        {
            var sql1 = $"SELECT EfternamnID FROM Efternamn where Efternamn = '{efternamn}'";

            int efterNamnID;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                efterNamnID = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return efterNamnID;
        }
        #endregion

        //varukorg
        public static (string, string, int) varukorg(int namnID, int smakID, int storlekID) 
        {
            var sql1 = $"SELECT Namn FROM Namn where NamnID = {namnID}";
            var sql2 = $"SELECT Smak FROM Smak where SmakID = {smakID}";
            var sql3 = $"SELECT Storlek FROM Storlek where StorlekID = {storlekID}";

            string namn;
            string smak;
            int storlek;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.QuerySingle<string>(sql1);
                smak = connection.QuerySingle<string>(sql2);
                storlek = int.Parse(connection.QuerySingle<string>(sql3));
            }

            return (namn, smak, storlek);
        }

        #region lägg till, ta bort produkt
        public static void LäggTillProduktnamn(string Namn, int KategoriID, int LeverantörID, string Produktinfo)
        {
            var sql = @$"insert into Namn (Namn, KategoriID, LeverantörID, Produktinfo) 
                        values ('{Namn}', {KategoriID}, {LeverantörID}, '{Produktinfo}')";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static void LäggTillInnehåll(int kalorier, float kolhydrater, float fett, float protein, int NamnID)
        {
            var sql = @$"insert into Innehåll (Kalorier, Kolhydrater, Fett, Protein, NamnID) 
                        values ({kalorier}, {kolhydrater}, {fett}, {protein}, {NamnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static void LäggTillSmak(string smak)
        {
            var sql = @$"insert into smak (smak) 
                        values ('{smak}')";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        public static void LäggTillSmak_Produktnamn(int smakID, int namnID)
        {
            var sql = @$"insert into smak_produktnamn (smakID, namnID) 
                        values ({smakID}, {namnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        public static void LäggTillStorlek(int storlek)
        {
            var sql = @$"insert into storlek (storlek) 
                        values ({storlek})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        public static void LäggTillInventarie(int inventarie, int namnID)
        {
            var sql = @$"insert into inventarie (inventarie, namnID) 
                        values ({inventarie}, {namnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        public static void LäggTillStorlek_Produktnamn(int storlekID, int namnID)
        {
            var sql = @$"insert into storlek_produktnamn (storlekID, namnID) 
                        values ({storlekID}, {namnID})";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }
        }

        
        public static void TaBortProdukt(int NamnID)
        {
            var sql = $"Delete from inventarie where namnID = {NamnID}";
            var sq2 = $"Delete from innehåll where namnID = {NamnID}";
            var sq3 = $"Delete from storlek_produktnamn where namnID = {NamnID}";
            var sq4 = $"Delete from smak_produktnamn where namnID = {NamnID}";
            var sq5 = $"Delete from namn where namnID = {NamnID}";

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
                connection.Execute(sq2);
                connection.Execute(sq3);
                connection.Execute(sq4);
                connection.Execute(sq5);

            }

        }

        public static void UppdateraProduktNamn(int namnID, string namn)
        {
            var sql = @$"update Namn 
                        set namn = '{namn}')
                        where NamnID = {namnID}";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }
        #endregion

        #region lägg till, ändra, ta bort och select kategorier
        public static void LäggTillKategori(string kategoriNamn) 
        {
            var sql = @$"insert into kategorier (kategori) 
                        values ('{kategoriNamn}')";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static void UppdateraKategori(int kategoriID, string kategoriNamn)
        {
            var sql = @$"update kategorier 
                        set kategori = '{kategoriNamn}')
                        where kategoriID = {kategoriID}";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static void TaBortKategori(int kategoriID)
        {
            var sql = $"Delete from kategorier where kategoriID = {kategoriID}";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static List<Models.Kategorier> Kategorier() 
        {
            var sql = "select * from kategorier";
            var kategorier = new List<Models.Kategorier>();
            using(var connection = new SqlConnection(connString)) 
            {
                connection.Open();
                kategorier = connection.Query<Models.Kategorier>(sql).ToList();
            } 

            return kategorier;
        }
        #endregion

        #region Leverantör
        public static List<Models.Leverantör> Leverantörer()
        {
            var sql = "select * from leverantör";
            var Leverantörer = new List<Models.Leverantör>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                Leverantörer = connection.Query<Models.Leverantör>(sql).ToList();
            }

            return Leverantörer;
        }
        #endregion

        #region ändra lager
        public static void UppdateraInventarie(int namnID, int inventarie)
        {
            var sql = @$"update Inventarie 
                        set inventarie = {inventarie})
                        where NamnID = {namnID}";
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                connection.Execute(sql);
            }

        }

        public static int Inventarie(int NamnID)
        {
            var sql1 = @$"select inventarie.inventarie)
                       From inventarie
                       where namnID = {NamnID}";

            int inventarie;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                inventarie = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return inventarie;
        }

        #endregion

        #region queries
        public static int lagersaldo()
        {
            var sql1 = @$"select sum(pris.pris * inventarie.inventarie)
                       From Namn
                       inner join pris on namn.NamnID = Pris.NamnID
                       inner join inventarie on namn.NamnID = Inventarie.NamnID";

            int lagersaldo;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                lagersaldo = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return lagersaldo;
        }

        public static int SumKalorier()
        {
            var sql1 = @$"SELECT
                          SUM(Kalorier) as 'Antal kalorier totalt'
                          FROM 
                          Innehåll";

            int sumKalorier;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                sumKalorier = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return sumKalorier;
        }

        public static int AVGKalorier()
        {
            var sql1 = @$"SELECT
                          AVG(Kalorier) as 'Antal kalorier totalt'
                          FROM 
                          Innehåll";

            int avgKalorier;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                avgKalorier = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return avgKalorier;
        }

        public static int totSmaker()
        {
            var sql1 = @$"SELECT COUNT(Smak) as 'Totalt antal smaker'
                          FROM Smak";

            int totSmak;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                totSmak = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return totSmak;
        }

        public static int antalSmak(int namnID)
        {
            var sql1 = @$"SELECT COUNT(Smak) as 'Antal smaker per produkt'
                          FROM Smak
                          FULL JOIN Smak_Produktnamn ON Smak.SmakID = Smak_Produktnamn.SmakID
                          WHERE NamnID = {namnID}";

            int antal;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                antal = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return antal;
        }

        public static int maxPris()
        {
            var sql1 = @$"SELECT MAX(Pris) as 'Högsta pris'
                          FROM Pris";

            int max;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                max = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return max;
        }

        public static int minPris()
        {
            var sql1 = @$"SELECT MIN(Pris) as 'Högsta pris'
                          FROM Pris";

            int min;

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                min = int.Parse(connection.QuerySingle<string>(sql1));
            }

            return min;
        }

        public static List<Models.Bästsäljare> top3PopulärastProdukt()
        {
            var sql1 = @$"SELECT TOP(3) NamnID,
                          COUNT(NamnID) AS 'Bästsäljare'
                          FROM Varukorg
                          GROUP BY NamnID 
                          ORDER BY 'Bästsäljare' DESC";

            var bästsäljare = new List<Models.Bästsäljare>(); 

            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                bästsäljare = connection.Query<Models.Bästsäljare>(sql1).ToList();
            }

            return bästsäljare;
        }

        #endregion
    }
}
