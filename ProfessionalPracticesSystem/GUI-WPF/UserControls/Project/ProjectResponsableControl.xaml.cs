/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using BusinessLogic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GUI_WPF.Windows;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para ProjectResponsableControl.xaml
    /// </summary>
    public partial class ProjectResponsableControl : UserControl
    {
        private const int MINIMUM_LENGHT = 10;

        public ProjectResponsableControl()
        {
            InitializeComponent();
        }

        public bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                projectResponsableData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => String.IsNullOrWhiteSpace(projectFields.Text)
                    )
                )
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        public void UpdateProjectResponsable()
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
                bool isUpdated = UpdateProjectResponsableData();
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

        private bool UpdateProjectResponsableData()
        {
            bool isUpdated;
            ManageProject manageProject = new ManageProject();
            int idProjectToUpdate = (DataContext as BusinessDomain.Project).IdProject;

            BusinessDomain.Project projectResponsableData = GetProjectResponsableData();
            projectResponsableData.IdProject = idProjectToUpdate;

            isUpdated = manageProject.UpdateProjectResponsableData(projectResponsableData);

            return isUpdated;
        }

        public BusinessDomain.Project GetProjectResponsableData()
        {
            BusinessDomain.Project projectResponsableData = new BusinessDomain.Project
            {
                ResponsableName = responsableName.Text,
                ResponsableCharge = responsableCharge.Text,
                ResponsableEmail = responsableEmail.Text,
                ResponsableTelephone = responsableTelephone.Text
            };

            return projectResponsableData;
        }

        public void ClearFields()
        {
            responsableName.Clear();
            responsableCharge.Clear();
            responsableEmail.Clear();
            responsableTelephone.Clear();
        }

        private void IsEmail(object sender, RoutedEventArgs e)
        {
            if (ValidatorText.IsEmail(responsableEmail.Text))
            {
                responsableEmail.BorderBrush = Brushes.Green;
            }
            else
            {
                responsableEmail.BorderBrush = Brushes.Red;
            }
        }

        private void IsName(object sender, RoutedEventArgs e)
        {
            if (IsResponsableNameRight())
            {
                responsableName.BorderBrush = Brushes.Green;
            }
            else
            {
                responsableName.BorderBrush = Brushes.Red;
            }
        }

        private void IsTelephoneNumber(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsTelephoneNumber(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
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

        public bool AreFieldsWrong()
        {
            bool areWrong = true;

            if (IsResponsableNameRight() && IsResponsableEmailRight() && IsResponsableChargeRight())
            {
                areWrong = false;
            }

            return areWrong;
        }

        private bool IsResponsableNameRight()
        {
            return ValidatorText.IsPersonName(responsableName.Text) && responsableName.Text.Length > MINIMUM_LENGHT;
        }

        private bool IsResponsableEmailRight()
        {
            bool isEmail;
            string emailToValidate = responsableEmail.Text;

            isEmail = ValidatorText.IsEmail(emailToValidate);

            return isEmail; 
        }

        private bool IsResponsableChargeRight()
        {
            bool isRight;
            string textToValidate = responsableCharge.Text;

            isRight = ValidatorText.IsTextRight(textToValidate);

            return isRight;
        }
    }
}
