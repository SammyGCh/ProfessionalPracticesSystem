/*
        Date: 25/06/2020                              
        Author:Ricardo Moguel Sanchez
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

        public UpdatePractitioner(String matricula)
        {
            InitializeComponent();

            genderList = new List<string>
            {
                "Masculino",
                "Femenino",
                "Otro"
            };
            practitionerGender.ItemsSource = genderList;

            PractitionerDAO practitionerDAO = new PractitionerDAO();
            BusinessDomain.Practitioner updatedPractitioner = new BusinessDomain.Practitioner();

            IndigenousLanguageDAO practitionerLanguageDao = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguageList = practitionerLanguageDao.GetAllIndigenousLanguages();
            practitionerLanguageList.ItemsSource = indigenousLanguageList;

            AcademicDAO academicDao = new AcademicDAO();
            List<Academic> academicList = academicDao.GetAllAcademic();
            practitionerAcademicList.ItemsSource = academicList;

            ScholarPeriodDAO schoolPeriodDao = new ScholarPeriodDAO();
            List<ScholarPeriod> schoolPeriodList = schoolPeriodDao.GetAllScholarPeriods();
            practitionerSchoolPeriodList.ItemsSource = schoolPeriodList;

            this.DataContext = updatedPractitioner;
            practitionerGender.Text = updatedPractitioner.Gender;
            practitionerLanguageList.Text = updatedPractitioner.Speaks.IndigenousLanguageName;
            practitionerAcademicList.Text = updatedPractitioner.Instructed.LastName;
            practitionerSchoolPeriodList.Text = updatedPractitioner.ScholarPeriod.Name;

            practitionerNames.Text = updatedPractitioner.Names;
            practitionerSurnames.Text = updatedPractitioner.LastName;
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
                Names = practitionerNames.Text,
                LastName = practitionerSurnames.Text,
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
            else
            {
                bool isSaved = SaveUpdatedPractitioner();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow("El practicante fue actualizado exitosamente.");

                    NavigationService.GoBack();
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }
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
            practitionerEnrollment.Clear();
            practitionerNames.Clear();
            practitionerSurnames.Clear();
            practitionerGender.SelectedItem = null;
            practitionerLanguageList.SelectedItem = null;
            practitionerAcademicList.SelectedItem = null;
            practitionerSchoolPeriodList.SelectedItem = null;
        }
    }
}
