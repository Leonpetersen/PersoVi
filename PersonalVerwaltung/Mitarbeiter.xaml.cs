using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PersonalVerwaltung
{
    /// <summary>
    /// Interaktionslogik für Mitarbeiter.xaml
    /// </summary>
    public partial class Mitarbeiter : UserControl
    {
        public CollectionView EmployeeView;
        public Mitarbeiter()
        {
            InitializeComponent();

            EmployeeView = (CollectionView)CollectionViewSource.GetDefaultView(mitarbeiter.ItemsSource);
            EmployeeView.Filter = UserFilter;
        }


        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Employee).Firstname.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(mitarbeiter.ItemsSource).Refresh();
        }


        private void Btn_Add(object sender, RoutedEventArgs e)
        {

            MaAdd maAdd = new MaAdd();
            bool? dialogStatus = maAdd.ShowDialog();
            if (dialogStatus == true)
            {
                //Refresh Mitarbeiter Liste
                EmployeeList employeeList = (EmployeeList)this.TryFindResource("employeeList");
                employeeList.Refresh();
                EmployeeView.Refresh();
                MessageBox.Show("Mitarbeiter erfolgreich gespeichert!");
            }
        }
        private void Btn_Remove(object sender, RoutedEventArgs e)
        {
            bool status = false;
            MessageBoxResult result = MessageBox.Show("Soll der Mitarbeiter von der Datenbank entfernt werden?", "Mitarbeiter löschen", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Employee employee = (Employee)mitarbeiter.SelectedItem;
                    status = employee.deleteEmployee();
                    break;
                case MessageBoxResult.No:
                    break;
            }

            if (status == true)
            {
                //Refresh Mitarbeiter Liste
                EmployeeList employeeList = (EmployeeList)this.TryFindResource("employeeList");
                employeeList.Refresh();
                EmployeeView.Refresh();

                MessageBox.Show("Mitarbeiter erfolgreich entfernt!");
            }

        }
        private void Btn_Show(object sender, RoutedEventArgs e)
        {
            MAanzeige maAnzeige = new MAanzeige((Employee)mitarbeiter.SelectedItem);
            bool? dialogStatus = maAnzeige.ShowDialog();
            if (dialogStatus == true)
            {
                //Refresh Mitarbeiter Liste
                EmployeeList employeeList = (EmployeeList)this.TryFindResource("employeeList");
                employeeList.Refresh();
                EmployeeView.Refresh();
            }
        }
        private void showEmployee(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MAanzeige maAnzeige = new MAanzeige((Employee)mitarbeiter.SelectedItem);
            bool? dialogStatus = maAnzeige.ShowDialog();
            if (dialogStatus == true)
            {
                //Refresh Mitarbeiter Liste
                EmployeeList employeeList = (EmployeeList)this.TryFindResource("employeeList");
                employeeList.Refresh();
                EmployeeView.Refresh();
            }
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


    }
}

