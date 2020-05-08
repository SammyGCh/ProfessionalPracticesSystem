/*
        Date: 07/05/2020                              
        Author:Cesar Sergio Martinez Palacios
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace GUI_WPF.Pages.Coordinator
{
    public partial class VinculatedOrganizations : Page 
    {
        public VinculatedOrganizations()
        {
            InitializeComponent();
            LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
            List<LinkedOrganization> allLinkedOrganizations = linkedOrganizationDAO.GetAllLinkedOrganizations();//
            tableLinkedOrganizations.ItemsSource = allLinkedOrganizations;
        }
        private void backButtonClick(object sender, RoutedEventArgs e)
        {
                NavigationService.GoBack();
        }

        private void clickDetailsOv(object sender, RoutedEventArgs e)
        {

            DataGrid dataGrid = tableLinkedOrganizations;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell rowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string name = ((TextBlock)rowAndColumn.Content).Text;
            LinkedOrganization organization = new LinkedOrganization()
            {
                Name=name
            };
            NavigationService.Navigate(new DisplayLinkedOrganization(organization));

        }


        private void clickUpdateOv(object sender, RoutedEventArgs e)
        {

        }
    }


}
