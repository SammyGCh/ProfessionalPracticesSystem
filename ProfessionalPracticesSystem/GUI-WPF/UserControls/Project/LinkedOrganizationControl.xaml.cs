/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using System.Windows.Controls;
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Windows;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para LinkedOrganizationControl.xaml
    /// </summary>
    public partial class LinkedOrganizationControl : UserControl
    {
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

            if (linkedOrganizations.SelectedItem != null)
            {
                isNotSelected = false;
            }

            return isNotSelected;
        }

        public void UnSelectLinkedOrganization()
        {
            linkedOrganizations.SelectedItem = null;
        }

        public LinkedOrganization GetLinkedOrganizationSelected()
        {
            LinkedOrganization linkedOrganizationSelected = linkedOrganizations.SelectedItem as LinkedOrganization;

            return linkedOrganizationSelected;
        }

        private void RegisterOrganization(object sender, System.Windows.RoutedEventArgs e)
        {
            Frame homeFrame = WindowManager.GetHomeFrame();
            homeFrame.Navigate(new AddOrganization());
        }
    }
}
