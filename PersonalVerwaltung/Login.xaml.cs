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
    /// Interaktionslogik für Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private void btn_submitClick(object sender, RoutedEventArgs e)
        {
            if (LoginHandler.checkCredentials(txtUsername.Text, txtPassword.Password) == true)
            {
                MainWindow dashboard = new MainWindow();
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Benutzername oder Passwort falsch eingegeben!");
            }
         
        }

        private void btn_shutdown_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void onKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (LoginHandler.checkCredentials(txtUsername.Text, txtPassword.Password) == true)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Benutzername oder Passwort falsch eingegeben!");
                }
            }
        }
    }
}
