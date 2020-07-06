/*
    Date: 03/07/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace GUI_WPF.Pages.Professor
{
    /// <summary>
    /// Lógica de interacción para EvaluateMensualReport.xaml
    /// </summary>
    public partial class EvaluateMensualReport : Page
    {
        public EvaluateMensualReport()
        {
            InitializeComponent();
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
            if (AreGradeValid())
            {
                bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Estás seguro de que deseas asignar esta calificación?");

                if (isConfirmed)
                {
                    MensualReport updatedReport = GetNewReport();
                    DocumentManagement manager = new DocumentManagement();

                    if (manager.AssingGradeToMensualReport(updatedReport))
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

        private bool AreGradeValid()
        {
            return ValidatorText.IsAValidGrade(gradeAssigned.Text);
        }

        private MensualReport GetNewReport()
        {
            MensualReport newReport = DataContext as MensualReport;
            newReport.Grade = gradeAssigned.Text;

            return newReport;
        }
    }
}
