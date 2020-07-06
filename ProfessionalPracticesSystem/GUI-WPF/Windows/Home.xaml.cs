/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using GUI_WPF.Windows;

namespace GUI_WPF.Windows
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        public Home(Page homePage, string userNameFullName)
        {
            InitializeComponent();
            userName.Text = userNameFullName;
            homeFrame.Content = homePage;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            if (DialogWindowManager.ShowConfirmationWindow("¿Desea cerrar sesion?"))
            {
                this.Close();
            }
        }

    }
}
