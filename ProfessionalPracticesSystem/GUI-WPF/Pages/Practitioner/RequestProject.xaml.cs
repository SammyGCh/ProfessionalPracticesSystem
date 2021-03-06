﻿/*
    Date: 12/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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
        private readonly String currentPractitionerMatricula; 

        public RequestProject(String practitionerMatricula)
        {
            InitializeComponent();

            currentPractitionerMatricula = practitionerMatricula;

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
            int projectsSelectedNumber = projectsSelectedList.Count;

            if (projectsSelectedNumber == MAX_PROJECTSELECTED_NUMBER)
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
                    NavigationService.GoBack();
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

            ManageProjectsRequest manageProjectsRequest = new ManageProjectsRequest();

            if (projectsSelected != null)
            {
                isGenerated = manageProjectsRequest.GenerateProjectsRequest(projectsSelected, currentPractitionerMatricula);
            }

            return isGenerated;
        }

        private void CancelRequest(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
