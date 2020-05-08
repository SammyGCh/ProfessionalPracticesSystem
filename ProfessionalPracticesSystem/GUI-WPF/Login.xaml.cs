/*
    Date: 27/04/2020
    Author(s): Sammy Guadarrama Chávez
 */

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
using System.Text.RegularExpressions;
using BusinessLogic;
using GUI_WPF.Windows;
using GUI_WPF.Pages.Coordinator;

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Content = new AddDocument(1, 1);
        }
        
        /*private void LogIn(object sender, RoutedEventArgs e)
        {
            CoordinatorHome coordinatorHome = new CoordinatorHome();
            Home homeWindow = new Home(coordinatorHome);
            
            homeWindow.Show();

            this.Close();
        }

        private void EnableLoginButton(object sender, RoutedEventArgs e)
        {
            if (password.SecurePassword.Length == 0 || userTextBox.Text.Length == 0)
            {
                logInButton.IsEnabled = false;
            }
            else
            {
                logInButton.IsEnabled = true;
            }
        }

        private void ValidateUsername(object sender, TextChangedEventArgs e)
        {
            if (!ValidatorText.IsUserName(userTextBox.Text))
            {
                invalidUsernameLabel.Visibility = Visibility.Visible;
            }
            else
            {
                invalidUsernameLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
