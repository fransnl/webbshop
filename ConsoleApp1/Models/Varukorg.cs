using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1.Models
{
    
    class Varukorg
    {
        public int VarukorgID { get; set; }
        public int KundID { get; set; }
        public int SmakID { get; set; }
        public int StorlekID { get; set; }
        public int NamnID { get; set; }
        public int PrisID { get; set; }
        public int FraktID { get; set; }
    }
}
