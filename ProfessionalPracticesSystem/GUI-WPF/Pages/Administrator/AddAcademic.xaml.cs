/*
  Date: 04/15/2020
    Author(s): Ricardo Moguel Sanchez
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
    /// Register a new Academic to the database
    /// </summary>
    public partial class AddAcademic : Page
    {
        private const int ACTIVO = 1;
        private List<String> genderList;
        private List<String> shiftList;

        public AddAcademic()
        {
            InitializeComponent();

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

            AcademicTypeDAO academicTypeHandler = new AcademicTypeDAO();
            List<AcademicType> listOfAcademicTypes = academicTypeHandler.GetAllAcademicTypes();
            if (listOfAcademicTypes.Count == 0)
            {
                DialogWindowManager.ShowErrorWindow(
                "No existen tipos de Academicos. No se puede crear un Académico sin tipo.");
            }
            else
            {
                academicTypeList.ItemsSource = listOfAcademicTypes;
            }  
        }

        private void CancelAddNewAcademic(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow(
                                      "¿Seguro que deseas cancelar el registro?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private Academic GetAcademicData()
        {
            AcademicType academicType = academicTypeList.SelectedItem as AcademicType;

            Academic newAcademic = new Academic()
            {
                PersonalNumber = academicPersonalNumber.Text,
                Names = academicNames.Text,
                LastName = academicSurnames.Text,
                Password = academicPersonalNumber.Text,
                Cubicle = academicCubicle.Text,
                Gender = academicGender.Text,
                Shift = academicShift.Text,
                BelongTo = academicType,
                Status = ACTIVO
            };
            return newAcademic;
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
                academicGender.SelectedItem == null || academicTypeList.SelectedItem == null
                ||
                academicShift.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private void AddNewAcademic(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else if (IsAcademicCountFull())
            {
                DialogWindowManager.ShowErrorWindow(
                "Error. Se llego al limite del tipo de academico. Debe eliminar academicos antes de crear uno nuevo.");
            }
            else if(!IsInValidUserName())
            {
                bool isSaved = SaveAcademic();

                CleanTextFields();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow(
                    "El Académico fue registrado exitosamente."); 
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }

                NavigationService.Navigate(new AdministratorHome());
            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
        }

        private bool SaveAcademic()
        {
            bool isAcademicSaved = false;

            Academic newAcademic = GetAcademicData();

            ManageAcademic managePractitioner = new ManageAcademic();

            isAcademicSaved = managePractitioner.AddAcademic(newAcademic);

            return isAcademicSaved;

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
            academicTypeList.SelectedItem = null;
            academicCubicle.Clear();
            academicShift.SelectedItem = null;
        }

        private bool IsAcademicCountFull()
        {
            bool isFull = false;

            AcademicType academicType = academicTypeList.SelectedItem as AcademicType;

            AcademicDAO academicHandler = new AcademicDAO();

            isFull = academicHandler.ActiveAcademicCountFull(academicType.IdAcademicType);

            return isFull;
        }
    }
}
