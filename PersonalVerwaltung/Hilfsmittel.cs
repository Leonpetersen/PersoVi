using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public static class Hilfsmittel
    {

        public static bool isValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static Kalenderwoche getCurrentCalendarWeek()
        {
            Kalenderwoche kalenderwoche;
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

            //Uhrzeit aus DateTimes entfernen 
            TimeSpan time = new TimeSpan(0, 0, 0);
            monday = monday.Date + time;
            sunday = sunday.Date + time;

            return kalenderwoche = new Kalenderwoche()
            {
                Wochennr = currentWeek,
                Wochenbezeichnung = "KW " + currentWeek + " " + DateTime.Now.Year,
                Startdatum = monday,
                Enddatum = sunday

            };
        }

        private static int GetDayCountFromMonday(DayOfWeek day)
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

        public static string getTotalHoursWeek(Kalenderwoche kalenderwoche, int employeenr)
        {
            DbConnector dbConnector = new DbConnector();
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {

                query = "SELECT SUM(arbeitszeit) AS summe FROM zeiterfassung WHERE beginn >= @beg AND ende <= @end AND personalnr = @pnr";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", kalenderwoche.Startdatum);
                query_cmd.Parameters.AddWithValue("@end", kalenderwoche.Enddatum);
                query_cmd.Parameters.AddWithValue("@pnr", employeenr);

                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        return reader["summe"].ToString() + " Std.";
                    }
                }
                else
                {
                    return "0 Stunden";
                }
      

                dbConnector.dbConn.Close();
            }
            return "0 Stunden";

        }
    }
}
