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
    /// Interaktionslogik für StundenMa.xaml
    /// </summary>
    public partial class StundenMa : Window
    {
        public StundenMa(WorkingTimeEmployee workingTimeEmployee, Kalenderwoche kalenderwoche)
        {
            InitializeComponent();
            WorkingDayEmployeeList workingDayEmployeeList = (WorkingDayEmployeeList)this.TryFindResource("workingDayEmployeeList");
            workingDayEmployeeList.Refresh(kalenderwoche, workingTimeEmployee.Employeenr);
            personalnr.Text = personalnr.Text + " " + workingTimeEmployee.Employeenr.ToString();
            name.Text = name.Text + " " + workingTimeEmployee.Firstname + " " + workingTimeEmployee.Lastname;
            kw.Text = kw.Text + " " + kalenderwoche.Wochennr;
            arbeitszeit.Text = arbeitszeit.Text + " " + Hilfsmittel.getTotalHoursWeek(kalenderwoche, workingTimeEmployee.Employeenr);
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

        private void minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void smaller(object sender, RoutedEventArgs e)
        {

        }

        private void shutdown(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
