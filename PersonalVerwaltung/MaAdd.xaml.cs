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
    /// Interaktionslogik für MaAdd.xaml
    /// </summary>
    public partial class MaAdd : Window
    {
        public MaAdd()
        {
            InitializeComponent();
            ort.IsReadOnly = true;
            ort.Background = Brushes.LavenderBlush;
        }

        private void saveEmployeeData(object sender, RoutedEventArgs e)
        {
            //Popup to confirm database insert

            //Insert
            Employee employee = new Employee(vorname.Text, nachname.Text, strasse.Text, (int)combo_plz.SelectedItem, email.Text, (int)combo_abt.SelectedItem, int.Parse(telefon.Text), combo_marital.SelectedItem.ToString(), int.Parse(combo_beruf.SelectedItem.ToString()), eintritt.SelectedDate.Value);
            employee.createEmployee();
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
    }
}
