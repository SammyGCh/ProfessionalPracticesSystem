/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessDomain;
using BusinessLogic;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para ProjectActivity.xaml
    /// </summary>
    public partial class ProjectActivityControl : UserControl
    {
        private readonly List<string> monthsList;
        private ObservableCollection<ProjectActivity> projectActivities;

        public ProjectActivityControl()
        {
            InitializeComponent();

            monthsList = new List<string>
            {
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            };

            projectActivities = new ObservableCollection<ProjectActivity>();

            months.ItemsSource = monthsList;
            projectActivitiesListBox.ItemsSource = projectActivities;
        }

        private void AddNewActivity(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                MessageBox.Show("Ingrese el nombre de la actividad y selecciona el mes correspondiente.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (AlreadyExist())
            {
                MessageBox.Show("La actividad ya existe. Por favor verifique la información ingresada.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!ValidatorText.IsRightExpression(activityName.Text))
            {
                MessageBox.Show("El nombre de la actividad es incorrecto. Por favor verifica la información.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                AddActivityToList();
            }
        }

        private void AddActivityToList()
        {
            
            projectActivities.Add(new ProjectActivity()
            {
                Name = activityName.Text,
                Month = months.Text
            });

            ClearFields();
        }

        private bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if(String.IsNullOrWhiteSpace(activityName.Text) || months.SelectedItem == null)
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private bool AlreadyExist()
        {
            bool exist = false;

            foreach (object activity in projectActivitiesListBox.Items)
            {
                ProjectActivity projectActivityAux = (activity as ProjectActivity);

                if(projectActivityAux.Name.Equals(activityName.Text) && 
                    projectActivityAux.Month.Equals(months.Text))
                {
                    exist = true;
                    break;
                }
            }

            return exist;
        }

        private void ClearFields()
        {
            activityName.Clear();
            months.SelectedItem = null;
        }

        public bool AreThereNotActivities()
        {
            bool areThereNotActivities = true;

            if(projectActivitiesListBox.Items.Count > 0)
            {
                areThereNotActivities = false;
            }

            return areThereNotActivities;
        }

        public List<ProjectActivity> GetProjectActivities()
        {
            List<ProjectActivity> allProjectActivities = new List<ProjectActivity>();
            ProjectActivity projectActivity;

            foreach (object activities in projectActivities)
            {
                projectActivity = new ProjectActivity
                {
                    Name = (activities as ProjectActivity).Name,
                    Month = (activities as ProjectActivity).Month
                };

                allProjectActivities.Add(projectActivity);
            }

            return allProjectActivities;
        }

        private void DeleteProjectActivity(object sender, RoutedEventArgs e)
        {
            if (projectActivitiesListBox.SelectedItem != null)
            {
                projectActivities.RemoveAt(projectActivitiesListBox.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Seleccione la actividad que desee eliminar.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteActivities()
        {
            projectActivities.Clear();
        }

        private void ValidateText(object sender, TextChangedEventArgs e)
        {
            if (ValidatorText.IsRightExpression(((TextBox)sender).Text))
            {
                ((TextBox)sender).BorderBrush = Brushes.Green;
            }
            else
            {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }
    }
}
