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
using System.ComponentModel;
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
using System.Collections.ObjectModel;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    public partial class ProjectsConsult : Page
    {
        private readonly ObservableCollection<Project> projects;
        private Project projectSelected;
        ICollectionView projectsView;

        public ProjectsConsult()
        {
            InitializeComponent();
            ProjectDAO projectDao = new ProjectDAO();
            List<Project> allProjects = projectDao.GetActiveProjects();
            projects = new ObservableCollection<Project>(allProjects);
            projectsView = CollectionViewSource.GetDefaultView(allProjects);
            tableProjects.ItemsSource = projectsView;
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoProjectDetails(object sender, RoutedEventArgs e)
        {
            projectSelected = (Project)tableProjects.SelectedItem;

            NavigationService.Navigate(new ProjectDetails(projectSelected));
        }

        private void GoToProjectSections(object sender, RoutedEventArgs e)
        {
            projectSelected = (Project)tableProjects.SelectedItem;

            NavigationService.Navigate(new ProjectSections(projectSelected));
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {
            bool wantToDelete;
            string message = "¿Seguro desea eliminar el proyecto seleccionado?";

            wantToDelete = DialogWindowManager.ShowConfirmationWindow(message);

            if (wantToDelete)
            {
                bool isDeleted = UpdateProjectStatus();

                if (isDeleted)
                {
                    message = "Proyecto eliminado exitosamente.";
                    DeleteProjectFromList();

                    DialogWindowManager.ShowSuccessWindow(message);
                }
                else
                {
                    message = "Ocurrió un error al intentar eliminar el proyecto. Intente más tarde.";

                    DialogWindowManager.ShowErrorWindow(message);
                }
            }

        }

        private bool UpdateProjectStatus()
        {
            bool isStatusUpdated;
            ManageProject manageProject = new ManageProject();
            int idProjectToUpdate;

            projectSelected = (Project)tableProjects.SelectedItem;
            idProjectToUpdate = projectSelected.IdProject;

            isStatusUpdated = manageProject.UpdateProjectStatus(idProjectToUpdate);

            return isStatusUpdated;
        }

        private void DeleteProjectFromList()
        {
            int projectDeletedIndex = tableProjects.SelectedIndex;

            projects.RemoveAt(projectDeletedIndex);
        }
    }
}
