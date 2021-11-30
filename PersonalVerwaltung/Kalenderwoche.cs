using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Kalenderwoche
    {
        public int Wochennr { get; set; }
        public string Wochenbezeichnung { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }
    }
}
