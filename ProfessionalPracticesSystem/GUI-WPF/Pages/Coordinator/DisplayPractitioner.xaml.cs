/*
        Date: 24/07/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using DataAccess.Implementation;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for DisplayPractitioner.xaml
    /// </summary>
    public partial class DisplayPractitioner : Page
    {
        public DisplayPractitioner(BusinessDomain.Practitioner selectedPractitioner)
        {
            InitializeComponent();

            this.DataContext = selectedPractitioner;

            if(selectedPractitioner.Assigned != null)
            {
                projectData.Visibility = Visibility.Visible;
            }
        }

        private void CancelViewPractitionerData(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoToProject(object sender, RoutedEventArgs e)
        {
            ProjectDAO selectedProjectDAO = new ProjectDAO();
            Project selectedProject = selectedProjectDAO.GetProjectByName(projectName.Text);
            NavigationService.Navigate(new ProjectDetails(selectedProject));
        }
    }
}
