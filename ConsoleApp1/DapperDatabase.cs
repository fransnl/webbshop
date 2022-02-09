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

        // fixa smak, kunder och inventarie

        public static List<Models.Produkt> MenuChoice(int choice)
        {
            var sql = $"select namn.namn from Namn where namn.kategoriID = {choice+1}";
            var namn = new List<Models.Produkt>();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                namn = connection.Query<Models.Produkt>(sql).ToList();
            }

            return namn;
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
    }
}
