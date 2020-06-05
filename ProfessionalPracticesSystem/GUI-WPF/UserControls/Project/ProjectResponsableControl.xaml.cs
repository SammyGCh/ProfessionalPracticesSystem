/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using BusinessLogic;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para ProjectResponsableControl.xaml
    /// </summary>
    public partial class ProjectResponsableControl : UserControl
    {
        public string ResponsableName
        {
            get;
            set;
        }

        public string ResponsableCharge
        {
            get;
            set;
        }

        public string ResponsableEmail
        {
            get;
            set;
        }

        public string ResponsableTelephone
        {
            get;
            set;
        }

        public ProjectResponsableControl()
        {
            InitializeComponent();
        }

        public bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                projectResponsableData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => String.IsNullOrWhiteSpace(projectFields.Text)
                    )
                )
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        public void ClearFields()
        {
            responsableName.Clear();
            responsableCharge.Clear();
            responsableEmail.Clear();
            responsableTelephone.Clear();
        }

        private void IsEmail(object sender, RoutedEventArgs e)
        {
            if (ValidatorText.IsEmail(responsableEmail.Text))
            {
                responsableEmail.BorderBrush = Brushes.Green;
            }
            else
            {
                responsableEmail.BorderBrush = Brushes.Red;
            }
        }

        private void IsName(object sender, RoutedEventArgs e)
        {
            if (ValidatorText.IsPersonName(responsableName.Text) && responsableName.Text.Length > 10)
            {
                responsableName.BorderBrush = Brushes.Green;
            }
            else
            {
                responsableName.BorderBrush = Brushes.Red;
            }
        }

        private void IsTelephoneNumber(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsTelephoneNumber(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
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

        public bool AreFieldsWrong()
        {
            bool areWrong = false;

            if (
                projectResponsableData.Children.OfType<StackPanel>().Any(
                    responsableSection => responsableSection.Children.OfType<TextBox>().Any(
                        responsableFields => !ValidatorText.IsPersonName(responsableFields.Text) ||
                        !ValidatorText.IsEmail(responsableFields.Text) ||
                        !ValidatorText.IsRightExpression(responsableFields.Text)
                    )
                )
            )
            {
                areWrong = true;
            }

            return areWrong;
        }
    }
}
