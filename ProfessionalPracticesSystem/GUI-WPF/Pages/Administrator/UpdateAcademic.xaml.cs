/*
    Date: 20/06/2020
    Author(s) : Ricardo Moguel Sanchez
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
    /// Lógica de interacción para UpdateAcademic.xaml
    /// </summary>
    public partial class UpdateAcademic : Page
    {
        private List<String> genderList;
        private List<String> shiftList;
        private const int ACTIVO = 1;
        private const string CONFIRM_MESSAGE = "¿Seguro que deseas cancelar la actualización?";
        private const string SUCCESS_MESSAGE = "El Académico fue actualizado exitosamente.";
        private const string NO_ACADEMIC_MESSAGE = "No existen Académicos. No se puede actualizar un Académico.";

        public UpdateAcademic(Academic academicToUpdate)
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
            List<AcademicType> allAcademicTypes = academicTypeHandler.GetAllAcademicTypes();
            academicTypeList.ItemsSource = allAcademicTypes;

            this.DataContext = academicToUpdate;

            academicNames.Text = academicToUpdate.Names;
            academicSurnames.Text = academicToUpdate.LastName;
            academicPersonalNumber.Text = academicToUpdate.PersonalNumber;
            academicCubicle.Text = academicToUpdate.Cubicle;
        }

        private void CancelUpdateAcademic(object sender, RoutedEventArgs e)
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

            Academic updatedAcademic = new Academic()
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
                academicGender.SelectedItem == null || academicTypeList.SelectedItem == null
                ||
                academicShift.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private void UpdateAcademicData(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else
            {
                bool isUpdated = SaveAcademicUpdate();

                if (isUpdated)
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
