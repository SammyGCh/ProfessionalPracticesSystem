/*
        Date: 07/05/2020                              
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
    /// <summary>
    /// Lógica de interacción para LinkedOrganizationsList.xaml
    /// </summary>
    public partial class LinkedOrganizationsList : Page
    {
        public LinkedOrganizationsList()
        {
            InitializeComponent();
            LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
            List<LinkedOrganization> allLinkedOrganizations = linkedOrganizationDAO.GetAllLinkedOrganizations();
            tableLinkedOrganizations.ItemsSource = allLinkedOrganizations;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ClickDetailsOv(object sender, RoutedEventArgs e)
        {

            DataGrid dataGrid = tableLinkedOrganizations;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell rowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string name = ((TextBlock)rowAndColumn.Content).Text;
            NavigationService.Navigate(new DisplayLinkedOrganization(name));

        }

        private void ClickUpdateOv(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = tableLinkedOrganizations;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell rowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string name = ((TextBlock)rowAndColumn.Content).Text;
            NavigationService.Navigate(new UpdateOrganization(name));
        }
    }
}
