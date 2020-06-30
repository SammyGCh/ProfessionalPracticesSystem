/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */


using System.Windows;
using System.Windows.Controls;
using BusinessDomain;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Pages.Practitioner;

namespace GUI_WPF.Windows
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
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
    }
}
