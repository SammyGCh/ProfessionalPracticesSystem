/*
    Date: 06/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
using GUI_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para Documentation.xaml
    /// </summary>
    public partial class Documentation : Page
    {
        private String practitionerMatricula;
        private const int MINIMUN_MENSUAL_REPORT = 2;
        private const int MINIMUN_PARTIAL_REPORT = 2;
        private const int ID_PARTIAL_REPORT = 1;
        private const int ID_SELFASSESSMENT = 3;
        private const int ID_ACEPTANCE_LETTER = 4;
        private const int ID_ASSIGMENT_LETTER = 5;

        public Documentation(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        public bool IsGenerateMensualReportActivated()
        {
            return (int.Parse(DateTime.Now.Day.ToString()) <= 5);
        }

        public bool IsGenerateSelfassessmentActivated()
        {
            DataAccess.Implementation.DocumentDAO documentDAO = new DataAccess.Implementation.DocumentDAO();
            int practitionerReports = documentDAO.GetAllPartialReportByPractitioner(practitionerMatricula);

            return (practitionerReports == MINIMUN_PARTIAL_REPORT);
        }

        public bool IsGeneratePartialReportActivated()
        {
            DataAccess.Implementation.MensualReportDAO mensualReportDAO = new DataAccess.Implementation.MensualReportDAO();
            List<MensualReport> practitionerReports = mensualReportDAO.GetAllReportsByPractitioner(practitionerMatricula);

            return (practitionerReports.Count == MINIMUN_MENSUAL_REPORT);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GenerateMensualReport(object sender, RoutedEventArgs e)
        {
            if (IsGenerateMensualReportActivated())
            {
                NavigationService.Navigate(new GenerateMensualReport(practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("La opcion 'Generar reporte mensual' no esta activa, solo lo esta los primeros 5 dias del mes");
            }
        }

        private void GenerateSelfassessment(object sender, RoutedEventArgs e)
        { 
            if (IsGenerateSelfassessmentActivated())
            {
                NavigationService.Navigate(new GenerateSelfassessment(practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("La opcion 'Generar autoevaluacion' no esta activa, necesitas tener dos reportes parciales");
            }
        }

        private void GeneratePartialReport(object sender, RoutedEventArgs e)
        {
            if (IsGeneratePartialReportActivated())
            {
                NavigationService.Navigate(new GeneratePartialReport(practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("La opcion 'Generar reporte parcial' no esta activa, necesitas tener dos reportes mensuales");
            }
        }

        private void AddPartialReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(ID_PARTIAL_REPORT, practitionerMatricula));
        }

        private void AddSelfAssessment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(ID_SELFASSESSMENT, practitionerMatricula));
        }

        private void AddAceptanceLetter(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(ID_ACEPTANCE_LETTER, practitionerMatricula));
        }

        private void AddAssigmentLetter(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(ID_ASSIGMENT_LETTER, practitionerMatricula));
        }
    }
}
