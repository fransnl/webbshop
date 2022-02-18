using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    [Keyless]
    class Förnamn_Efternamn
    {
        public int Förnamn_EfternamnID { get; set; }
        public int FörnamnID { get; set; }
        public int EfternamnID { get; set; }
    }
}
