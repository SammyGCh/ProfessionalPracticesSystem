/*
        Date: 15/05/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System.Collections.Generic;
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
    public partial class UpdateOrganization : Page
    {
        public UpdateOrganization(LinkedOrganization updatedOrganization)
        {
            InitializeComponent();
            sectorsList.ItemsSource = StatisticsListsManage.GetOrganizationSectors();

            this.DataContext = updatedOrganization;
            sectorsList.Text = updatedOrganization.BelongsTo.Name;
            organizationID.Text = (updatedOrganization.IdLinkedOrganization).ToString();
            organizationName.Text = updatedOrganization.Name;
            organizationState.Text = updatedOrganization.State;
            organizationPhone.Text = updatedOrganization.TelephoneNumber;
            organizationEmail.Text = updatedOrganization.Email;
            organizationCity.Text = updatedOrganization.City;
            organizationAddress.Text = updatedOrganization.Address;
            

        }

        private void CancelUpdate(object sender, RoutedEventArgs e)
        {
            if (DialogWindowManager.ShowConfirmationWindow("¿Seguro que desea salir?") == true)
            {
                NavigationService.GoBack();
            }
        }
        private void UpdateOrganizationClick(object sender, RoutedEventArgs e)
        {
            
            if (DialogWindowManager.ShowConfirmationWindow("¿Está seguro de actualizar la organizacion?") == true)
            {

                
                LinkedOrganization newUpdatedOrganization = new LinkedOrganization
                {
                    IdLinkedOrganization = int.Parse(organizationID.Text),
                    Name = organizationName.Text,
                    State = organizationState.Text,
                    TelephoneNumber = organizationPhone.Text,
                    Email = organizationEmail.Text,
                    City = organizationCity.Text,
                    Address = organizationAddress.Text,
                    BelongsTo = sectorsList.SelectedItem as OrganizationSector
                };

                ManageOrganization manageOrganization = new ManageOrganization();
                bool check = manageOrganization.OrganizationUpdate(newUpdatedOrganization);

                if (check == true)
                {
                    DialogWindowManager.ShowSuccessWindow("Se ha actualizado correctamente");
                    ClearFields();
                    NavigationService.GoBack();
                }
                else
                {
                    DialogWindowManager.ShowErrorWindow("Ha ocurrido un error");
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
