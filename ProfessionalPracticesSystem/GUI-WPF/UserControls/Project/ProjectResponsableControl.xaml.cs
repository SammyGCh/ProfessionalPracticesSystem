/*
    Date: 13/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using BusinessLogic;
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
            this.DataContext = this;
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
            if (ValidatorText.IsPersonName(responsableName.Text) && responsableName.Text.Length > 1)
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
    }
}
