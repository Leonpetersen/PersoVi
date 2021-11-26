using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace PersonalVerwaltung
{
    /// <summary>
    /// Interaktionslogik für MaAdd.xaml
    /// </summary>
    public partial class MaAdd : Window
    {
        public DbConnector dbConnector = new DbConnector();
        public MaAdd()
        {
            InitializeComponent();
            ort.IsReadOnly = true;
            ort.Background = Brushes.LavenderBlush;
        }

        private void saveEmployeeData(object sender, RoutedEventArgs e)
        {
            //Werte prüfen
            if (vorname.Text == "")
            {
                MessageBox.Show("Feld \"Vorname\" darf nicht leer sein!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (nachname.Text == "")
            {
                MessageBox.Show("Feld \"Nachname\" darf nicht leer sein!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (strasse.Text == "")
            {
                MessageBox.Show("Feld \"Straße & Hausnummer\" darf nicht leer sein!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (combo_plz.SelectedValue == null)
            {
                MessageBox.Show("Bitte einen Ort auswählen", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (email.Text == "")
            {
                MessageBox.Show("Feld \"E-Mail\" darf nicht leer sein!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (combo_abt.SelectedValue == null)
            {
                MessageBox.Show("Bitte eine Abteilung auswählen!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else if (combo_beruf.SelectedValue == null)
            {
                MessageBox.Show("Bitte eine Berufung auswählen!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else
            {

                //Popup um Datenbankinsert zu akzeptieren
                MessageBoxResult result = MessageBox.Show("Mitarbeiter anlegen?", "Mitarbeiter anlegen", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        //Datenbankinsert
                        Ort ort = (Ort)combo_plz.SelectedItem;
                        Abteilung abteilung = (Abteilung)combo_abt.SelectedItem;
                        Beruf beruf = (Beruf)combo_beruf.SelectedItem;

                        Employee employee = new Employee()
                        {
                            Firstname = vorname.Text,
                            Lastname = nachname.Text,
                            Street = strasse.Text,
                            Zipcode = ort.Plz,
                            Maritalstatus = combo_marital.SelectedItem.ToString(),
                            Phonenumber = telefon.Text,
                            Email = email.Text,
                            Departmentnr = abteilung.Abteilungsnr,
                            Professionnr = beruf.Berufsnummer,
                            Entrydate = eintritt.SelectedDate.Value
                        };

                        if (employee.createEmployee() == true)
                        {
                            this.DialogResult = true;
                            this.Close();
                        }
                        else
                        {
                            this.DialogResult = false;
                            this.Close();
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }

        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void onSelectionChangedPlz(object sender, SelectionChangedEventArgs e)
        {
            Ort ort_obj = new Ort();
            ort_obj = (Ort)combo_plz.SelectedItem;
            if (ort_obj != null)
            {
                ort.Text = (string)ort_obj.Ortsname;
            }
        }
    }
}
