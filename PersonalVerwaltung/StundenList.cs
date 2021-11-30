using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class StundenList:List<string>
    {
        public StundenList()
        {
            for (int i = 0; i <= 24; i++)
            {
                if (i < 10)
                {
                    this.Add("0" + i);
                }
                else
                {
                    this.Add(i.ToString());
                }
            }
        }
    }
}
