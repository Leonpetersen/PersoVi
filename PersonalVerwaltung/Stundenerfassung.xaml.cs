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
    /// Interaktionslogik für Stundenerfassung.xaml
    /// </summary>
    public partial class Stundenerfassung : UserControl
    {
        public CollectionView WorkingTimeView;
        public Stundenerfassung()
        {
            InitializeComponent();
            WorkingTimeView = (CollectionView)CollectionViewSource.GetDefaultView(workingTimeEmployees.ItemsSource);
            WorkingTimeView.Filter = TimeFilter;
        }

        private bool TimeFilter(object item)
        {
            bool entryExists;

            if (String.IsNullOrEmpty(txtFilter.Text))
            {
                //return true;
                entryExists = true;
            }
            else
            {
                //Prüfe Personalnummer
                if (((item as WorkingTimeEmployee).Employeenr.ToString().IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    entryExists = true;
                }
                //Prüfe Vorname
                else if (((item as WorkingTimeEmployee).Firstname.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    entryExists = true;
                }
                //Prüfe Nachname
                else if (((item as WorkingTimeEmployee).Lastname.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    entryExists = true;
                }
                else
                {
                    entryExists = false;
                }

            }
            return entryExists;

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

        private void createTimeRecord(object sender, RoutedEventArgs e)
        {
  
            StundenAdd stundenAdd = new StundenAdd();
            bool? dialogStatus = stundenAdd.ShowDialog();
            if (dialogStatus == true)
            {
                MessageBox.Show("Arbeitszeit wurde erfasst!");
                //Refresh Stundenerfassung

            }

        }

        private void refreshTimeTable(object sender, SelectionChangedEventArgs e)
        {
            if (WorkingTimeView != null)
            {
                WorkingTimeEmployeeList workingTimeEmployeeList = (WorkingTimeEmployeeList)this.TryFindResource("workingTimeEmployeeList");
                Kalenderwoche kalenderwoche = (Kalenderwoche)combo_kw.SelectedItem;
                workingTimeEmployeeList.Refresh(kalenderwoche);
                WorkingTimeView.Refresh();
            }

          
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            WorkingTimeView.Refresh();
        }

        private void showEmployeeDetail(object sender, MouseButtonEventArgs e)
        {
            StundenMa stundenMa = new StundenMa((WorkingTimeEmployee)workingTimeEmployees.SelectedItem, (Kalenderwoche)combo_kw.SelectedItem);
            stundenMa.Show();
        }
    }
}
