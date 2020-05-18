/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
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

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para LinkedOrganizationControl.xaml
    /// </summary>
    public partial class LinkedOrganizationControl : UserControl
    {
        public object LinkedOrganizationSelected
        {
            get;
            set;
        }

        public LinkedOrganizationControl()
        {
            InitializeComponent();

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> allLinkedOrganizations = linkedOrganizationDao.GetAllLinkedOrganizations();

            linkedOrganizations.ItemsSource = allLinkedOrganizations;
        }

        public bool IsNotSelected()
        {
            bool isNotSelected = true;

            if(linkedOrganizations.SelectedItem != null)
            {
                isNotSelected = false;
            }

            return isNotSelected;
        }

        public void UnSelectLinkedOrganization()
        {
            linkedOrganizations.SelectedItem = null;
        }
    }
}
