using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class FamilienstandList:List<Familienstand>
    {
        public FamilienstandList()
        {
            this.Add(new Familienstand("Ledig"));
            this.Add(new Familienstand("Verheiratet"));
            this.Add(new Familienstand("Verwitwet"));
            this.Add(new Familienstand("Geschieden"));
            this.Add(new Familienstand("Nicht bekannt"));
        }
    }
}
