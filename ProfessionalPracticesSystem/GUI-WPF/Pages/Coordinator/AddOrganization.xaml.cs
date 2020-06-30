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
using GUI_WPF.Windows;

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

                MessageBoxResult question = System.Windows.MessageBox.Show("¿Está seguro de guardar la organizacion?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (question == MessageBoxResult.Yes)
                {

                    String sectorName = sectorsList.Text;

                    LinkedOrganization newOrganization = new LinkedOrganization
                    {
                        Name = organizationName.Text,
                        State = organizationState.Text,
                        TelephoneNumber = organizationPhone.Text,
                        Email = organizationEmail.Text,
                        City = organizationCity.Text,
                        Address = organizationAddress.Text,
                        BelongsTo = null
                    };

                    ManageOrganization manageOrganization = new ManageOrganization();

                    bool check = manageOrganization.OrganizationSave(newOrganization, sectorName);

                    if (check == true)
                    {
                        System.Windows.MessageBox.Show("Se ha guardado correctamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Ha ocurrido un error al intentar guardar. Porfavor intente mas tarde", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
