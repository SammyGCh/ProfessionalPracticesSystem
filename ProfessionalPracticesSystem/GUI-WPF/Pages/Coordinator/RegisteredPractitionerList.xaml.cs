/*
        Date: 07/20/2020                              
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
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;

using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for PractitionerList.xaml
    /// </summary>
    public partial class RegisteredPractitionerList : Page
    {
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
                tableOfPractitioners.ItemsSource = allPractitioners;
            }
        }

        private void CancelViewList(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DisplayPractitionerData(object sender, RoutedEventArgs e)
        {

            DataGrid dataGrid = tableOfPractitioners;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell rowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string practitionerName = ((TextBlock)rowAndColumn.Content).Text;
            NavigationService.Navigate(new DisplayLinkedOrganization(practitionerName));

        }

        private void UpdatePractitionerData(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = tableOfPractitioners;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);
            DataGridCell rowAndColumn = (DataGridCell)dataGrid.Columns[0].GetCellContent(row).Parent;
            string practitionerName = ((TextBlock)rowAndColumn.Content).Text;
            NavigationService.Navigate(new UpdateOrganization(practitionerName));
        }
    }
}
