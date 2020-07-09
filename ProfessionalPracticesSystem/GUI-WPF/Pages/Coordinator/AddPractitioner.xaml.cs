/*
        Date: 15/06/2020                              
        Author:Ricardo Moguel Sanchez
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
namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for AddPractitioner.xaml
    /// </summary>
    public partial class AddPractitioner : Page
    {
        private List<string> genderList;

        public AddPractitioner()
        {
            InitializeComponent();

            genderList = new List<string>
            {
                "Masculino",
                "Femenino",
            };
            practitionerGender.ItemsSource = genderList;

            IndigenousLanguageDAO practitionerLanguageHandler = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguageList = practitionerLanguageHandler.GetAllIndigenousLanguages();
            practitionerLanguageList.ItemsSource = indigenousLanguageList;

            AcademicDAO academicHandler = new AcademicDAO();
            List<Academic> academicList = academicHandler.GetAllActiveProfessors();
            practitionerAcademicList.ItemsSource = academicList;

            ScholarPeriodDAO schoolPeriodHandler = new ScholarPeriodDAO();
            List<ScholarPeriod> schoolPeriodList = schoolPeriodHandler.GetAllScholarPeriods();
            practitionerSchoolPeriodList.ItemsSource = schoolPeriodList;
        }

        private void CancelAddNewPractitioner(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow(
                                      "¿Seguro que deseas cancelar el registro?");
            
            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private BusinessDomain.Practitioner GetPractitonerData()
        {
            IndigenousLanguage practitionerLanguage = practitionerLanguageList.SelectedItem as IndigenousLanguage;
            Academic linkedAcademic = practitionerAcademicList.SelectedItem as Academic;
            ScholarPeriod practitionerPeriod = practitionerSchoolPeriodList.SelectedItem as ScholarPeriod;

            BusinessDomain.Practitioner newPractitioner = new BusinessDomain.Practitioner
            {
                Matricula = practitionerEnrollment.Text,
                Password = practitionerEnrollment.Text,
                Names = practitionerNames.Text,
                LastName = practitionerSurnames.Text,
                Gender = practitionerGender.Text,
                Speaks = practitionerLanguage,
                Instructed = linkedAcademic,
                ScholarPeriod = practitionerPeriod
            };
            return newPractitioner;
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

        private void AddNewPractitioner(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else if (!IsInValidUserName())
            {
                bool isSaved = SavePractitioner();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow(
                    "El practicante fue registrado exitosamente.");
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }

                NavigationService.Navigate(new CoordinatorHome());
            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
        }   

        private bool SavePractitioner()
        {
            bool isPractitionerSaved = false;
            
            BusinessDomain.Practitioner newPractitioner = GetPractitonerData();

            ManagePractitioner managePractitioner = new ManagePractitioner();

            isPractitionerSaved = managePractitioner.AddPractitioner(newPractitioner);

            return isPractitionerSaved;
            
        }

        private bool IsInValidUserName()
        {
            bool isWrong = false;

            String stringToValidate = practitionerEnrollment.Text;

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