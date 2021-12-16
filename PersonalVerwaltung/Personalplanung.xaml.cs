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
        public DateTime[] wochentage = new DateTime[7];
        public Personalplanung()
        {
            InitializeComponent();

            //ComboBox Kalenderwoche um zwei zukünftige Wochen ergänzen
            KalenderwocheList kalenderwocheList = (KalenderwocheList)combo_kw.ItemsSource;

            Kalenderwoche aktuelleKw = (Kalenderwoche)combo_kw.SelectedItem;
            int woche = aktuelleKw.Wochennr;
            DateTime startDatum = aktuelleKw.Startdatum;
            DateTime endDatum = aktuelleKw.Enddatum;
            int aktuellesJahr = aktuelleKw.Startdatum.Year;
            for (int i = 0; i < 4; i++)
            {
                startDatum = startDatum.AddDays(7);
                endDatum = endDatum.AddDays(7);
                if (startDatum.Year == aktuellesJahr) //KW liegt im aktuellen Jahr
                {
                    woche++;
                    Kalenderwoche kalenderwocheZukunft = new Kalenderwoche()
                    {
                        Wochennr = woche,
                        Startdatum = startDatum,
                        Enddatum = endDatum,
                        Wochenbezeichnung = "KW " + woche + " " + aktuellesJahr,
                    };

                    kalenderwocheList.Add(kalenderwocheZukunft);
                }
                else //Erste KW Folgejahr
                {
                    woche = 1;
                    aktuellesJahr++;
                    Kalenderwoche kalenderwocheZukunft = new Kalenderwoche()
                    {
                        Wochennr = woche,
                        Startdatum = startDatum,
                        Enddatum = endDatum,
                        Wochenbezeichnung = "KW " + woche + " " + aktuellesJahr,
                    };
                    kalenderwocheList.Add(kalenderwocheZukunft);
                }
            }

            List<Kalenderwoche> kalenderwocheListSorted = kalenderwocheList.OrderByDescending(o => o.Startdatum).ToList();
            combo_kw.ItemsSource = kalenderwocheListSorted;

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
                            dienstagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            dienstagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            dienstagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[2].Date) //Mittwoch
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            mittwochFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            mittwochSpät.Children.Add(newButton);
                            break;
                        case "N":
                            mittwochNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[3].Date) //Donnerstag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            donnerstagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            donnerstagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            donnerstagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[4].Date) //Freitag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            freitagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            freitagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            freitagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[5].Date) //Samstag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            samstagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            samstagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            samstagNacht.Children.Add(newButton);
                            break;
                    }
                }
                else if (schichteinsatz.Beginn.Date == wochentage[6].Date) //Sonntag
                {
                    switch (schichteinsatz.Art)
                    {
                        case "F":
                            sonntagFrüh.Children.Add(newButton);
                            break;
                        case "S":
                            sonntagSpät.Children.Add(newButton);
                            break;
                        case "N":
                            sonntagNacht.Children.Add(newButton);
                            break;
                    }
                }
                
            }

            headerMo.Text = headerMo.Text + wochentage[0].Date.ToShortDateString();
            headerDi.Text = headerDi.Text + wochentage[1].Date.ToShortDateString();
            headerMi.Text = headerMi.Text + wochentage[2].Date.ToShortDateString();
            headerDo.Text = headerDo.Text + wochentage[3].Date.ToShortDateString();
            headerFr.Text = headerFr.Text + wochentage[4].Date.ToShortDateString();
            headerSa.Text = headerSa.Text + wochentage[5].Date.ToShortDateString();
            headerSo.Text = headerSo.Text + wochentage[6].Date.ToShortDateString();

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

            //Ermittle Schichtnr
            Kalenderwoche kalenderwoche = (Kalenderwoche)combo_kw.SelectedItem;
            DateTime? datum = null;
            string art = string.Empty;
            switch (stackPanel.Name)
            {
                case "montagFrüh":
                    datum = wochentage[0] + new TimeSpan(6,0,0);
                    art = "F";
                    break;
                case "montagSpät":
                    datum = wochentage[0] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "montagNacht":
                    datum = wochentage[0] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "dienstagFrüh":
                    datum = wochentage[1] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "dienstagSpät":
                    datum = wochentage[1] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "dienstagNacht":
                    datum = wochentage[1] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "mittwochFrüh":
                    datum = wochentage[2] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "mittwochSpät":
                    datum = wochentage[2] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "mittwochNacht":
                    datum = wochentage[2] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "donnerstagFrüh":
                    datum = wochentage[3] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "donnerstagSpät":
                    datum = wochentage[3] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "donnerstagNacht":
                    datum = wochentage[3] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "freitagFrüh":
                    datum = wochentage[4] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "freitagSpät":
                    datum = wochentage[4] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "freitagNacht":
                    datum = wochentage[4] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "samstagFrüh":
                    datum = wochentage[5] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "samstagSpät":
                    datum = wochentage[5] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "samstagNacht":
                    datum = wochentage[5] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
                case "sonntagFrüh":
                    datum = wochentage[6] + new TimeSpan(6, 0, 0);
                    art = "F";
                    break;
                case "sonntagSpät":
                    datum = wochentage[6] + new TimeSpan(14, 0, 0);
                    art = "S";
                    break;
                case "sonntagNacht":
                    datum = wochentage[6] + new TimeSpan(22, 0, 0);
                    art = "N";
                    break;
            }

            int shiftnr = Schichteinsatz.getShiftNr(datum, art);
            if (shiftnr == 0)
            {
                string message = "Für den " + datum.Value.Date.ToShortDateString() + " ist keine Schicht auf der Datenbank vorhanden!";
                MessageBox.Show(message);
            }
            else
            {
                bool result;
                result = Schichteinsatz.checkNewShiftEntry(employeenr, shiftnr);
                if (result == true)
                {
                    result = Schichteinsatz.createShiftEntry(employeenr, shiftnr);
                    if (result == true)
                    {
                        Button newButton = new Button();
                        newButton.Name = "e" + employeenr.ToString();
                        newButton.Content = name;
                        newButton.FontSize = 12;
                        newButton.HorizontalContentAlignment = HorizontalAlignment.Left;
                        newButton.Click += new RoutedEventHandler(removeEmployee);
                        stackPanel.Children.Add(newButton);
                    }
                    else
                    {
                        MessageBox.Show("Fehler beim Erfassen der Schichtzuordnung!");
                    }
                }
                else
                {

                    MessageBox.Show("Mitarbeiter/in " + name + " ist der Schicht bereits zugeordnet!");
                }
   
            }

        }

        private void removeEmployee(object sender, RoutedEventArgs e)
        {
            int employeenr; int shiftnr;
            MessageBoxResult result = MessageBox.Show("Soll der/die Mitarbeiter/-in aus der Schicht entfernt werden?", "Mitarbeiter/-in entfernen", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Button button = (Button)sender;
                    StackPanel parentPanel = (StackPanel)button.Parent;

                    employeenr = int.Parse(button.Name.TrimStart('e'));

                    Kalenderwoche kalenderwoche = (Kalenderwoche)combo_kw.SelectedItem;
                    DateTime? datum = null;
                    string art = string.Empty;
                    switch (parentPanel.Name)
                    {
                        case "montagFrüh":
                            datum = wochentage[0] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "montagSpät":
                            datum = wochentage[0] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "montagNacht":
                            datum = wochentage[0] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "dienstagFrüh":
                            datum = wochentage[1] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "dienstagSpät":
                            datum = wochentage[1] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "dienstagNacht":
                            datum = wochentage[1] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "mittwochFrüh":
                            datum = wochentage[2] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "mittwochSpät":
                            datum = wochentage[2] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "mittwochNacht":
                            datum = wochentage[2] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "donnerstagFrüh":
                            datum = wochentage[3] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "donnerstagSpät":
                            datum = wochentage[3] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "donnerstagNacht":
                            datum = wochentage[3] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "freitagFrüh":
                            datum = wochentage[4] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "freitagSpät":
                            datum = wochentage[4] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "freitagNacht":
                            datum = wochentage[4] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "samstagFrüh":
                            datum = wochentage[5] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "samstagSpät":
                            datum = wochentage[5] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "samstagNacht":
                            datum = wochentage[5] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                        case "sonntagFrüh":
                            datum = wochentage[6] + new TimeSpan(6, 0, 0);
                            art = "F";
                            break;
                        case "sonntagSpät":
                            datum = wochentage[6] + new TimeSpan(14, 0, 0);
                            art = "S";
                            break;
                        case "sonntagNacht":
                            datum = wochentage[6] + new TimeSpan(22, 0, 0);
                            art = "N";
                            break;
                    }

                    shiftnr = Schichteinsatz.getShiftNr(datum, art);

                    if (Schichteinsatz.deleteShiftEntry(employeenr, shiftnr) == true)
                    {
                        parentPanel.Children.Remove(button);
                    }
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
                kalenderwoche.Enddatum = kalenderwoche.Enddatum + new TimeSpan(23, 59, 59);
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

        private void refreshShiftPlan(object sender, SelectionChangedEventArgs e)
        {
            //Planer aktualisieren

            if (this.IsInitialized == true)
            {
                montagFrüh.Children.Clear();
                montagSpät.Children.Clear();
                montagNacht.Children.Clear();
                dienstagFrüh.Children.Clear();
                dienstagSpät.Children.Clear();
                dienstagNacht.Children.Clear();
                mittwochFrüh.Children.Clear();
                mittwochSpät.Children.Clear();
                mittwochNacht.Children.Clear();
                donnerstagFrüh.Children.Clear();
                donnerstagSpät.Children.Clear();
                donnerstagNacht.Children.Clear();
                freitagFrüh.Children.Clear();
                freitagSpät.Children.Clear();
                freitagNacht.Children.Clear();
                samstagFrüh.Children.Clear();
                samstagSpät.Children.Clear();
                samstagNacht.Children.Clear();
                sonntagFrüh.Children.Clear();
                sonntagSpät.Children.Clear();
                sonntagNacht.Children.Clear();

                headerMo.Text = "";
                headerDi.Text = "";
                headerMi.Text = "";
                headerDo.Text = "";
                headerFr.Text = "";
                headerSa.Text = "";
                headerSo.Text = "";

                Kalenderwoche kalenderwoche = (Kalenderwoche)combo_kw.SelectedItem;

                DateTime datum = kalenderwoche.Startdatum;

                for (int i = 0; i <= 6; i++)
                {
                    wochentage[i] = datum;
                    datum = datum.AddDays(1);
                }

                List<Schichteinsatz> schichteinsatzList = getShiftInformation(kalenderwoche);
                if (schichteinsatzList.Count != 0)
                {
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
                                    dienstagFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    dienstagSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    dienstagNacht.Children.Add(newButton);
                                    break;
                            }
                        }
                        else if (schichteinsatz.Beginn.Date == wochentage[2].Date) //Mittwoch
                        {
                            switch (schichteinsatz.Art)
                            {
                                case "F":
                                    mittwochFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    mittwochSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    mittwochNacht.Children.Add(newButton);
                                    break;
                            }
                        }
                        else if (schichteinsatz.Beginn.Date == wochentage[3].Date) //Donnerstag
                        {
                            switch (schichteinsatz.Art)
                            {
                                case "F":
                                    donnerstagFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    donnerstagSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    donnerstagNacht.Children.Add(newButton);
                                    break;
                            }
                        }
                        else if (schichteinsatz.Beginn.Date == wochentage[4].Date) //Freitag
                        {
                            switch (schichteinsatz.Art)
                            {
                                case "F":
                                    freitagFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    freitagSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    freitagNacht.Children.Add(newButton);
                                    break;
                            }
                        }
                        else if (schichteinsatz.Beginn.Date == wochentage[5].Date) //Samstag
                        {
                            switch (schichteinsatz.Art)
                            {
                                case "F":
                                    samstagFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    samstagSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    samstagNacht.Children.Add(newButton);
                                    break;
                            }
                        }
                        else if (schichteinsatz.Beginn.Date == wochentage[6].Date) //Sonntag
                        {
                            switch (schichteinsatz.Art)
                            {
                                case "F":
                                    sonntagFrüh.Children.Add(newButton);
                                    break;
                                case "S":
                                    sonntagSpät.Children.Add(newButton);
                                    break;
                                case "N":
                                    sonntagNacht.Children.Add(newButton);
                                    break;
                            }
                        }

                    }
                }
                headerMo.Text = "Mo." + wochentage[0].Date.ToShortDateString();
                headerDi.Text = "Di." + wochentage[1].Date.ToShortDateString();
                headerMi.Text = "Mi." + wochentage[2].Date.ToShortDateString();
                headerDo.Text = "Do." + wochentage[3].Date.ToShortDateString();
                headerFr.Text = "Fr." + wochentage[4].Date.ToShortDateString();
                headerSa.Text = "Sa." + wochentage[5].Date.ToShortDateString();
                headerSo.Text = "So." + wochentage[6].Date.ToShortDateString();

            }
            
        }
    }
}
