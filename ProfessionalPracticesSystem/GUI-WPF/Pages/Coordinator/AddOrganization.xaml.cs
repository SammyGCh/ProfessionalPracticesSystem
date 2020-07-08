/*
        Date: 15/05/2020                              
        Author:Cesar Sergio Martinez Palacios
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for AddOrganization.xaml
    /// </summary>
    
    public partial class AddOrganization : Page
    {
        public AddOrganization()
        {
            InitializeComponent();

            sectorsList.ItemsSource = StatisticsListsManage.GetOrganizationSectors();
        }

        private void CancelAdd(object sender, RoutedEventArgs e)
        {
            
            if (DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas salir?") == true)
            {
                NavigationService.GoBack();
            }
        }

        private void InputTextChanged(Object sender, RoutedEventArgs e)
        {
            if (organizationName.Text.Length == 0 || organizationState.Text.Length == 0 ||
                organizationPhone.Text.Length == 0 || organizationEmail.Text.Length == 0 ||
                organizationCity.Text.Length == 0 || organizationAddress.Text.Length == 0 || sectorsList.Text.Length == 0)
            {
                registerButton.IsEnabled = false;
            }
            else
            {
                registerButton.IsEnabled = true;
            }
        }

        private bool IsEmpty()
        {
            bool empyFields = true;
            if (organizationName.Text.Length == 0 || organizationState.Text.Length == 0 ||
                organizationPhone.Text.Length == 0 || organizationEmail.Text.Length == 0 ||
                organizationCity.Text.Length == 0 || organizationAddress.Text.Length == 0 || sectorsList.Text.Length == 0)
            {
                empyFields = true;
            }
            else
            {
                empyFields = false;
            }

            return empyFields;
        }
        private void AddNewOrganization(object sender, RoutedEventArgs e)
        {
            if (IsEmpty() == true)
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else
            {

                if (DialogWindowManager.ShowConfirmationWindow("¿Está seguro de guardar la organizacion?") == true)
                {

                    LinkedOrganization newOrganization = new LinkedOrganization
                    {
                        Name = organizationName.Text,
                        State = organizationState.Text,
                        TelephoneNumber = organizationPhone.Text,
                        Email = organizationEmail.Text,
                        City = organizationCity.Text,
                        Address = organizationAddress.Text,
                        BelongsTo = sectorsList.SelectedItem as OrganizationSector
                    };

                    ManageOrganization manageOrganization = new ManageOrganization();

                    bool check = manageOrganization.OrganizationSave(newOrganization);

                    if (check == true)
                    {
                        DialogWindowManager.ShowSuccessWindow("Se ha guardado con exito la organizacion");
                        ClearFields();
                        NavigationService.GoBack();
                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("Ha ocurrido un error al intentar guardar. Porfavor intente mas tarde");
                    }

                }
            }

        }

        private void IsTelephoneNumber(object sender, TextCompositionEventArgs e)
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

        private void ClearFields()
        {
            organizationName.Clear();
            organizationState.Clear();
            organizationPhone.Clear();
            organizationEmail.Clear();
            organizationCity.Clear();
            organizationAddress.Clear();
        }
    }
}
