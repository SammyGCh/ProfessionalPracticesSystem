/*
        Date: 25/06/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for UpdatePractitioner.xaml
    /// </summary>
    public partial class UpdatePractitioner : Page
    {
        private List<string> genderList;

        public UpdatePractitioner(BusinessDomain.Practitioner selectedPractitioner)
        {
            InitializeComponent();
            
            this.DataContext = selectedPractitioner;

            genderList = new List<string>
            {
                "Masculino",
                "Femenino",
                "Otro"
            };
            practitionerGender.ItemsSource = genderList;

            IndigenousLanguageDAO practitionerLanguageHandler = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguageList = practitionerLanguageHandler.GetAllIndigenousLanguages();
            practitionerLanguageList.ItemsSource = indigenousLanguageList;

            AcademicDAO professorHandler = new AcademicDAO();
            List<Academic> professorList = professorHandler.GetAllActiveProfessors();
            practitionerAcademicList.ItemsSource = professorList;

            ScholarPeriodDAO schoolPeriodHandler = new ScholarPeriodDAO();
            List<ScholarPeriod> schoolPeriodList = schoolPeriodHandler.GetAllScholarPeriods();
            practitionerSchoolPeriodList.ItemsSource = schoolPeriodList;

            practitionerID.Text = selectedPractitioner.IdPractitioner.ToString();
            practitionerNames.Text = selectedPractitioner.Names;
            practitionerSurname.Text = selectedPractitioner.LastName;
            practitionerMatricula.Text = selectedPractitioner.Matricula;
            practitionerGender.Text = selectedPractitioner.Gender;
            practitionerLanguageList.Text = selectedPractitioner.Speaks.IndigenousLanguageName;
            practitionerAcademicList.Text = selectedPractitioner.Instructed.ToString();
            practitionerSchoolPeriodList.Text = selectedPractitioner.ScholarPeriod.Name;
        }

        private void CancelUpdatePractitioner(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar actualizar el practicante?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private BusinessDomain.Practitioner GetUpdatedPractitonerData()
        {
            IndigenousLanguage practitionerLanguage = practitionerLanguageList.SelectedItem as IndigenousLanguage;
            Academic linkedAcademic = practitionerAcademicList.SelectedItem as Academic;
            ScholarPeriod practitionerPeriod = practitionerSchoolPeriodList.SelectedItem as ScholarPeriod;

            BusinessDomain.Practitioner updatedNewPractitioner = new BusinessDomain.Practitioner
            {
                IdPractitioner = int.Parse(practitionerID.Text),
                Matricula = practitionerMatricula.Text,
                Names = practitionerNames.Text,
                LastName = practitionerSurname.Text,
                Gender = practitionerGender.Text,
                Speaks = practitionerLanguage,
                Instructed = linkedAcademic,
                ScholarPeriod = practitionerPeriod
            };

            return updatedNewPractitioner;
        }

        private bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                practitionerData.Children.OfType<StackPanel>().Any(
                    practitionerSections => practitionerSections.Children.OfType<TextBox>().Any(
                        practitionerFields => String.IsNullOrWhiteSpace(practitionerFields.Text)
                    )
                )
                ||
                practitionerLanguageList.SelectedItem == null || practitionerAcademicList.SelectedItem == null ||
                practitionerSchoolPeriodList.SelectedItem == null || practitionerGender.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private void UpdatePractitionerData(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else if(!IsInValidUserName())
            {
                bool isSaved = SaveUpdatedPractitioner();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow(
                    "El practicante fue actualizado exitosamente.");  
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }

                NavigationService.Navigate(new RegisteredPractitionerList());
            }
            else
            {
                DialogWindowManager.ShowErrorWindow(
                "Error. La matricula ingresada es inválida. Debe iniciar con S capital y contener 8 digitos");
            }
        }

        private bool SaveUpdatedPractitioner()
        {
            bool isUpdatedPractitionerSaved = false;

            BusinessDomain.Practitioner changedPractitioner = GetUpdatedPractitonerData();

            ManagePractitioner managePractitioner = new ManagePractitioner();

            isUpdatedPractitionerSaved = managePractitioner.UpdatePractitioner(changedPractitioner);

            return isUpdatedPractitionerSaved;

        }

        private bool IsInValidUserName()
        {
            bool isWrong = false;

            String stringToValidate = practitionerMatricula.Text;

            if (!ValidatorText.IsUserName(stringToValidate))
            {
                isWrong = true;
            }

            return isWrong;
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
    }
}
