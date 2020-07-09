/*
    Date: 02/06/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para ProjectActivityUpdateControl.xaml
    /// </summary>
    public partial class ProjectActivityUpdateControl : UserControl
    {
        private readonly ObservableCollection<ProjectActivity> projectActivities;
        private readonly BusinessDomain.Project project;

        public ProjectActivityUpdateControl(BusinessDomain.Project projectSelected)
        {
            InitializeComponent();
            

            if (projectSelected != null)
            {
                project = projectSelected;
                this.DataContext = projectSelected;

                projectActivities = new ObservableCollection<ProjectActivity>(project.ProjectActivities);
            }
            
            projectActivityList.ItemsSource = projectActivities;
        }

        private void DeleteProjectActivity(object sender, RoutedEventArgs e)
        {
            
            bool isDeleted;
            string message;

            isDeleted = DeleteProjectActivitySelected();

            if (isDeleted)
            {
                message = "La actividad seleccionada fue eliminada exitosamente.";
                DeleteProjectActivityFromList();

                DialogWindowManager.ShowSuccessWindow(message);
            }
            else
            {
                message = "No se pudo eliminar la actividad seleccionada. Intente de nuevo.";

                DialogWindowManager.ShowErrorWindow(message);
            }
        }

        public void UpdateProjectActivities()
        {
            string message;

            if (projectActivityControl.AreThereNotActivities())
            {
                message = "No hay nuevas actividades por agregar. Agrega nuevas actividades y " +
                    "selecciona el botón Actualizar.";

                DialogWindowManager.ShowErrorWindow(message);
            }
            else
            {
                bool areSaved = AddProjectActivities();

                if (areSaved)
                {
                    message = "Proyecto actualizado exitosamente. Las activididades fueron agregadas.";

                    DialogWindowManager.ShowSuccessWindow(message);
                    AddProjectActivitiesToList();
                    projectActivityControl.DeleteActivities();
                }
                else
                {
                    message = "Error al intentar actualizar el proyecto. Intente más tarde.";

                    DialogWindowManager.ShowErrorWindow(message);
                }
            }
        }

        private bool AddProjectActivities()
        {
            bool areSaved;
            ManageProject manageProject = new ManageProject();
            List<ProjectActivity> newProjectActivities = projectActivityControl.GetProjectActivities();
            string projectName = project.Name;

            areSaved = manageProject.AddProjectActivities(newProjectActivities, projectName);

            return areSaved;
        }

        private void AddProjectActivitiesToList()
        {
            List<ProjectActivity> newProjectActivities = projectActivityControl.GetProjectActivities();

            foreach (ProjectActivity newProjectActivity in newProjectActivities)
            {
                projectActivities.Add(newProjectActivity);
            }
        }

        private bool DeleteProjectActivitySelected()
        {
            bool isDeleted;
            ManageProject manageProject = new ManageProject();
            ProjectActivity projectActivityToDelete = projectActivityList.SelectedItem as ProjectActivity;

            isDeleted = manageProject.DeleteProjectActivity(projectActivityToDelete);

            return isDeleted;
        }

        private void DeleteProjectActivityFromList()
        {
            int projectActivitySelectedIndex = projectActivityList.SelectedIndex;

            projectActivities.RemoveAt(projectActivitySelectedIndex);
        }
    }
}
