/*
        Date: 06/20/2020                              
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
using DataAccess.Implementation;
using BusinessDomain;
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
            PractitionerDAO practitionerDAO = new PractitionerDAO();
            List<BusinessDomain.Practitioner> allPractitioners = practitionerDAO.GetAllPractitionerByMatricula();
            
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
            confirmDelete = DialogWindowManager.ShowConfirmationWindow("¿Seguro desea eliminar el Practicante seleccionado?");

            if (confirmDelete)
            {
                bool isEliminated = ChangePractitionerStatus();

                if (isEliminated)
                {
                    DialogWindowManager.ShowSuccessWindow("Practicante eliminado exitosamente");
                }
                else
                {
                    DialogWindowManager.ShowErrorWindow("Ocurrió un error al intentar eliminar el Practicante. Intente más tarde.");
                }
                NavigationService.GoBack();
            }
        }

        private bool ChangePractitionerStatus()
        {
            bool isStatusChanged = false;
            ManagePractitioner practitionerManager = new ManagePractitioner();
            int selectedPractitionerID = 0;

            selectedPractitioner = (BusinessDomain.Practitioner)tableOfPractitioners.SelectedItem;
            selectedPractitionerID = selectedPractitioner.IdPractitioner;

            isStatusChanged = practitionerManager.DeletePractitioner(selectedPractitionerID);

            return isStatusChanged;
        }

        private void DeleteFromList()
        {
            int deletedPractitionerIndex = tableOfPractitioners.SelectedIndex;

            practitioners.RemoveAt(deletedPractitionerIndex);
        }
    }
}
