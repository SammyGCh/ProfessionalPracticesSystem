/*
    Date: 27/05/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GUI_WPF.UserControls.Project;
using BusinessDomain;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para ProjectSections.xaml
    /// </summary>
    public partial class ProjectSections : Page
    {
        private readonly Project projectSelected;

        public ProjectSections(Project projectSelected)
        {
            InitializeComponent();
            this.projectSelected = projectSelected;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoToProjectData(object sender, RoutedEventArgs e)
        {
            DataProjectControl dataProjectControl = new DataProjectControl
            {
                DataContext = projectSelected
            };

            NavigationService.Navigate(new ProjectUpdate(dataProjectControl));
        }

        private void GoToProjectResponsable(object sender, RoutedEventArgs e)
        {
            ProjectResponsableControl projectResponsableControl = new ProjectResponsableControl
            {
                DataContext = projectSelected
            };

            /*
            NavigationService.Navigate(new ProjectUpdate(projectResponsableControl));
            */
            PageMannagerNavigation.NavigateTo(this, new ProjectUpdate(projectResponsableControl));
        }

        private void GoToProjectActivities(object sender, RoutedEventArgs e)
        {
            ProjectActivityUpdateControl projectActivityUpdateControl = new ProjectActivityUpdateControl(projectSelected);

            NavigationService.Navigate(new ProjectUpdate(projectActivityUpdateControl));
        }
    }
}
