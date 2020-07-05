using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_WPF.Pages.Professor
{
    /// <summary>
    /// Lógica de interacción para EvaluatePartialReport.xaml
    /// </summary>
    public partial class EvaluatePartialReport : Page
    {
        private Document currentDocument;
        public EvaluatePartialReport(Document currentDocument)
        {
            InitializeComponent();
            this.currentDocument = currentDocument;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Seguro deseas cancelar la evaluación?");

            if (isConfirmed)
            {
                NavigationService.GoBack();
            }
        }

        private void AssingGrade(object sender, RoutedEventArgs e)
        {
            if (AreFieldsComplete())
            {
                bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Estás seguro de que deseas asignar esta calificación?");

                if (isConfirmed)
                {
                    Document updatedReport = GetNewReport();
                    DocumentManagement manager = new DocumentManagement();

                    if (manager.AssingGradeToPartialReport(updatedReport))
                    {
                        DialogWindowManager.ShowSuccessWindow("Calificación asignada exitosamente");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("Ocurrió un error al asignar la calificación. Intente de nuevo");
                    }
                }

            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
            }
        }

        private Document GetNewReport()
        {
            Document newReport = currentDocument;
            newReport.Grade = gradeAssigned.Text;

            return newReport;
        }

        private bool AreFieldsComplete()
        {
            bool isComplete = false;

            if (!AreFieldsEmpty())
            {
                isComplete = (ValidatorText.IsAValidGrade(gradeAssigned.Text) && ValidatorText.IsPartialReportobservationsRight(observations.Text));
            }

            return isComplete;
        }

        private bool AreFieldsEmpty()
        {
            return (String.IsNullOrEmpty(gradeAssigned.Text) || String.IsNullOrEmpty(observations.Text));
        }

        private void DownloadReport(object sender, RoutedEventArgs e)
        {
            String destinyPath = PathToSave();

            if (destinyPath != null)
            {
                DocumentManagement manager = new DocumentManagement();

                if (manager.DownloadDocument(currentDocument, destinyPath))
                {
                    DialogWindowManager.ShowSuccessWindow("Reporte parcial descargado exitosamente");
                }
                else
                {
                    DialogWindowManager.ShowErrorWindow("Error al descargar el archivo PDF del reporte parcial");
                }
            }
        }

        private String PathToSave()
        {
            System.Windows.Forms.SaveFileDialog saveWindow = new System.Windows.Forms.SaveFileDialog();
            saveWindow.Filter = "PDF Document|*.pdf";
            saveWindow.Title = "Selecciona ruta de guardado";
            saveWindow.ShowDialog();

            return saveWindow.FileName;
        }
    }
}
