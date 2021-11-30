using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class KalenderwocheList
    {
        public KalenderwocheList()
        {
            DateTime monday;
            int currentWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;

            //Ermittle Montag und Sonntag der aktuellen Woche
            if (currentDay != DayOfWeek.Monday)
            {
                int dayoffset = GetDayCountFromMonday(currentDay);
                monday = DateTime.Now ;
            }
            else
            {
                monday = DateTime.Now;
            }
            
        }

        private int GetDayCountFromMonday(DayOfWeek day)
        {
            /*
            switch (day)
            {
                case DayOfWeek.Sunday:
                    
                    break;
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    break;
                case DayOfWeek.Wednesday:
                    break;
                case DayOfWeek.Thursday:
                    break;
                case DayOfWeek.Friday:
                    break;
                case DayOfWeek.Saturday:
                    break;
                default:
                    break;
            }
            */
            return 1;
        }
    }
}
