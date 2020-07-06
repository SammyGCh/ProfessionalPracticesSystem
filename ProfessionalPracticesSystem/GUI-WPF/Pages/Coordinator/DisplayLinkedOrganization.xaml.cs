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
    public partial class DisplayLinkedOrganization : Page
    {
        public DisplayLinkedOrganization(LinkedOrganization linkedOrganization)
        {
            InitializeComponent();

            PractitionerDAO practitionerDAO = new PractitionerDAO();
            ProjectDAO projectDAO = new ProjectDAO();

            this.DataContext = linkedOrganization;

            List<Project> projects = projectDAO.GetProjectsByOrganization(linkedOrganization.IdLinkedOrganization);
            List<BusinessDomain.Practitioner> practitioners = practitionerDAO.GetAllPractitionerByLinkedOrganization(linkedOrganization.IdLinkedOrganization);
            
            orgSector.Text = linkedOrganization.BelongsTo.Name;
            projectsList.ItemsSource = projects;
            practitionerList.ItemsSource = practitioners;

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

    }
}
