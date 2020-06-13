/*
    Date: 24/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;


namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para ProjectDetails.xaml
    /// </summary>
    public partial class ProjectDetails : Page
    {
        public ProjectDetails(Project project)
        {
            InitializeComponent();

            this.DataContext = project;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
