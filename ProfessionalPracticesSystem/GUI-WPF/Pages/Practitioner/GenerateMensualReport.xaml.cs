/*
    Date: 05/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using BusinessLogic;
using BusinessDomain;
using System.Globalization;
using GUI_WPF.Windows;

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para GenerateMonthlyReport.xaml
    /// </summary>
    public partial class GenerateMensualReport : Page
    {
        private DocumentManagement documentManager;
        private String practitionerMatricula;

        public GenerateMensualReport(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar?");

            if (isConfirmed)
            {
                NavigationService.GoBack();
            }
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Deseas finalizar tu reporte?");

                if (isConfirmed)
                {
                    if (SaveMensualReport())
                    {
                        DialogWindowManager.ShowSuccessWindow("Generaste tu reporte mensual exitosamente");
                        NavigationService.GoBack();

                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("Error, no se pudo cargar el reporte, intente más tarde");
                    }
                }

            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
        }

        private bool SaveMensualReport()
        {
            bool isSaved;
            documentManager = new DocumentManagement();
            MensualReport newMensualReport = GetReport();
            isSaved = documentManager.GenerateMensualReport(newMensualReport);

            return isSaved;
        }

        private bool AreFieldsComplete()
        {
            bool isComplete = false;

            if (!AreFieldsEmpty())
            {
                isComplete = (ValidatorText.IsTextRight(documentName.Text) == ValidatorText.IsMensualReportTextRight(documentDescription.Text));
            }

            return isComplete;
        }

        private bool AreFieldsEmpty()
        {
            return (String.IsNullOrEmpty(documentName.Text) || String.IsNullOrEmpty(documentDescription.Text));
        }

        private MensualReport GetReport()
        {
            Practitioner currentPractitioner = GetCurrentPractitioner();
            Project auxiliarProject = new Project
            {
                IdProject = currentPractitioner.Assigned.IdProject
            };


            MensualReport mensualReport = new MensualReport
            {
                MensualReportName = documentName.Text,
                Description = documentDescription.Text,
                GeneratedBy = currentPractitioner,
                DerivedFrom = auxiliarProject,
                MonthReportedDate = DateTime.Now.ToString("MM/dd/yyyy")
            };

            return mensualReport;
        }

        private Practitioner GetCurrentPractitioner()
        {
            DocumentManagement documentManager = new DocumentManagement();
            Practitioner currentPractitioner = documentManager.GetAllInformationPractitioner(practitionerMatricula);

            return currentPractitioner;
        }
    }
}
