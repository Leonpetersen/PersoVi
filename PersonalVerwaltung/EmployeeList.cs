using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalVerwaltung
{
    public class EmployeeList:List<Employee>
    {
        public DbConnector dbConnector = new DbConnector();
        public EmployeeList()
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
                query = "SELECT * FROM mitarbeiter INNER JOIN orte ON mitarbeiter.plz = orte.plz INNER JOIN abteilungen ON mitarbeiter.abteilungsnr = abteilungen.abteilungsnr";
                query_cmd.CommandText = query;

                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee()
                    {
                        Employeenr = (int)reader["personalnr"],
                        Firstname = (string)reader["vorname"],
                        Lastname = (string)reader["nachname"],
                        Street = (string)reader["strasse"],
                        Zipcode = (int)reader["plz"],
                        Country = (string)reader["land"],
                        Maritalstatus = (string)reader["familienstand"],
                        Phonenumber = (string)reader["telefon"],
                        Email = (string)reader["email"],
                        Departmentnr = (int)reader["abteilungsnr"],
                        Department = (string)reader["bezeichnung"],
                        Professionnr = (int)reader["berufsnr"],
                        Entrydate = (DateTime)reader["eintritt"]
                    };

                    this.Add(employee);
                }
                dbConnector.dbConn.Close();
            }
        }
    }
}
