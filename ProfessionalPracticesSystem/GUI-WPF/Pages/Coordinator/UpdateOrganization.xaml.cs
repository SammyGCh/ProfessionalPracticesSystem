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

namespace GUI_WPF.Pages.Coordinator
{
    public partial class UpdateOrganization : Page
    {
        public UpdateOrganization(String name)
        {
            InitializeComponent();
            OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> sectorList = sectorDao.GetAllOrganizationSectors();
            sectorsList.ItemsSource = sectorList;

            LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
            LinkedOrganization updatedOrganization = linkedOrganizationDAO.GetLinkedOrganizationByName(name);

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
            MessageBoxResult confirmation = System.Windows.MessageBox.Show("¿Seguro que deseas salir?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
        private void UpdateOrganizationClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult question = System.Windows.MessageBox.Show("¿Está seguro de actualizar la organizacion?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (question == MessageBoxResult.Yes)
            {

                OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
                OrganizationSector sectorOrg = sectorDao.GetOrganizationSectorByName(sectorsList.Text);
                LinkedOrganization updatedOrganization = new LinkedOrganization();

                updatedOrganization.Name = organizationName.Text;
                updatedOrganization.State = organizationState.Text;
                updatedOrganization.TelephoneNumber = organizationPhone.Text;
                updatedOrganization.Email = organizationEmail.Text;
                updatedOrganization.City = organizationCity.Text;
                updatedOrganization.Address = organizationAddress.Text;
                updatedOrganization.BelongsTo = sectorOrg;

                LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
                bool check = linkedOrganizationDAO.UpdateLinkedOrganization(updatedOrganization);

                if (check == true)
                {
                    System.Windows.MessageBox.Show("Se ha actualizado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    NavigationService.GoBack();
                }
                else
                {
                    System.Windows.MessageBox.Show("Ha ocurrido un error", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
