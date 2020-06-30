/*
    Date: 22/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataAccess.Implementation;
using BusinessDomain;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para Requests.xaml
    /// </summary>
    public partial class Requests : Page
    {
        private readonly ObservableCollection<ProjectsRequest> projectsRequestsActive;

        public Requests()
        {
            InitializeComponent();
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            List<ProjectsRequest> projectsRequestsList = projectsRequestDao.GetAllProjectsRequestActive();

            projectsRequestsActive = new ObservableCollection<ProjectsRequest>(projectsRequestsList);
            projectsRequestsTemplate.ItemsSource = projectsRequestsActive;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public void DeleteProjectRequestAssigned(ProjectsRequest projectsRequestAssigned)
        {
            if (projectsRequestAssigned != null)
            {
                projectsRequestsActive.Remove(projectsRequestAssigned);
            }
        }
    }
}
