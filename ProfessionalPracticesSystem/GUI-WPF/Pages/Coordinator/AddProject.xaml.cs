/*
    Date: 04/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para AddProject.xaml
    /// </summary>
    public partial class AddProject : Page
    {

        public AddProject()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            string confirmMessage = "¿Seguro desea cancelar el registro del proyecto?";

            bool wantToCancel = DialogWindowManager.ShowConfirmationWindow(confirmMessage);

            if (wantToCancel)
            {
                NavigationService.GoBack();
            }
        }

        private Project GetProject()
        {
            Project projectResponsableData = projectResponsableControl.GetProjectResponsableData();
            Project projectData = dataProjectControl.GetProjectData();

            Project project = new Project
            {
                ResponsableName = projectResponsableData.ResponsableName,
                ResponsableCharge = projectResponsableData.ResponsableCharge,
                ResponsableEmail = projectResponsableData.ResponsableEmail,
                ResponsableTelephone = projectResponsableData.ResponsableTelephone,

                Name = projectData.Name,
                GeneralDescription = projectData.GeneralDescription,
                GeneralGoal = projectData.GeneralGoal,
                InmediateGoals = projectData.InmediateGoals,
                MediateGoals = projectData.MediateGoals,
                Metology = projectData.Metology,
                NeededResources = projectData.NeededResources,
                Responsabilities = projectData.Responsabilities,
                Duration = projectData.Duration,
                DirectUsersNumber = projectData.DirectUsersNumber,
                IndirectUsersNumber = projectData.IndirectUsersNumber,
                BelongsTo = projectData.BelongsTo,
                PractitionerNumber = projectData.PractitionerNumber,

                ProposedBy = linkedOrganizationControl.GetLinkedOrganizationSelected(),
                ProjectActivities = projectActivityControl.GetProjectActivities()
            };

            return project;
        }

        private bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (linkedOrganizationControl.IsNotSelected() ||
                dataProjectControl.AreFieldsEmpty() ||
                projectResponsableControl.AreFieldsEmpty() ||
                dataProjectControl.AreFieldsEmpty() ||
                projectActivityControl.AreThereNotActivities()
             )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private bool AreFieldsWrong()
        {
            bool areWrong = false;

            if (projectResponsableControl.AreFieldsWrong() ||
                dataProjectControl.AreFieldsWrong()    
            )
            {
                areWrong = true;
            }

            return areWrong;
        }

        private void AddNewProject(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else if (AreFieldsWrong())
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
            else
            {
                bool isSaved = SaveProject();
                string messageWindow;

                if (isSaved)
                {
                    messageWindow = "El proyecto fue registrado exitosamente.";

                    DialogWindowManager.ShowSuccessWindow(messageWindow);

                    ClearFields();
                }
                else
                {
                    messageWindow = "No se pudo registrar el proyecto. Intente de nuevo.";

                    DialogWindowManager.ShowErrorWindow(messageWindow);
                }
            }

        }

        private bool SaveProject()
        {
            bool isAdded;
            ManageProject manageProject = new ManageProject();

            Project newProject = GetProject();

            isAdded = manageProject.SaveProject(newProject);

            return isAdded;
        }

        private void ClearFields()
        {
            linkedOrganizationControl.UnSelectLinkedOrganization();
            dataProjectControl.ClearFields();
            projectResponsableControl.ClearFields();
            dataProjectControl.ClearFields();
            projectActivityControl.DeleteActivities();
        }
    }
}
