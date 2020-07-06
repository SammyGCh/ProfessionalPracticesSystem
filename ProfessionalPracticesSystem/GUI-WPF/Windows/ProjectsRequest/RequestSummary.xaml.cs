/*
    Date: 22/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Windows;
using BusinessLogic;
using BusinessDomain;
using GUI_WPF.Pages.Coordinator;

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

                GenerateLetters();

                this.Close();
                DeleteProjectsRequest();
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
            
            ManageProject manageProjectToAsign = new ManageProject();
            BusinessDomain.ProjectsRequest projectsRequestSelected = DataContext as BusinessDomain.ProjectsRequest;

            isAssigned = manageProjectToAsign.AssignProject(projectsRequestSelected);

            return isAssigned;
        }

        private Letter GetLetter()
        {
            string coordinatorName = WindowManager.GetCurrentUserName();
            BusinessDomain.ProjectsRequest projectsRequestSelected = DataContext as BusinessDomain.ProjectsRequest;

            Letter assignmentLetter = new Letter
            {
                PractitionerSelected = projectsRequestSelected.RequestedBy,
                ProjectSelected = projectsRequestSelected.ProjectsRequested[0],
                CoordinatorName = coordinatorName
            };

            return assignmentLetter;
        }

        private void GenerateLetters()
        {
            LetterDocumentManager documentManagement = new LetterDocumentManager();
            Letter letter = GetLetter();
            string practitionerName = letter.PractitionerSelected.Names;

            string path = DialogWindowManager.ShowSaveAssigmentLetterWindow(practitionerName);
            string message;
           
            bool isAssigmentLetterGenerated = documentManagement.GenerateAsignmentLetter(letter, path);

            if (!isAssigmentLetterGenerated)
            {
                message = "Ocurrió un error al generar el Oficio de Asignación. Intente más tarde.";
                DialogWindowManager.ShowErrorWindow(message);
            }

            path = DialogWindowManager.ShowSaveAcceptanceLetterWindow(practitionerName);
            bool isAcceptanceLetterGenerated = documentManagement.GenerateAcceptanceLetter(letter, path);

            if (!isAcceptanceLetterGenerated)
            {
                message = "Ocurrió un error al generar el Oficio de Aceptación. Intente más tarde.";
                DialogWindowManager.ShowErrorWindow(message);
            }
        }

        private void DeleteProjectsRequest()
        {
            Requests requestsPage = WindowManager.GetRequestsPage();
            BusinessDomain.ProjectsRequest projectsRequest = DataContext as BusinessDomain.ProjectsRequest;
            requestsPage.DeleteProjectRequestAssigned(projectsRequest);
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
