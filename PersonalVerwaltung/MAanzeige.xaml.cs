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
using System.Windows.Shapes;

namespace PersonalVerwaltung
{
    /// <summary>
    /// Interaktionslogik für MAanzeige.xaml
    /// </summary>
    public partial class MAanzeige : Window
    {
        public Employee employee { get; set; }
        public MAanzeige(Employee mitarbeiter)
        {
            InitializeComponent();

            this.employee = mitarbeiter;

            //Felder füllen
            vorname.Text = mitarbeiter.Firstname;
            nachname.Text = mitarbeiter.Lastname;
            strasse.Text = mitarbeiter.Street;
            combo_plz.SelectedValue = mitarbeiter.Zipcode;
            combo_marital.SelectedValue = mitarbeiter.Maritalstatus;
            email.Text = mitarbeiter.Email;
            telefon.Text = mitarbeiter.Phonenumber;
            combo_abt.SelectedValue = mitarbeiter.Departmentnr;
            combo_beruf.SelectedValue = mitarbeiter.Professionnr;
            eintritt.SelectedDate = mitarbeiter.Entrydate;

            vorname.IsReadOnly = true;
            nachname.IsReadOnly = true;
            strasse.IsReadOnly = true;
            combo_plz.IsEnabled = false;
            ort.IsReadOnly = true;
            combo_marital.IsEnabled = false;
            email.IsReadOnly = true;
            telefon.IsReadOnly = true;
            combo_abt.IsEnabled = false;
            combo_beruf.IsEnabled = false;
            eintritt.IsEnabled = false;

            vorname.Background = Brushes.LavenderBlush;
            nachname.Background = Brushes.LavenderBlush;
            strasse.Background = Brushes.LavenderBlush;
            ort.Background = Brushes.LavenderBlush;
            email.Background = Brushes.LavenderBlush;
            telefon.Background = Brushes.LavenderBlush;
            eintritt.Background = Brushes.LavenderBlush;


        }
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness();
            }
        }

        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void deleteEmployee(object sender, RoutedEventArgs e)
        {
            bool status = false;
            MessageBoxResult result = MessageBox.Show("Soll der Mitarbeiter von der Datenbank entfernt werden?", "Mitarbeiter löschen", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    status = employee.deleteEmployee();
                    break;
                case MessageBoxResult.No:
                    break;
            }

            if (status == true)
            {
                MessageBox.Show("Mitarbeiter erfolgreich entfernt!");
                this.DialogResult = true;
                this.Close();
            }
        }

        private void changeEditMode(object sender, RoutedEventArgs e)
        {
            if (vorname.IsReadOnly == true) //Felder zur Bearbeitung freigeben
            {
                vorname.IsReadOnly = false;
                nachname.IsReadOnly = false;
                strasse.IsReadOnly = false;
                combo_plz.IsEnabled = true;
                combo_marital.IsEnabled = true;
                email.IsReadOnly = false;
                telefon.IsReadOnly = false;
                combo_abt.IsEnabled = true;
                combo_beruf.IsEnabled = true;
                eintritt.IsEnabled = true;

                vorname.Background = Brushes.White;
                nachname.Background = Brushes.White;
                strasse.Background = Brushes.White;
                email.Background = Brushes.White;
                telefon.Background = Brushes.White;
                eintritt.Background = Brushes.White;
            }
            else //Felder auf ReadOnly setzen
            {
                vorname.IsReadOnly = true;
                nachname.IsReadOnly = true;
                strasse.IsReadOnly = true;
                combo_plz.IsEnabled = false;
                ort.IsReadOnly = true;
                combo_marital.IsEnabled = false;
                email.IsReadOnly = true;
                telefon.IsReadOnly = true;
                combo_abt.IsEnabled = false;
                combo_beruf.IsEnabled = false;
                eintritt.IsEnabled = false;

                vorname.Background = Brushes.LavenderBlush;
                nachname.Background = Brushes.LavenderBlush;
                strasse.Background = Brushes.LavenderBlush;
                ort.Background = Brushes.LavenderBlush;
                email.Background = Brushes.LavenderBlush;
                telefon.Background = Brushes.LavenderBlush;
                eintritt.Background = Brushes.LavenderBlush;
            }
        }

        private void saveChanges(object sender, RoutedEventArgs e)
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
                bool status = false;
                MessageBoxResult result = MessageBox.Show("Mitarbeiterdaten ändern?", "Mitarbeiter ändern", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Employee employeeChanged = new Employee()
                        {
                            Firstname = vorname.Text,
                            Lastname = nachname.Text,
                            Street = strasse.Text,
                            Zipcode = (int)combo_plz.SelectedValue,
                            Maritalstatus = (string)combo_marital.SelectedValue,
                            Email = email.Text,
                            Phonenumber = telefon.Text,
                            Departmentnr = (int)combo_abt.SelectedValue,
                            Professionnr = (int)combo_beruf.SelectedValue,
                            Entrydate = (DateTime)eintritt.SelectedDate

                        };
                        status = employeeChanged.updateEmployee();
                        break;
                    case MessageBoxResult.No:
                        break;
                }

                if (status == true)
                {
                    MessageBox.Show("Mitarbeiter erfolgreich geändert!");
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }
    }
}
