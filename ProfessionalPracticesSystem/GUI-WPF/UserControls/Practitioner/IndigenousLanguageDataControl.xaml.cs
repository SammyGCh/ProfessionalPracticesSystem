/*
    Date: 10/06/2020
    Author(s): Ricardo Moguel Sanchez
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

namespace GUI_WPF.UserControls.Practitioner
{
    /// <summary>
    /// Interaction logic for IndigenousLanguageDataControl.xaml
    /// </summary>
    public partial class IndigenousLanguageDataControl : UserControl
    {
        /*
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
        */
    }
}
