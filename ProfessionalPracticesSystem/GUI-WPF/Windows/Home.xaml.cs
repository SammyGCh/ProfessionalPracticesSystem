/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */


using System.Windows;
using System.Windows.Controls;
using BusinessDomain;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Pages.Practitioner;
using GUI_WPF.Pages.User;

namespace GUI_WPF.Windows
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private readonly Page userHomePage;

        public Home()
        {
            InitializeComponent();
        }

        public Home(Page homePage, string userNameFullName)
        {
            InitializeComponent();
            userName.Text = userNameFullName;
            homeFrame.Content = homePage;
            userHomePage = homePage;
        }

        public Page GetUserHomePage()
        {
            return userHomePage;
        }

        private void GoHome(object sender, RoutedEventArgs e)
        {
            homeFrame.Navigate(userHomePage);
        }

        private void UpdatePassword(object sender, RoutedEventArgs e)
        {
            homeFrame.Navigate(new UpdatePassword());
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Login loginWindow = new Login();

            loginWindow.Show();
            this.Close();
        }
    }
}
