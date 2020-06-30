/*
    Date: 27/05/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GUI_WPF.UserControls.Project;
using GUI_WPF.Windows;
using BusinessDomain;
using System;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para ProjectUpdate.xaml
    /// </summary>
    public partial class ProjectUpdate : Page
    {
        private readonly UserControl sectionSelected;

        public ProjectUpdate(UserControl sectionSelected)
        {
            InitializeComponent();

            this.sectionSelected = sectionSelected;
            projectSection.Children.Add(this.sectionSelected);
            
        }

        private void CancelUpdate(object sender, RoutedEventArgs e)
        {
            string confirmMessage = "¿Seguro desea cancelar la actualización?";

            bool wantToCancel = DialogWindowManager.ShowConfirmationWindow(confirmMessage);

            if (wantToCancel)
            {
                NavigationService.GoBack();               
            }
        }

        private void UpdateProjectSection(object sender, RoutedEventArgs e)
        {
            if (sectionSelected is DataProjectControl)
            {
                (sectionSelected as DataProjectControl).UpdateProjectData();
            }
            else if (sectionSelected is ProjectResponsableControl)
            {
                (sectionSelected as ProjectResponsableControl).UpdateProjectResponsable();
            }
            else if (sectionSelected is ProjectActivityUpdateControl)
            {
                (sectionSelected as ProjectActivityUpdateControl).UpdateProjectActivities();
            }
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (sectionSelected.Content != null)
            {
                
            }
        }
    }
}
