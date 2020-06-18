/*
    Date: 12/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using GUI_WPF.Pages.Practitioner;
using System.Linq;
using System.Windows;
using BusinessDomain;

namespace GUI_WPF.Windows
{
    /// <summary>
    /// Lógica de interacción para ProjectInformation.xaml
    /// </summary>
    public partial class ProjectInformation : Window
    {
        public ProjectInformation()
        {
            InitializeComponent();
        }

        private void SelectProject(object sender, RoutedEventArgs e)
        {
            RequestProject requestProjectPage = GetRequestProjectPage();

            if (requestProjectPage.AreThreeProjectsSelected())
            {
                string message = "Ya tienes seleccionado tres proyectos. Si deseas seleccionar otro elimina " +
                    "un proyecto de la sección \"Mis proyectos seleccionados\".";

                DialogWindowManager.ShowErrorWindow(message);
            }
            else
            {
                Project projectSelected = this.DataContext as Project;

                requestProjectPage.AddProjectSelected(projectSelected);
                requestProjectPage.RemoveAvailableProjectSelected(projectSelected);
                this.Close();
            }         
        }

        private RequestProject GetRequestProjectPage()
        {
            var requestProjectWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(
                    window => window is Home
                ) as Home;

            RequestProject requestProjectPage = requestProjectWindow.homeFrame.Content as RequestProject;

            return requestProjectPage;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
