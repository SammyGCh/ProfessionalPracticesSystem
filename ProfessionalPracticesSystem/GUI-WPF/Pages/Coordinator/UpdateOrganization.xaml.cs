/*
        Date: 15/05/2020                              
        Author:Cesar Sergio Martinez Palacios
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
    public partial class UpdateOrganization : Page
    {
        public UpdateOrganization(LinkedOrganization updatedOrganization)
        {
            InitializeComponent();
            OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> sectorList = sectorDao.GetAllOrganizationSectors();
            sectorsList.ItemsSource = sectorList;

            this.DataContext = updatedOrganization;
            sectorsList.Text = updatedOrganization.BelongsTo.Name;

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

                OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
                OrganizationSector sectorOrg = sectorDao.GetOrganizationSectorByName(sectorsList.Text);
                LinkedOrganization newUpdatedOrganization = new LinkedOrganization
                {
                    Name = organizationName.Text,
                    State = organizationState.Text,
                    TelephoneNumber = organizationPhone.Text,
                    Email = organizationEmail.Text,
                    City = organizationCity.Text,
                    Address = organizationAddress.Text,
                    BelongsTo = sectorOrg
                };
                ManageOrganization manageOrganization = new ManageOrganization();
                bool check = manageOrganization.OrganizationUpdate(newUpdatedOrganization);

                if (check == true)
                {
                    DialogWindowManager.ShowSuccessWindow("Se ha actualizado correctamente");
                    
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
    }
}
