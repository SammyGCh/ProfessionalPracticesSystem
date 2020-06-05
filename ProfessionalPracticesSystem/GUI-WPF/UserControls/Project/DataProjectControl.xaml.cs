/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessDomain;
using DataAccess.Implementation;
using BusinessLogic;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para DataProjectControl.xaml
    /// </summary>
    public partial class DataProjectControl : UserControl
    {
        public DataProjectControl()
        {
            InitializeComponent();

            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> allDevelopmentStages = developmentStageDao.GetAllDevelopmentStages();

            developmentStages.ItemsSource = allDevelopmentStages;
        }

        public bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                projectData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => String.IsNullOrWhiteSpace(projectFields.Text)
                    )
                )
                ||
                developmentStages.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        public void ClearFields()
        {
            projectName.Clear();
            projectDescription.Clear();
            projectGeneralGoals.Clear();
            inmediateGoals.Clear();
            mediateGoals.Clear();
            metology.Clear();
            neededResources.Clear();
            responsabilities.Clear();
            duration.Clear();
            directUserNumber.Clear();
            indirectUserNumber.Clear();
            practitionerNumber.Clear();
            developmentStages.SelectedItem = null;
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

        private void ValidateNumber(object sender, TextChangedEventArgs e)
        {
            string number = ((TextBox)sender).Text;

            if (ValidatorText.IsANumber(number))
            {
                ((TextBox)sender).BorderBrush = Brushes.Green;
            }
            else
            {
                ((TextBox)sender).BorderBrush = Brushes.Red;
            }
        }

        public bool AreFieldsWrong()
        {
            bool areWrong = false;

            if (projectData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => !ValidatorText.IsRightExpression(projectFields.Text) || 
                        !ValidatorText.IsANumber(projectFields.Text)
                    )
                ))
            {
                areWrong = true;
            }

            return areWrong;
        }
    }
}
