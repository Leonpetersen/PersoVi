using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonalVerwaltung
{
    /// <summary>
    /// Interaktionslogik für Personalplanung.xaml
    /// </summary>
    public partial class Personalplanung : UserControl
    {
        public DbConnector dbConnector = new DbConnector();
        public Personalplanung()
        {
            InitializeComponent();

            //Verfügbare Mitarbeiter ermitteln
            EmployeeList employees = new EmployeeList();
            List<Employee> employeesSorted = employees.OrderBy(o => o.Lastname).ToList();

            foreach (Employee employee in employeesSorted)
            {
                Button button = new Button();
                button.Content = employee.Firstname + " " + employee.Lastname;
                button.Name = "e" + employee.Employeenr.ToString();
                button.FontSize = 12;
                button.HorizontalContentAlignment = HorizontalAlignment.Left;
                button.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(startDragEvent);
                employeePanel.Children.Add(button);
            }

            //Planer initialisieren
            Kalenderwoche kalenderwoche = (Kalenderwoche)combo_kw.SelectedItem;
            DateTime[] wochentage = new DateTime[7];
            DateTime datum = kalenderwoche.Startdatum;

            for (int i = 0; i <= 6; i++)
            {
                wochentage[i] = datum;
                datum = datum.AddDays(1);
            }

            List<Schichteinsatz> schichteinsatzList = getShiftInformation(kalenderwoche);
            foreach (Schichteinsatz schichteinsatz in schichteinsatzList)
            {
                Button newButton = new Button();
                newButton.Name = "e" + schichteinsatz.Personalnr.ToString();
                newButton.Content = schichteinsatz.Name;
                newButton.FontSize = 12;
                newButton.HorizontalContentAlignment = HorizontalAlignment.Left;
                newButton.Click += new RoutedEventHandler(removeEmployee);

                if (schichteinsatz.Beginn.Date == wochentage[0].Date) //Montag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[1].Date) //Dienstag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[2].Date) //Mittwoch
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[3].Date) //Donnerstag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[4].Date) //Freitag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[5].Date) //Samstag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[6].Date) //Sonntag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            montagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            montagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            montagNacht.Children.Add(newButton);
                            break;
                    }
                }
                
            }
          
        }

        private void startDragEvent(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            string name = (string)button.Content;
            int employeenr = int.Parse(button.Name.TrimStart('e'));

            DataObject dataObject = new DataObject();
            dataObject.SetData("Name", name);
            dataObject.SetData("Pnr", employeenr);

            DragDrop.DoDragDrop(button, dataObject, DragDropEffects.Move);
        }

        private void handleDrop(object sender, DragEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            string name = (string)e.Data.GetData("Name");
            int employeenr = (int)e.Data.GetData("Pnr");

            Button newButton = new Button();
            newButton.Name = "e" + employeenr.ToString();
            newButton.Content = name;
            newButton.FontSize = 12;
            newButton.HorizontalContentAlignment = HorizontalAlignment.Left;
            newButton.Click += new RoutedEventHandler(removeEmployee);
            stackPanel.Children.Add(newButton);
        }

        private void removeEmployee(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Soll der/die Mitarbeiter/-in aus der Schicht entfernt werden?", "Mitarbeiter/-in entfernen", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Button button = (Button)sender;
                    StackPanel parentPanel = (StackPanel)button.Parent;
                    parentPanel.Children.Remove(button);
                    break;
                case MessageBoxResult.No:
                    break;
            }
           
        }

        private List<Schichteinsatz> getShiftInformation(Kalenderwoche kalenderwoche)
        {
            List<Schichteinsatz> schichteinsatzList = new List<Schichteinsatz>();
            string query = "";

            MySqlCommand query_cmd = new MySqlCommand(query, dbConnector.dbConn);

            if (dbConnector.dbConn != null)
            {


                query = "SELECT schichten.schichtnr, art, beginn, ende, schichteinsatz.personalnr, vorname, nachname FROM schichten INNER JOIN schichteinsatz " +
                    "ON schichten.schichtnr = schichteinsatz.schichtnr INNER JOIN mitarbeiter ON schichteinsatz.personalnr = mitarbeiter.personalnr " +
                    "WHERE schichten.beginn >= @beg AND schichten.ende <= @end";
                query_cmd.CommandText = query;
                query_cmd.Parameters.AddWithValue("@beg", kalenderwoche.Startdatum);
                query_cmd.Parameters.AddWithValue("@end", kalenderwoche.Enddatum);


                dbConnector.dbConn.Open();

                MySqlDataReader reader = query_cmd.ExecuteReader();

                while (reader.Read())
                {
                    Schichteinsatz schichteinsatz = new Schichteinsatz()
                    {
                        Schichtnr = (int)reader["schichtnr"],
                        Art = (string)reader["art"],
                        Personalnr = (int)reader["personalnr"],
                        Beginn = (DateTime)reader["beginn"],
                        Ende = (DateTime)reader["ende"],
                        Name = (string)reader["vorname"] + " " + (string)reader["nachname"]

                    };

                    schichteinsatzList.Add(schichteinsatz);
                }
                dbConnector.dbConn.Close();

            }
            return schichteinsatzList;
        }
    }
}
