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

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void LogIn(object sender, RoutedEventArgs e)
        {
            userTextBox.Text = "Apreté el botón";
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
    }
}
