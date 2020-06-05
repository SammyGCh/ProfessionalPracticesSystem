/*
    Date: 04/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;

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
            
            MessageBoxResult userAnswer =  MessageBox.Show("¿Seguro desea cancelar?", "Cancelar registro", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (userAnswer == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
            
        }

        private LinkedOrganization GetLinkedOrganization()
        {
            object linkedOrg = linkedOrganizationControl.FindName("linkedOrganizations");
            LinkedOrganization linkedOrganization = ((linkedOrg as ComboBox).SelectedItem as LinkedOrganization);

            return linkedOrganization;
        }

        private Project GetProject()
        {
            object responsableName = projectResponsableControl.FindName("responsableName");
            object responsableCharge = projectResponsableControl.FindName("responsableCharge");
            object responsableEmail = projectResponsableControl.FindName("responsableEmail");
            object responsableTelephone = projectResponsableControl.FindName("responsableTelephone");

            object projectName = dataProjectControl.FindName("projectName");
            object projectDescription = dataProjectControl.FindName("projectDescription");
            object projectGeneralGoals = dataProjectControl.FindName("projectGeneralGoals");
            object inmediateGoals = dataProjectControl.FindName("inmediateGoals");
            object mediateGoals = dataProjectControl.FindName("mediateGoals");
            object metology = dataProjectControl.FindName("metology");
            object neededResources = dataProjectControl.FindName("neededResources");
            object responsabilities = dataProjectControl.FindName("responsabilities");
            object duration = dataProjectControl.FindName("duration");
            object directUserNumber = dataProjectControl.FindName("directUserNumber");
            object indirectUserNumber = dataProjectControl.FindName("indirectUserNumber");
            object developmentStage = dataProjectControl.FindName("developmentStages");
            object practitionerNumber = dataProjectControl.FindName("practitionerNumber");
            int practitionerNum = Convert.ToInt32((practitionerNumber as TextBox).Text);

            Project project = new Project
            {
                ResponsableName = (responsableName as TextBox).Text,
                ResponsableCharge = (responsableCharge as TextBox).Text,
                ResponsableEmail = (responsableEmail as TextBox).Text,
                ResponsableTelephone = (responsableTelephone as TextBox).Text,

                Name = (projectName as TextBox).Text,
                GeneralDescription = (projectDescription as TextBox).Text,
                GeneralGoal = (projectGeneralGoals as TextBox).Text,
                InmediateGoals = (inmediateGoals as TextBox).Text,
                MediateGoals = (mediateGoals as TextBox).Text,
                Metology = (metology as TextBox).Text,
                NeededResources = (neededResources as TextBox).Text,
                Responsabilities = (responsabilities as TextBox).Text,
                Duration = (duration as TextBox).Text,
                DirectUsersNumber = (directUserNumber as TextBox).Text,
                IndirectUsersNumber = (indirectUserNumber as TextBox).Text,
                BelongsTo = ((developmentStage as ComboBox).SelectedItem as DevelopmentStage),
                PractitionerNumber = practitionerNum,

                ProposedBy = GetLinkedOrganization(),
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
                MessageBox.Show("Uno o varios campos están vacíos. Por favor ingresa los datos necesarios.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (AreFieldsWrong())
            {
                MessageBox.Show("La información en uno o varios campos es incorrecta. Por favor verifica la información.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool isSaved = SaveProject();

                if (isSaved)
                {
                    MessageBox.Show("Proyecto fue registrado exitosamente.", "Éxito", 
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el proyecto. Intente de nuevo.", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
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
