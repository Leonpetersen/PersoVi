﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class KalenderwocheList:List<Kalenderwoche>
    {
        public KalenderwocheList()
        {
            Kalenderwoche kalenderwoche; DateTime monday; DateTime sunday;
            kalenderwoche = Hilfsmittel.getCurrentCalendarWeek();
            int currentWeek = kalenderwoche.Wochennr;
            monday = kalenderwoche.Startdatum;
            sunday = kalenderwoche.Enddatum;
            this.Add(kalenderwoche);

            /**
            DateTime monday; DateTime sunday;
            int currentWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;

            //Ermittle Montag und Sonntag der aktuellen Woche
            if (currentDay != DayOfWeek.Monday)
            {
                int dayoffset = GetDayCountFromMonday(currentDay);
                monday = DateTime.Now.AddDays(-dayoffset);
                sunday = monday.AddDays(6);
            }
            else
            {
                monday = DateTime.Now;
                sunday = monday.AddDays(6);
            }

            kalenderwoche = new Kalenderwoche()
            {
                Wochennr = currentWeek,
                Wochenbezeichnung = "KW " + currentWeek + " " + DateTime.Now.Year,
                Startdatum = monday,
                Enddatum = sunday

            };
            */

            while (currentWeek != 1)
            {
                //KW Vorwoche berechnen
                currentWeek--;
                //Montag Vorwoche berechnen
                monday = monday.AddDays(-7);
                //Sonntag Vorwoche berechnen
                sunday = sunday.AddDays(-7);


                kalenderwoche = new Kalenderwoche()
                {
                    Wochennr = currentWeek,
                    Wochenbezeichnung = "KW " + currentWeek + " " + DateTime.Now.Year,
                    Startdatum = monday,
                    Enddatum = sunday
                };
                this.Add(kalenderwoche);
            }

        }

        private int GetDayCountFromMonday(DayOfWeek day)
        {
            
            switch (day)
            {
                
                case DayOfWeek.Tuesday:
                    return 1;
                case DayOfWeek.Wednesday:
                    return 2;
                case DayOfWeek.Thursday:
                    return 3;
                case DayOfWeek.Friday:
                    return 4;
                case DayOfWeek.Saturday:
                    return 5;
                case DayOfWeek.Sunday:
                    return 6;
                default:
                    return 0;
            }
        }
    }
}
