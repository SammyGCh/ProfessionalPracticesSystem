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
        private List<string> genderList;

        public AddAcademic()        
        {
            InitializeComponent();

            genderList = new List<string>
            {
                "Masculino",
                "Femenino",
            };
            academicGender.ItemsSource = genderList;

            AcademicTypeDAO academicTypeHandler = new AcademicTypeDAO();
            List<AcademicType> ListOfAcademicTypes = academicTypeHandler.GetAllAcademicTypes();

            if(ListOfAcademicTypes.Count == 0)
            {
                DialogWindowManager.ShowErrorWindow("No existen tipos de Academicos. No se puede crear un Académico sin tipo.");
                NavigationService.GoBack();
            }
            else
            {
                academicTypeList.ItemsSource = ListOfAcademicTypes;
            }
        }

        private void CancelAddNewAcademic(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar el registro?");

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
                    DialogWindowManager.ShowSuccessWindow("El Académico fue registrado exitosamente.");
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

        private void CleanTextFields()
        {
            academicPersonalNumber.Clear();
            academicNames.Clear();
            academicSurnames.Clear();
            academicGender.SelectedItem = null;
            academicTypeList.SelectedItem = null;
        }

        private void IsPersonalNumber(object sender, TextCompositionEventArgs e)
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
    }
}
