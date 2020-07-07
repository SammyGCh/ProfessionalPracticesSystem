/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
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
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;
using System.Collections.ObjectModel;
using GUI_WPF.Windows;
using GUI_WPF.Pages.Notice;

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
            NavigationService.Navigate(new NoticeBoard());
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
