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
    /// Interaktionslogik für Lohnabrechnung.xaml
    /// </summary>
    public partial class Lohnabrechnung : UserControl
    {
        public Lohnabrechnung()
        {
            InitializeComponent();

            //ComboBox Monate füllen
            combo_month.Items.Add("Januar");
            combo_month.Items.Add("Februar");
            combo_month.Items.Add("März");
            combo_month.Items.Add("April");
            combo_month.Items.Add("Mai");
            combo_month.Items.Add("Juni");
            combo_month.Items.Add("Juli");
            combo_month.Items.Add("August");
            combo_month.Items.Add("September");
            combo_month.Items.Add("Oktober");
            combo_month.Items.Add("November");
            combo_month.Items.Add("Dezember");

            //ComboBox Jahre füllen
            int aktuellesJahr = DateTime.Now.Year;
            List<int> jahreList = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                jahreList.Add(aktuellesJahr--);
            }
            combo_year.ItemsSource = jahreList;

            //ComboBoxen initial belegen
            combo_year.SelectedIndex = 0;

            int aktuellerMonat = DateTime.Now.Month;
            switch (aktuellerMonat)
            {
                case 1:
                    combo_month.SelectedIndex = 0;
                    break;
                case 2:
                    combo_month.SelectedIndex = 1;
                    break;
                case 3:
                    combo_month.SelectedIndex = 2;
                    break;
                case 4:
                    combo_month.SelectedIndex = 3;
                    break;
                case 5:
                    combo_month.SelectedIndex = 4;
                    break;
                case 6:
                    combo_month.SelectedIndex = 5;
                    break;
                case 7:
                    combo_month.SelectedIndex = 6;
                    break;
                case 8:
                    combo_month.SelectedIndex = 7;
                    break;
                case 9:
                    combo_month.SelectedIndex = 8;
                    break;
                case 10:
                    combo_month.SelectedIndex = 9;
                    break;
                case 11:
                    combo_month.SelectedIndex = 10;
                    break;
                case 12:
                    combo_month.SelectedIndex = 11;
                    break;
            }

        }
        private void Btn_Show(object sender, RoutedEventArgs e)
        {

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

        private void showPayroll(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
