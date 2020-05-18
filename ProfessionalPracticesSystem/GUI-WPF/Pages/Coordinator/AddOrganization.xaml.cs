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
using MaterialDesignThemes.Wpf;
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;

namespace GUI_WPF.Pages.Coordinator
{
    public partial class AddOrganization : Page
    {
        public AddOrganization()
        {
            InitializeComponent();
            OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> sectorList = sectorDao.GetAllOrganizationSectors();

            sectorsList.ItemsSource = sectorList;
        }
        private void CancelAdd(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmation = System.Windows.MessageBox.Show("¿Seguro que deseas salir?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
        private void AddNewOrganization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult question = System.Windows.MessageBox.Show("¿Está seguro de guardar la organizacion?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (question == MessageBoxResult.Yes)
            {

                OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
                String sectorName = sectorsList.SelectedItem.ToString();
                OrganizationSector sectorOrg = sectorDao.GetOrganizationSectorByName(sectorName);
                LinkedOrganization newOrganization = new LinkedOrganization();

                newOrganization.Name = organizationName.Text;
                newOrganization.State = organizationState.Text;
                newOrganization.TelephoneNumber = organizationPhone.Text;
                newOrganization.Email = organizationEmail.Text;
                newOrganization.City = organizationCity.Text;
                newOrganization.Address = organizationAddress.Text;
                newOrganization.BelongsTo = sectorOrg;

                LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
                bool check = linkedOrganizationDAO.SaveLinkedOrganization(newOrganization);

                if (check == true)
                {
                    System.Windows.MessageBox.Show("Se ha guardado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
