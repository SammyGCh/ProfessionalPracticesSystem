/*
    Date: 22/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Windows.ProjectsRequest
{
    /// <summary>
    /// Lógica de interacción para ProjectSummary.xaml
    /// </summary>
    public partial class RequestSummary : Window
    {
        public RequestSummary()
        {
            InitializeComponent();
        }

        private void AssignProject(object sender, RoutedEventArgs e)
        {
            AssignProjectResult isAssigned;
            string message;

            isAssigned = AssignProjectSelected();

            if (isAssigned == AssignProjectResult.Assigned)
            {
                message = "El proyecto fue asignado correctamente";
                DialogWindowManager.ShowSuccessWindow(message);
            }
            else if (isAssigned == AssignProjectResult.NoEnoughSpace)
            {
                DialogWindowManager.ShowNoEnoughProjectSpaceWindow();
            }
            else
            {
                message = "No se pudo asignar el Practicante a un proyecto. Intente de nuevo.";
                DialogWindowManager.ShowErrorWindow(message);
            }
        }

        private AssignProjectResult AssignProjectSelected()
        {
            AssignProjectResult isAssigned;

            BusinessDomain.ProjectsRequest projectsRequestSelected = this.DataContext as BusinessDomain.ProjectsRequest;
            ManageProject manageProjectToAsign = new ManageProject();

            isAssigned = manageProjectToAsign.AssignProject(projectsRequestSelected);

            return isAssigned;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
