using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    class Produkt
    {
        //Namn
        public string Namn { get; set; }
        public int NamnID { get; set; }

        public string Produktinfo { get; set; }

        //Innehåll
        public int Kalorier { get; set; }
        public float Kolhydrater { get; set; }
        public float Fett { get; set; }
        public float Protein { get; set; }

        //Styrka
        public int Styrka { get; set; }
        //Pris
        public decimal Pris { get; set; }

        //Inventarie
        public int Inventarie { get; set; }

        //Smak
        public string Smak { get; set; }
        public int SmakID { get; set; }

        //Storlek
        public int Storlek { get; set; }
        public int StorlekID { get; set; }
    }
}
