/*
    Date: 12/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using GUI_WPF.Windows;
using BusinessLogic;

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para RequestProject.xaml
    /// </summary>
    public partial class RequestProject : Page
    {
        private readonly ObservableCollection<Project> availableProjectsList;
        private readonly ObservableCollection<Project> projectsSelectedList;
        private const int MAX_PROJECTSELECTED_NUMBER = 3;

        public RequestProject()
        {
            InitializeComponent();

            ProjectDAO projectDao = new ProjectDAO();
            List<Project> projects = projectDao.GetActiveProjects();

            availableProjectsList = new ObservableCollection<Project>(projects);
            projectsSelectedList = new ObservableCollection<Project>();

            availableProjects.ItemsSource = availableProjectsList;
            selectedProjects.ItemsSource = projectsSelectedList;
        }

        private void GoToProjectInformation(object sender, RoutedEventArgs e)
        {
            Project projectSelected = availableProjects.SelectedItem as Project;

            ProjectInformation projectInformationWindow = new ProjectInformation()
            {
                DataContext = projectSelected
            };

            projectInformationWindow.Show();
        }

        public bool AreThreeProjectsSelected()
        {
            bool areSelected = false;

            if (projectsSelectedList.Count == MAX_PROJECTSELECTED_NUMBER)
            {
                areSelected = true;
            }

            return areSelected;
        }

        public void AddProjectSelected(Project projectSelected)
        {
            if (projectSelected != null)
            {
                projectsSelectedList.Add(projectSelected);
            }
        }

        public void RemoveAvailableProjectSelected(Project projectSelected)
        {
            if (projectSelected != null)
            {
                availableProjectsList.Remove(projectSelected);
            }
        }

        private void RemoveProjectFromSelectedProjects(Project projectSelectedToRemove)
        {
            if (projectSelectedToRemove != null)
            {
                projectsSelectedList.Remove(projectSelectedToRemove);
            }
        }

        private void AddProjectToAvailableProjects(Project project)
        {
            if (project != null)
            {
                availableProjectsList.Add(project);
            }
        }

        private void DeleteProjectSelected(object sender, RoutedEventArgs e)
        {
            Project projectSelectedToRemove = selectedProjects.SelectedItem as Project;

            RemoveProjectFromSelectedProjects(projectSelectedToRemove);
            AddProjectToAvailableProjects(projectSelectedToRemove);
        }

        private void RequestProjects(object sender, RoutedEventArgs e)
        {
            string message;

            if (AreThreeProjectsSelected())
            {
                bool isGenerated = GenerateProjectsRequest();

                if (isGenerated)
                {
                    message = "Has solicitados los proyectos exitosamente.";

                    DialogWindowManager.ShowSuccessWindow(message);
                }
                else
                {
                    message = "No se pudo realizar la solicitud. Intentar más tarde.";

                    DialogWindowManager.ShowErrorWindow(message);
                }
            }
            else
            {
                message = "No se han seleccionado los tres proyectos.";
                DialogWindowManager.ShowErrorWindow(message);
            }
        }

        private bool GenerateProjectsRequest()
        {
            bool isGenerated = false;
            List<Project> projectsSelected = new List<Project>(projectsSelectedList);
            BusinessDomain.Practitioner requester = DataContext as BusinessDomain.Practitioner;

            ManageProjectsRequest manageProjectsRequest = new ManageProjectsRequest();

            if (projectsSelected != null && requester != null)
            {
                isGenerated = manageProjectsRequest.GenerateProjectsRequest(projectsSelected, requester);
            }

            return isGenerated;
        }
    }
}
