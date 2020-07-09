/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Pages.Notice;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para CoordinatorHome.xaml
    /// </summary>
    public partial class CoordinatorHome : Page
    {
        private const int COORDINATOR_TYPE = 1;

        public CoordinatorHome()
        {
            InitializeComponent();
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProject());
        }

        private void RegisterPractitioner(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPractitioner());
        }

        private void CheckPractioners(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisteredPractitionerList());
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
            AcademicDAO academicHandler = new AcademicDAO();

            Academic academic = academicHandler.GetCoordinator(); 

            NavigationService.Navigate(new NoticeBoard(academic.PersonalNumber));
        }

        private void GoToStatitics(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisplayPractitionersStatistics());
        }

        private void GoToRequests(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Requests());
        }
    }
}
