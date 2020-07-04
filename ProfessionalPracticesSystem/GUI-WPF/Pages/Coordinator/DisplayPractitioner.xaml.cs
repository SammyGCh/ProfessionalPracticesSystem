/*
        Date: 24/07/2020                              
        Author:Ricardo Moguel Sanchez
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
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Windows;

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
