/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessDomain;

using GUI_WPF.Pages.Practitioner;


namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para CoordinatorHome.xaml
    /// </summary>
    public partial class CoordinatorHome : Page
    {
        public CoordinatorHome()
        {
            InitializeComponent();
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProject());
        }

        private void CheckOrg(object sender, RoutedEventArgs e)
        { 
            NavigationService.Navigate(new LinkedOrganizationsList());
        }

        private void CheckProjects(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProjectsConsult());
        }

        private void RegisterOrganization(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddOrganization());
        }

        private void GoToNotices(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestProject());
        }
    }
}
