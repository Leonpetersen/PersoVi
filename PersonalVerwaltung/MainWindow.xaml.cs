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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            Personalplanung usercontrol = new Personalplanung();
            grid1.Children.Add(usercontrol);
        }
        private void mitarbeiter(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            Mitarbeiter usercontrol = new Mitarbeiter();
            grid1.Children.Add(usercontrol);
        }
        private void stundenerfassung(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            Stundenerfassung usercontrol = new Stundenerfassung();
            grid1.Children.Add(usercontrol);
        }
        private void lohnabrechnung(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            Lohnabrechnung usercontrol = new Lohnabrechnung();
            grid1.Children.Add(usercontrol);
        }
        private void personalplanung(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            Personalplanung usercontrol = new Personalplanung();
            grid1.Children.Add(usercontrol);
        }
        private void einstellungen(object sender, RoutedEventArgs e)
        {
            grid1.Children.Clear();
            Einstellungen usercontrol = new Einstellungen();
            grid1.Children.Add(usercontrol);
        }
        private void Logout (object sender,RoutedEventArgs e)
        {

        }
        private void minimize(object sender, RoutedEventArgs e)
        {

        }
        private void smaller(object sender, RoutedEventArgs e)
        {

        }
        private void shutdown(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
        private void OnMouseEnterButton(object sender, EventArgs e)
        {
        }
    }
}
