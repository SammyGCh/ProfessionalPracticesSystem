/*
    Date: 20/06/2020
    Author(s) : Ricardo Moguel Sanchez
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Administrator
{
    /// <summary>
    /// Lógica de interacción para UpdateAcademic.xaml
    /// </summary>
    public partial class UpdateAcademic : Page
    {
        private List<String> genderList;
        private List<String> shiftList;
        private readonly int currentAcademicStatus;
        private Academic currentAcademic;
        private const int ACTIVE_STATUS = 1;

        public UpdateAcademic(Academic academicToUpdate)
        {
            InitializeComponent();

            currentAcademic = academicToUpdate;
            genderList = new List<String>
            {
                "Masculino",
                "Femenino",
            };

            academicGender.ItemsSource = genderList;

            shiftList = new List<String>
            {
                "Matutino",
                "Vespertino",
            };

            academicShift.ItemsSource = shiftList;

            this.DataContext = currentAcademic;

            academicID.Text = currentAcademic.IdAcademic.ToString();
            academicNames.Text = currentAcademic.Names;
            academicSurnames.Text = currentAcademic.LastName;
            academicPersonalNumber.Text = currentAcademic.PersonalNumber;
            academicCubicle.Text = currentAcademic.Cubicle;
            academicGender.Text = currentAcademic.Gender;
            academicType.Text = currentAcademic.BelongTo.AcademicTypeName;
            academicShift.Text = currentAcademic.Shift;
            currentAcademicStatus = currentAcademic.Status;
        }

        private void CancelUpdateAcademic(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar la actualización?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private Academic GetAcademicData()
        {
            Academic updatedAcademic = new Academic()
            {
                IdAcademic = int.Parse(academicID.Text),
                PersonalNumber = academicPersonalNumber.Text,
                Names = academicNames.Text,
                LastName = academicSurnames.Text,
                Cubicle = academicCubicle.Text,
                Gender = academicGender.Text,
                Shift = academicShift.Text,
                Status = currentAcademicStatus
            };

            return updatedAcademic;
        }

        private bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                academicData.Children.OfType<StackPanel>().Any(
                    academicSections => academicSections.Children.OfType<TextBox>().Any(
                        practitionerFields => String.IsNullOrWhiteSpace(practitionerFields.Text)
                    )
                )
                ||
                academicGender.SelectedItem == null || academicShift.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private void UpdateAcademicData(object sender, RoutedEventArgs e)
        {
            ManageAcademic academicManager = new ManageAcademic();
            

            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else if(!IsInValidUserName())
            {
                bool isUpdated = SaveAcademicUpdate();

                if (isUpdated)
                {
                    DialogWindowManager.ShowSuccessWindow("El Académico fue actualizado exitosamente.");
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }

                CleanTextFields();
                NavigationService.Navigate(new AdministratorHome());
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Error. Uno de los campos ingresados esta inválido. Verifique los campos");
            }
        }

        private bool SaveAcademicUpdate()
        {
            bool isUpdateSaved = false;

            Academic academicUpdate = GetAcademicData();

            ManageAcademic managePractitioner = new ManageAcademic();

            isUpdateSaved = managePractitioner.UpdateAcademic(academicUpdate);

            return isUpdateSaved;
        }

        private void IsPersonName(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsPersonName(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void IsANumber(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsANumber(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private bool IsInValidUserName()
        {
            bool isWrong = false;

            String stringToValidate = academicPersonalNumber.Text;

            if (!ValidatorText.IsUserName(stringToValidate))
            {
                isWrong = true;
            }

            return isWrong;
        }

        private void CleanTextFields()
        {
            academicPersonalNumber.Clear();
            academicNames.Clear();
            academicSurnames.Clear();
            academicGender.SelectedItem = null;
            academicCubicle.Clear();
            academicShift.SelectedItem = null;
        }

    }
}
