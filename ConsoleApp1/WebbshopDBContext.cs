using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1
{
    class WebbshopDBContext : DbContext
    {
        public DbSet<Models.Varukorg> varukorg { get; set; }
        public DbSet<Models.Kund> kund { get; set; }
        public DbSet<Models.Förnamn> förnamn { get; set; }
        public DbSet<Models.Efternamn> efternamn { get; set; }
        public DbSet<Models.Förnamn_Efternamn> förnamnEfternamn { get; set; }
        public DbSet<Models.Kund_Förnamn> kundFörnamn { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer("Server=tcp:newtonwebbshop.database.windows.net,1433;Initial Catalog=DemoDB;Persist Security Info=False;User ID=josefine;Password=jerevienS93;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
