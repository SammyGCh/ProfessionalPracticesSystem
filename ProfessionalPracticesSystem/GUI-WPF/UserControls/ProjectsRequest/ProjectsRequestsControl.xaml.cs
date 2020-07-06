/*
    Date: 22/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using GUI_WPF.Windows.ProjectsRequest;
using GUI_WPF.Windows;

namespace GUI_WPF.UserControls.ProjectsRequest
{
    /// <summary>
    /// Lógica de interacción para ProjectsRequestsControl.xaml
    /// </summary>
    public partial class ProjectsRequestsControl : UserControl
    {
        private BusinessDomain.Project projectSelected;

        public ProjectsRequestsControl()
        {
            InitializeComponent();
        }

        private void GoToRequestSummary(object sender, RoutedEventArgs e)
        {
            BusinessDomain.ProjectsRequest projectsRequestToAssign = GetProjectsRequestSelected();

            if (projectsRequestToAssign != null)
            {
                RequestSummary requestSummaryWindow = new RequestSummary
                {
                    DataContext = projectsRequestToAssign
                };

                requestSummaryWindow.Show();
                requestSummaryWindow.Focus();
            }
        }

        private BusinessDomain.ProjectsRequest GetProjectsRequestSelected()
        {
            BusinessDomain.ProjectsRequest projectsRequestSelected =
                this.DataContext as BusinessDomain.ProjectsRequest;
            projectSelected = GetProjectSelected();

            projectsRequestSelected.ProjectsRequested.Clear();
            projectsRequestSelected.ProjectsRequested.Add(projectSelected);

            return projectsRequestSelected;
        }

        private BusinessDomain.Project GetProjectSelected()
        {
            BusinessDomain.Project projectSelected =
                projectsRequestedList.SelectedItem as BusinessDomain.Project;

            return projectSelected;
        }
    }
}
