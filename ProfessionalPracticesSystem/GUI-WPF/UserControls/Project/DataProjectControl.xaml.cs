/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessDomain;
using DataAccess.Implementation;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para DataProjectControl.xaml
    /// </summary>
    public partial class DataProjectControl : UserControl
    {
        public DataProjectControl()
        {
            InitializeComponent();

            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> allDevelopmentStages = developmentStageDao.GetAllDevelopmentStages();

            developmentStages.ItemsSource = allDevelopmentStages;
        }

        public void UpdateProjectData()
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
                bool isUpdated = UpdateData();
                string messageWindow;

                if (isUpdated)
                {
                    messageWindow = "El proyecto fue actualizado existosamente";

                    DialogWindowManager.ShowSuccessWindow(messageWindow);
                }
                else
                {
                    messageWindow = "No se pudo actualizar la información del proyecto. Intente de nuevo.";

                    DialogWindowManager.ShowErrorWindow(messageWindow);
                }
            }
        }

        private bool UpdateData()
        {
            bool isUpdated;
            ManageProject manageProject = new ManageProject();
            int idProjectToUpdated = (DataContext as BusinessDomain.Project).IdProject;

            BusinessDomain.Project projectToUpdate = GetProjectData();
            projectToUpdate.IdProject = idProjectToUpdated;

            isUpdated = manageProject.UpdateProjectData(projectToUpdate);

            return isUpdated;
        }

        public BusinessDomain.Project GetProjectData()
        {
            int practitionerNum = Int32.Parse(practitionerNumber.Text); 

            BusinessDomain.Project project = new BusinessDomain.Project
            {
                Name = projectName.Text,
                GeneralDescription = projectDescription.Text,
                GeneralGoal = projectGeneralGoals.Text,
                InmediateGoals = inmediateGoals.Text,
                MediateGoals = mediateGoals.Text,
                Metology = metology.Text,
                NeededResources = neededResources.Text,
                Responsabilities = responsabilities.Text,
                Duration = duration.Text,
                DirectUsersNumber = directUserNumber.Text,
                IndirectUsersNumber = indirectUserNumber.Text,
                PractitionerNumber = practitionerNum,
                BelongsTo = (developmentStages.SelectedItem as DevelopmentStage)
            };

            return project;
        }

        public bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                projectData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => String.IsNullOrWhiteSpace(projectFields.Text)
                    )
                )
                ||
                developmentStages.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        public void ClearFields()
        {
            projectName.Clear();
            projectDescription.Clear();
            projectGeneralGoals.Clear();
            inmediateGoals.Clear();
            mediateGoals.Clear();
            metology.Clear();
            neededResources.Clear();
            responsabilities.Clear();
            duration.Clear();
            directUserNumber.Clear();
            indirectUserNumber.Clear();
            practitionerNumber.Clear();
            developmentStages.SelectedItem = null;
        }

        private void ValidateText(object sender, TextChangedEventArgs e)
        {
            string textToValidate = ((TextBox)sender).Text;

            if (ValidatorText.IsTextRight(textToValidate))
            {
                ((TextBox)sender).BorderBrush = Brushes.Green;
            }
            else
            {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }

        private void ValidateNumber(object sender, TextChangedEventArgs e)
        {
            string number = ((TextBox)sender).Text;

            if (ValidatorText.IsANumber(number))
            {
                ((TextBox)sender).BorderBrush = Brushes.Green;
            }
            else
            {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }

        public bool AreFieldsWrong()
        {
            bool areWrong = true;

            if(AreTextFieldsRight() && AreNumberFieldsRight())
            {
                areWrong = false;
            }

            return areWrong;
        }

        private bool AreTextFieldsRight()
        {
            bool areRight = false;

            if (
                ValidatorText.IsTextRight(projectName.Text) &&
                ValidatorText.IsTextRight(projectDescription.Text) &&
                ValidatorText.IsTextRight(projectGeneralGoals.Text) &&
                ValidatorText.IsTextRight(inmediateGoals.Text) &&
                ValidatorText.IsTextRight(mediateGoals.Text) &&
                ValidatorText.IsTextRight(metology.Text) &&
                ValidatorText.IsTextRight(neededResources.Text) &&
                ValidatorText.IsTextRight(responsabilities.Text)
            )
            {
                areRight = true;
            }
           
            return areRight;
        }

        private bool AreNumberFieldsRight()
        {
            bool areRight = false;

            if (ValidatorText.IsANumber(duration.Text) &&
                ValidatorText.IsANumber(directUserNumber.Text) &&
                ValidatorText.IsANumber(indirectUserNumber.Text) &&
                ValidatorText.IsANumber(practitionerNumber.Text)
            )
            {
                areRight = true;
            }

            return areRight;
        }
    }
}
