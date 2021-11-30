using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class TimeRecord
    {
        public int Recordnr { get; set; }
        public int Employeenr { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public decimal Workingtime { get; set; }

        public bool createRecord()
        {
            DbConnector dbConnector = new DbConnector();
            string query;

            query = "INSERT INTO zeiterfassung (personalnr, beginn, ende, arbeitszeit)" +
                    "VALUES (@pnr, @beg, @end, @zeit)";

            MySqlCommand insert = new MySqlCommand(query, dbConnector.dbConn);
            insert.Parameters.AddWithValue("@pnr", this.Employeenr);
            insert.Parameters.AddWithValue("@beg", this.Begin);
            insert.Parameters.AddWithValue("@end", this.End);
            insert.Parameters.AddWithValue("@zeit", this.Workingtime);


            dbConnector.dbConn.Open();
            int status = insert.ExecuteNonQuery();
            dbConnector.dbConn.Close();

            if (status > 0) //Insert erfolgreich
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool deleteRecord()
        {
            return true;
        }

        public bool updateRecord()
        {
            return true;
        }

        public bool checkPeriod()
        {
            DbConnector dbConnector = new DbConnector();
            string count;

            //Prüfe ob Zeitraum schon auf Datenbank existiert um überschneidungen zu vermeiden
            count = "SELECT COUNT(erfassungsnr) FROM zeiterfassung WHERE personalnr = @pnr AND" +
                "((beginn < @beg AND ende > @end) " + //Beginn vor angegebener Beginnzeit, Ende nach angegebener Endzeit 
                "OR (beginn >= @beg AND beginn <= @end AND ende > @end) " + //Beginn im angebebenen Zeitraum, Ende nach angegebenen Zeitraum
                "OR (beginn < @beg AND ende > @beg AND ende <= @end) " + //Beginn vor angegebenen Zeitraum, Ende im Zeitraum 
                "OR (beginn >= @beg AND beginn < @end AND ende > @beg AND ende <= @end))"; //Beginn im angegebenen Zeitraum, ende im Zeitraum

            MySqlCommand count_cmd = new MySqlCommand(count, dbConnector.dbConn);

            count_cmd.Parameters.AddWithValue("@pnr", this.Employeenr);
            count_cmd.Parameters.AddWithValue("@beg", this.Begin);
            count_cmd.Parameters.AddWithValue("@end", this.End);

            dbConnector.dbConn.Open();
            int i = (int)(long)count_cmd.ExecuteScalar();
            dbConnector.dbConn.Close();

            if (i == 0) //Keine Überschneidungen
            {
                return true;
            }
            else //Überschneidungen
            {
                return false;
            }
        }
    }
}
