/*
        Date: 15/05/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using DataAccess.Implementation;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for DisplayLinkedOrganization.xaml
    /// </summary>
     
    public partial class DisplayLinkedOrganization : Page
    {
        public DisplayLinkedOrganization(LinkedOrganization linkedOrganization)
        {
            InitializeComponent();

            PractitionerDAO practitionerDAO = new PractitionerDAO();
            ProjectDAO projectDAO = new ProjectDAO();

            this.DataContext = linkedOrganization;

            List<Project> allOrganizationProjects = projectDAO.GetProjectsByOrganization(linkedOrganization.IdLinkedOrganization);
            List<BusinessDomain.Practitioner> allOrganizationPractitioners = practitionerDAO.GetAllPractitionerByLinkedOrganization(linkedOrganization.IdLinkedOrganization);
            
            orgSector.Text = linkedOrganization.BelongsTo.Name;
            projectsList.ItemsSource = allOrganizationProjects;
            practitionerList.ItemsSource = allOrganizationPractitioners;

        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoToProject(object sender, RoutedEventArgs e)
        {
            Project projectSelected = (projectsList.SelectedItem as Project);

            NavigationService.Navigate(new ProjectDetails(projectSelected));
        }

        private void GoToPractitioner(object sender, RoutedEventArgs e)
        {
            BusinessDomain.Practitioner practitionerSelected = (practitionerList.SelectedItem as BusinessDomain.Practitioner);

            NavigationService.Navigate(new DisplayPractitioner(practitionerSelected));
        }
    }
}
