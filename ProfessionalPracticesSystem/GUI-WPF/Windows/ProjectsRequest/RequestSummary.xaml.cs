/*
    Date: 22/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Windows;
using BusinessLogic;
using BusinessDomain;
using System.Windows.Forms;
using GUI_WPF.Windows;

namespace GUI_WPF.Windows.ProjectsRequest
{
    /// <summary>
    /// Lógica de interacción para ProjectSummary.xaml
    /// </summary>
    public partial class RequestSummary : Window
    {
        private BusinessDomain.ProjectsRequest projectsRequestSelected;

        public RequestSummary()
        {
            InitializeComponent();
        }

        private void AssignProject(object sender, RoutedEventArgs e)
        {
            /*
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
            */

            string path = PathToSave();

            DocumentManagement documentManagement = new DocumentManagement();

            if (documentManagement.GenerateAsignmentLetter(GetAssignmentLetter(), path))
            {
                DialogWindowManager.ShowSuccessWindow("Sí se generó");
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("nelpas");
            }
        }

        private AssignProjectResult AssignProjectSelected()
        {
            AssignProjectResult isAssigned;

            projectsRequestSelected = this.DataContext as BusinessDomain.ProjectsRequest;
            ManageProject manageProjectToAsign = new ManageProject();

            isAssigned = manageProjectToAsign.AssignProject(projectsRequestSelected);

            return isAssigned;
        }

        private AssignmentLetter GetAssignmentLetter()
        {
            string coordinatorName = WindowManager.GetCurrentUserName();

            //Esto se debe quitar
            projectsRequestSelected = this.DataContext as BusinessDomain.ProjectsRequest;
            AssignmentLetter assignmentLetter = new AssignmentLetter
            {
                PractitionerAssigned = projectsRequestSelected.RequestedBy,
                ProjectAssigned = projectsRequestSelected.ProjectsRequested[0],
                CoordinatorName = coordinatorName
            };

            return assignmentLetter;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private String PathToSave()
        {
            SaveFileDialog saveWindow = new SaveFileDialog();
            saveWindow.Filter = "PDF Document|*.pdf";
            saveWindow.Title = "Selecciona ruta de guardado";
            saveWindow.ShowDialog();

            return saveWindow.FileName;
        }
    }
}
