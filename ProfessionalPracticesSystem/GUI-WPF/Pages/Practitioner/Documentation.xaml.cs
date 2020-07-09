/*
    Date: 06/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
using DataAccess.Implementation;
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

        private const int MAXIMUM_MENSUAL_REPORT = 4;
        private const int MAXIMUM_PARTIAL_REPORT = 2;
        private const int MAXIMUM_SELFASSESSMENT = 1;
        private const int MAXIMUM_ASSIGMENT_LETTER = 1;
        private const int MAXIMUM_ACEPTANCE_LETTER = 1;

        private const int MINIMUN_MENSUAL_REPORT = 2;
        private const int MINIMUN_PARTIAL_REPORT = 2;

        private const int ID_PARTIAL_REPORT = 1;
        private const int ID_SELFASSESSMENT = 3;
        private const int ID_ACEPTANCE_LETTER = 4;
        private const int ID_ASSIGMENT_LETTER = 5;
        private const int MAX_DAYS_TO_GENERATE_MENSUAL_REPORT = 15;

        public Documentation(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        public int ConsulNumberOfMensualReportsByPractitioner()
        {
            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            int numberOfReports = mensualReportDAO.GetNumberOfAllMensualReportsByPractitioner(practitionerMatricula);

            return numberOfReports;
        }

        public int ConsulNumberOfPartialReportsByPractitioner()
        {
            DocumentDAO documentDAO = new DocumentDAO();
            int numberOfReports = documentDAO.GetNumberOfAllPartialReportByPractitioner(practitionerMatricula);

            return numberOfReports;
        }

        public int ConsulNumberOfSelfassessmentByPractitioner()
        {
            DocumentDAO documentDAO = new DocumentDAO();
            int numberOfassassment = documentDAO.GetNumberOfAllSelfassessmentByPractitioner(practitionerMatricula);

            return numberOfassassment;
        }

        public int ConsulNumberOfAceptanceLetterByPractitioner()
        {
            DocumentDAO documentDAO = new DocumentDAO();
            int numberOfAceptanceLetter = documentDAO.GetNumberOfAllAceptanceLetterByPractitioner(practitionerMatricula);

            return numberOfAceptanceLetter;
        }

        public int ConsulNumberOfAssigmentLetterByPractitioner()
        {
            DocumentDAO documentDAO = new DocumentDAO();
            int numberOfAssigment = documentDAO.GetNumberOfAllAssigmentLetterByPractitioner(practitionerMatricula);

            return numberOfAssigment;
        }

        public bool IsGenerateMensualReportActivated()
        {
            return (int.Parse(DateTime.Now.Day.ToString()) < MAX_DAYS_TO_GENERATE_MENSUAL_REPORT) && (ConsulNumberOfMensualReportsByPractitioner() < MAXIMUM_MENSUAL_REPORT);
        }

        public bool IsGenerateSelfassessmentActivated()
        {
            return (ConsulNumberOfPartialReportsByPractitioner() == MINIMUN_PARTIAL_REPORT);
        }

        public bool IsGeneratePartialReportActivated()
        {
            return (ConsulNumberOfMensualReportsByPractitioner() == MINIMUN_MENSUAL_REPORT);
        }

        public bool IsAddPartialReportActivated()
        {
            return (ConsulNumberOfPartialReportsByPractitioner() < MAXIMUM_PARTIAL_REPORT);
        }

        public bool IsAddSelfAssessmentActivated()
        {
            return (ConsulNumberOfSelfassessmentByPractitioner() < MAXIMUM_SELFASSESSMENT);
        }

        public bool IsAddAceptanceLetterActivated()
        {
            return (ConsulNumberOfAceptanceLetterByPractitioner() < MAXIMUM_ACEPTANCE_LETTER);
        }

        public bool IsAddAssigmentLetterActivated()
        {
            return (ConsulNumberOfAssigmentLetterByPractitioner() < MAXIMUM_ASSIGMENT_LETTER);
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
                DialogWindowManager.ShowErrorWindow("La opcion 'Generar reporte parcial' no esta activa, necesitas tener unicamente dos reportes mensuales");
            }
        }

        private void AddPartialReport(object sender, RoutedEventArgs e)
        {
            if (IsAddPartialReportActivated())
            {
                NavigationService.Navigate(new AddDocument(ID_PARTIAL_REPORT, practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Ya cuentas con el maximo de reportes parciales permitidos");
            }
        }

        private void AddSelfAssessment(object sender, RoutedEventArgs e)
        {
            if (IsAddSelfAssessmentActivated())
            {
                NavigationService.Navigate(new AddDocument(ID_SELFASSESSMENT, practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Ya cuentas con una autoevaluación en el sistema");
            }
        }

        private void AddAceptanceLetter(object sender, RoutedEventArgs e)
        {
            if (IsAddAceptanceLetterActivated())
            {
                NavigationService.Navigate(new AddDocument(ID_ACEPTANCE_LETTER, practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Ya cuentas con una oficio de aceptación");
            }
        }

        private void AddAssigmentLetter(object sender, RoutedEventArgs e)
        {
            if (IsAddAssigmentLetterActivated())
            {
                NavigationService.Navigate(new AddDocument(ID_ASSIGMENT_LETTER, practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Ya cuentas con una oficio de asignación");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
