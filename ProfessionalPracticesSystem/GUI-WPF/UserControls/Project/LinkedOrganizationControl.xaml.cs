/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using System.Windows.Controls;
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Windows;
using BusinessLogic;

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

        public void UpdateProjectLinkedOrganization()
        {
            bool isUpdated = UpdatedLinkedOrganizationSelected();
            string messageWindow;

            if (isUpdated)
            {
                messageWindow = "El proyecto fue actualizado existosamente";

                DialogWindowManager.ShowSuccessWindow(messageWindow);
            }
            else
            {
                messageWindow = "No se pudo actualizar la información del proyecto. Intente de nuevo.";

                DialogWindowManager.ShowErrorWindow(messageWindow);
            }
        }

        private bool UpdatedLinkedOrganizationSelected()
        {
            bool isUpdated;
            ManageProject manageProject = new ManageProject();
            int idProject = (DataContext as BusinessDomain.Project).IdProject;
            int idLinkedOrganization = (linkedOrganizations.SelectedItem as LinkedOrganization).IdLinkedOrganization;

            isUpdated = manageProject.UpdateLinkedOrganizationOfProject(idProject, idLinkedOrganization);

            return isUpdated;
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
    }
}
