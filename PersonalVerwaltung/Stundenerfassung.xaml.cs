﻿using System;
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
        public Stundenerfassung()
        {
            InitializeComponent();
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
    }
}
