/*
    Date: 15/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Windows;
using iText.Svg.Renderers.Impl;

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

        public bool CurrentPractitionerHasProject()
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();

            return practitionerDAO.PractitionerHasProject(practitionerMatricula);
        }

        public Project CurrentPractitionerProject()
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();
            BusinessDomain.Practitioner practitioner = new BusinessDomain.Practitioner();
            practitioner = practitionerDAO.GetPractitionerByMatricula(practitionerMatricula);

            return practitioner.Assigned;
        }

        private void ConsultDocumentation(object sender, RoutedEventArgs e)
        {
            if (CurrentPractitionerHasProject())
            {
                NavigationService.Navigate(new Documentation(practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Necesitas tener un proyecto asignado para acceder a esta sección");
            }
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

        private void ConsultProject(object sender, RoutedEventArgs e)
        {
            if (CurrentPractitionerHasProject())
            {
                Project currentProject = CurrentPractitionerProject();
                NavigationService.Navigate(new ProjectDetails(currentProject));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Aún no cuentas con un proyecto asignado");
            }
        }
    }
}
