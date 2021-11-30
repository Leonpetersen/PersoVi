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
    /// Interaktionslogik für StundenAdd.xaml
    /// </summary>
    public partial class StundenAdd : Window
    {
        public StundenAdd()
        {
            InitializeComponent();
        }

        private void saveTimeRecording(object sender, RoutedEventArgs e)
        {

            int hourstart; int minutestart; int hourend; int minuteend;

            if (hour_start.SelectedValue.ToString() == "00")
            {
                hourstart = 0;
            }
            else
            {
                hourstart = int.Parse(hour_start.SelectedValue.ToString().TrimStart('0'));
            }
            if (minute_start.SelectedValue.ToString() == "00")
            {
                minutestart = 0;
            }
            else
            {
                minutestart = int.Parse(minute_start.SelectedValue.ToString().TrimStart('0'));
            }

            if (hour_end.SelectedValue.ToString() == "00")
            {
                hourend = 0;
            }
            else
            {
                hourend = int.Parse(hour_end.SelectedValue.ToString().TrimStart('0'));
            }
            if (minute_end.SelectedValue.ToString() == "00")
            {
                minuteend = 0;
            }
            else
            {
                minuteend = int.Parse(minute_end.SelectedValue.ToString().TrimStart('0'));
            }

            DateTime begin = new DateTime((int)beginDate.SelectedDate.Value.Year, (int)beginDate.SelectedDate.Value.Month, (int)beginDate.SelectedDate.Value.Day, hourstart, minutestart, 0);
            DateTime end = new DateTime((int)endDate.SelectedDate.Value.Year, (int)endDate.SelectedDate.Value.Month, (int)endDate.SelectedDate.Value.Day, hourend, minuteend, 0);
            decimal hours = (decimal)(end - begin).TotalHours;

            //Prüfungen
            //Beginndatum darf nicht größer sein als Enddatum
            if (begin > end)
            {
                MessageBox.Show("Beginndatum größer als Enddatum!", "Eingaben prüfen!", MessageBoxButton.OK);
            }
            else
            {
                
                Employee employee = (Employee)combo_employee.SelectedValue;

                TimeRecord timeRecord = new TimeRecord()
                {
                    Employeenr = employee.Employeenr,
                    Begin = begin,
                    End = end,
                    Workingtime = hours,
                };

                if (timeRecord.checkPeriod() == false)
                {
                    MessageBox.Show("Für den/die Mitarbeiter/in " + timeRecord.Employeenr + " existiert bereits ein Eintrag für den angegebenen Zeitraum!", "Eingaben prüfen!", MessageBoxButton.OK);
                }
                else
                {
                    if (timeRecord.createRecord() == true)
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        this.DialogResult = false;
                        this.Close();
                    }
                }
            }


        }

        private void closeWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
