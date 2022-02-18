using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    [Keyless]
    class Kund_Förnamn
    {
        public int Kund_FörnamnID { get; set; }
        public int KundID { get; set; }
        public int FörnamnID { get; set; }
    }
}
