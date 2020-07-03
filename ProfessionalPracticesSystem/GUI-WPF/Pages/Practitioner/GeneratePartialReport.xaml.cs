using BusinessDomain;
using GUI_WPF.Windows;
using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para GeneratePartialReport.xaml
    /// </summary>
    public partial class GeneratePartialReport : Page
    {
         private String practitionerMatricula;

        public GeneratePartialReport(String practitionerMatricula)
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

        private void Generate(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Desea finalizar su reporte?” ");

                if (isConfirmed)
                {
                    String destinyPath = PathToSave();

                    if (destinyPath != null)
                    {
                        DocumentManagement documentManager = new DocumentManagement();
                        PartialReport partialReport = GetReport();

                        if (documentManager.GeneratePartialReport(destinyPath, partialReport))
                        {
                            DialogWindowManager.ShowSuccessWindow("Reporte parcial generado exitosamente");
                        }
                        else
                        {
                            DialogWindowManager.ShowErrorWindow("Error al generar el archivo PDF del reporte parcial");
                        }
                    }
                }

            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
        }

        private bool AreFieldsComplete()
        {
            bool isComplete = false;

            if (!AreFieldsEmpty())
            {
                isComplete = (ValidatorText.IsPartialReportTextRight(practitionerObservations.Text) == ValidatorText.IsPartialReportTextRight(practitionerResults.Text));
            }

            return isComplete;
        }

        private bool AreFieldsEmpty()
        {
            return (String.IsNullOrEmpty(practitionerObservations.Text) || String.IsNullOrEmpty(practitionerResults.Text));
        }

        private PartialReport GetReport()
        {

            PartialReport partialReport = new PartialReport()
            {
                GeneratedBy = GetCurrentPractitioner(),
                PractitionerObservationsAnswer = practitionerObservations.Text,
                PractitionerResultsAnswer = practitionerResults.Text
            };

            return partialReport;
        }

        private BusinessDomain.Practitioner GetCurrentPractitioner()
        {
            DocumentManagement documentManager = new DocumentManagement();
            BusinessDomain.Practitioner currentPractitioner = documentManager.GetAllInformationPractitioner(practitionerMatricula);

            return currentPractitioner;
        }

        private String PathToSave()
        {
            SaveFileDialog saveWindow = new SaveFileDialog();
            saveWindow.Filter = "PDF Document|*.pdf";
            saveWindow.Title = "Selecciona ruta de guardado";
            saveWindow.ShowDialog();

            return saveWindow.FileName;
        }
    }
}
