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
using DataAccess.Implementation;

namespace GUI_WPF.Pages.Coordinator
{
    public partial class DisplayLinkedOrganization : Page
    {
        public DisplayLinkedOrganization(LinkedOrganization organization)
        {
            InitializeComponent();
            LinkedOrganizationDAO linkedOrganizationDAO = new LinkedOrganizationDAO();
            LinkedOrganization linkedOrganization = linkedOrganizationDAO.GetLinkedOrganizationByName(organization.Name);
            this.DataContext = linkedOrganization;

            
        }
        private void backButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
