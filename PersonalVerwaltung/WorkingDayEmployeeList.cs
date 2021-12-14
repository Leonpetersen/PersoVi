using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class WorkingDayEmployeeList : List<WorkingDayEmployee>
    {
        public DbConnector dbConnector = new DbConnector();
        public WorkingDayEmployeeList()
        {
            //Refresh();
        }

        

        public void Refresh(Kalenderwoche kalenderwoche, int employeenr)
        {
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {
                this.Clear();

                query = "SELECT personalnr, beginn, ende, arbeitszeit FROM zeiterfassung WHERE beginn >= @beg AND ende <= @end AND personalnr = @pnr";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", kalenderwoche.Startdatum);
                query_cmd.Parameters.AddWithValue("@end", kalenderwoche.Enddatum);
                query_cmd.Parameters.AddWithValue("@pnr", employeenr);

                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                DateTime? start = null; 
                DateTime? end = null;
                string date = string.Empty;
                decimal workingTime = decimal.Zero;
                double breakTime = 0;
                int rowCount = 1;

                while (reader.Read())
                {
                    //Prüfe ob das gelesene Startdatum dem des vorherigen Datensatzes entspricht
                    DateTime startNew = (DateTime)reader["beginn"];
                    if (start != null && start.Value.Date == startNew.Date)
                    {
                        //Pausenzeit berechnen -> Beginn des aktuellen Datensatzes - Ende des letzten Datensatzes
                        breakTime = ((DateTime)reader["beginn"] - (DateTime)end).TotalMinutes;

                        //Arbeitszeit summieren
                        workingTime = workingTime + (decimal)reader["arbeitszeit"];

                        end = (DateTime)reader["ende"];

                        continue;
                    }
                    else if (rowCount!= 1) //Vorherigen Datensatz abschließen (nur wenn es sich nicht um die erste Zeile handelt)
                    {
                        
                        if (start.Value.Date == end.Value.Date)
                        {
                            date = start.Value.Date.ToString("d");
                        }
                        else
                        {
                            date = start.Value.Date.ToString("d") + " - " + end.Value.Date.ToString("d");
                        }

                        WorkingDayEmployee workingDayEmployee = new WorkingDayEmployee()
                        {
                            Date = date,
                            Starttime = start.Value.ToString("t") + " Uhr",
                            Endtime = end.Value.ToString("t") + " Uhr",
                            Breaktime = breakTime.ToString() + " Min.",
                            Totalhours = workingTime.ToString() + " Std.",
                        };

                        this.Add(workingDayEmployee);

                        //Variablen zurücksetzen
                        breakTime = 0;
                    }

                        start = (DateTime)reader["beginn"];
                        end = (DateTime)reader["ende"];
                        workingTime = (decimal)reader["arbeitszeit"];
   
                    rowCount++;
                }

                //Letzten Datensatz anhängen
                if (start.Value.Date == end.Value.Date)
                {
                    date = start.Value.Date.ToString("d");
                }
                else
                {
                    date = start.Value.Date.ToString("d") + " - " + end.Value.Date.ToString("d");
                }

                WorkingDayEmployee workingDayEmployeeLast = new WorkingDayEmployee()
                {
                    Date = date,
                    Starttime = start.Value.ToString("t") + " Uhr",
                    Endtime = end.Value.ToString("t") + " Uhr",
                    Breaktime = breakTime.ToString() + " Min.",
                    Totalhours = workingTime.ToString() + " Std.",
                };

                this.Add(workingDayEmployeeLast);

                dbConnector.dbConn.Close();
            }
        }
    }
}
