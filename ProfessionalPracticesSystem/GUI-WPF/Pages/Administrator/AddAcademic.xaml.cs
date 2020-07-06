/*
  Date: 04/15/2020
    Author(s): Ricardo Moguel Sanchez
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
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
        private const string CONFIRM_MESSAGE = "¿Seguro que deseas cancelar el registro?";
        private const string SUCCESS_MESSAGE = "El Académico fue registrado exitosamente.";
        private const string NO_ACADEMIC_TYPE_MESSAGE = "No existen tipos de Academicos. No se puede crear un Académico sin tipo.";

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

            if(listOfAcademicTypes.Count == 0)
            {
                DialogWindowManager.ShowErrorWindow(NO_ACADEMIC_TYPE_MESSAGE);
                NavigationService.GoBack();
            }
            else
            {
                academicTypeList.ItemsSource = listOfAcademicTypes;
            }
        }

        private void CancelAddNewAcademic(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow(CONFIRM_MESSAGE);

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
            else
            {
                bool isSaved = SaveAcademic();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow(SUCCESS_MESSAGE);
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }
                NavigationService.GoBack();
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
            if (!ValidatorText.IsPersonName(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void IsUserName(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsUserName(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
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
    }
}
