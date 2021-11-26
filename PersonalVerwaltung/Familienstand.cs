using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class Familienstand
    {
        public string Stand { get; set; }

        public Familienstand(string stand)
        {
            this.Stand = stand;
        }
    }
}
