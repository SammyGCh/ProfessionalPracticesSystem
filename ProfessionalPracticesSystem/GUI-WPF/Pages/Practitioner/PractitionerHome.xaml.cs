/*
    Date: 15/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para PractitionerHome.xaml
    /// </summary>
    public partial class PractitionerHome : Page
    {
        private String practitionerMatricula;

        public PractitionerHome(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        private void ConsultDocumentation(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Documentation(practitionerMatricula));
        }

        private void GoToRequestProject(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestProject());
        }
    }
}
