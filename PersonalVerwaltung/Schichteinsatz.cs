using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Schichteinsatz
    {
        public int Schichtnr { get; set; }
        public int Personalnr { get; set; }
        public string Name { get; set; }
        public DateTime Beginn { get; set; }
        public DateTime Ende { get; set; }
        public string Art { get; set; }
        
    }
}
