﻿/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using GUI_WPF.Pages.Practitioner;
using BusinessLogic;
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
