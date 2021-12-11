using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class WorkingTimeEmployeeList : List<WorkingTimeEmployee>
    {
        public DbConnector dbConnector = new DbConnector();
        public WorkingTimeEmployeeList()
        {
            Refresh();
        }
        public void Refresh()
        {
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                //Hole Daten zu aktueller Kalenderwoche
                Kalenderwoche kalenderwoche = Hilfsmittel.getCurrentCalendarWeek();

                query = "SELECT zeiterfassung.personalnr, vorname, nachname, SUM(arbeitszeit) AS summearbeitszeit FROM zeiterfassung INNER JOIN mitarbeiter ON zeiterfassung.personalnr = mitarbeiter.personalnr " +
                    "WHERE beginn >= @beg AND ende <= @end GROUP BY zeiterfassung.personalnr ORDER BY zeiterfassung.personalnr ASC";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", kalenderwoche.Startdatum);
                query_cmd.Parameters.AddWithValue("@end", kalenderwoche.Enddatum);


                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                while (reader.Read())
                {
                    WorkingTimeEmployee workingTimeEmployee = new WorkingTimeEmployee()
                    {
                        Employeenr = (int)reader["personalnr"],
                        Firstname = (string)reader["vorname"],
                        Lastname = (string)reader["nachname"],
                        TotalWorkingTime = (decimal)reader["summearbeitszeit"]
                    };

                    this.Add(workingTimeEmployee);
                }
                dbConnector.dbConn.Close();
            }
        }

        public void Refresh(Kalenderwoche kalenderwoche)
        {
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                query = "SELECT zeiterfassung.personalnr, vorname, nachname, SUM(arbeitszeit) AS summearbeitszeit FROM zeiterfassung INNER JOIN mitarbeiter ON zeiterfassung.personalnr = mitarbeiter.personalnr " +
                    "WHERE beginn >= @beg AND ende <= @end GROUP BY zeiterfassung.personalnr ORDER BY zeiterfassung.personalnr ASC";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", kalenderwoche.Startdatum);
                query_cmd.Parameters.AddWithValue("@end", kalenderwoche.Enddatum);


                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                while (reader.Read())
                {
                    WorkingTimeEmployee workingTimeEmployee = new WorkingTimeEmployee()
                    {
                        Employeenr = (int)reader["personalnr"],
                        Firstname = (string)reader["vorname"],
                        Lastname = (string)reader["nachname"],
                        TotalWorkingTime = (decimal)reader["summearbeitszeit"]
                    };

                    this.Add(workingTimeEmployee);
                }
                dbConnector.dbConn.Close();
            }

        }
    }
}
