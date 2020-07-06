/*
    Date: 15/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using GUI_WPF.Windows;

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
            ManagePractitioner managePractitioner = new ManagePractitioner();

            if (managePractitioner.CanRequestProject(practitionerMatricula))
            {
                NavigationService.Navigate(new RequestProject(practitionerMatricula));
            }
            else
            {
                string message = "Ya has solicitado tus tres proyectos.";
                DialogWindowManager.ShowErrorWindow(message);
            }
        }
    }
}
