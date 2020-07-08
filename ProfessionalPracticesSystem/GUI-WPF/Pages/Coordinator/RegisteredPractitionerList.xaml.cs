/*
        Date: 06/20/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataAccess.Implementation;
using BusinessLogic;
using System.Collections.ObjectModel;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for PractitionerList.xaml
    /// </summary>
    public partial class RegisteredPractitionerList : Page
    {
        private readonly ObservableCollection<BusinessDomain.Practitioner> practitioners;
        private BusinessDomain.Practitioner selectedPractitioner;

        public RegisteredPractitionerList()
        {
            InitializeComponent();

            PractitionerDAO practitionerHandler = new PractitionerDAO();
            List<BusinessDomain.Practitioner> allPractitioners = practitionerHandler.GetAllPractitionerByMatricula();
            
            if (allPractitioners.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
                NavigationService.GoBack();
            }
            else
            {
                practitioners = new ObservableCollection<BusinessDomain.Practitioner>(allPractitioners);
                tableOfPractitioners.ItemsSource = practitioners;
            }
        }

        private void CancelViewList(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DisplayPractitionerData(object sender, RoutedEventArgs e)
        {
            selectedPractitioner = (BusinessDomain.Practitioner)tableOfPractitioners.SelectedItem;

            NavigationService.Navigate(new DisplayPractitioner(selectedPractitioner));
        }

        private void UpdatePractitionerData(object sender, RoutedEventArgs e)
        {
            selectedPractitioner = (BusinessDomain.Practitioner)tableOfPractitioners.SelectedItem;

            NavigationService.Navigate(new UpdatePractitioner(selectedPractitioner));
        }

        private void DeletePractitioner(object sender, RoutedEventArgs e)
        {
            bool confirmDelete = false;
            confirmDelete = DialogWindowManager.ShowConfirmationWindow(
                            "¿Seguro desea eliminar el Practicante seleccionado?");

            if (confirmDelete)
            {
                bool isEliminated = ChangePractitionerStatus();

                if (isEliminated)
                {
                    DialogWindowManager.ShowSuccessWindow(
                    "Practicante eliminado exitosamente");
                }
                else
                {
                    DialogWindowManager.ShowErrorWindow(
                    "Ocurrió un error al intentar eliminar el Practicante. Intente más tarde.");
                }
                NavigationService.GoBack();
            }
        }

        private bool ChangePractitionerStatus()
        {
            bool isStatusChanged = false;
            int selectedPractitionerID = 0;

            ManagePractitioner practitionerManager = new ManagePractitioner();

            selectedPractitioner = (BusinessDomain.Practitioner)tableOfPractitioners.SelectedItem;

            selectedPractitionerID = selectedPractitioner.IdPractitioner;

            isStatusChanged = practitionerManager.DeletePractitioner(selectedPractitionerID);

            return isStatusChanged;
        }
    }
}
