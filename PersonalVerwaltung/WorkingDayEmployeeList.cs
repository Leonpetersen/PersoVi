using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class WorkingDayEmployeeList : List<WorkingDayEmployee>
    {
        public WorkingDayEmployeeList()
        {
            Refresh();
        }

        public void Refresh()
        {


        }
    }
}
