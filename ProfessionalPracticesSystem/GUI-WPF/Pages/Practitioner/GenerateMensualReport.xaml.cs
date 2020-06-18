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

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para GenerateMonthlyReport.xaml
    /// </summary>
    public partial class GenerateMensualReport : Page
    {
        private DocumentManagement documentManager;
        private Practitioner practitioner;

        public GenerateMensualReport(Practitioner practitioner)
        {
            this.practitioner = practitioner;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que deseas cancelar?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void GenerateReport(object sender, RoutedEventArgs e)
        {

            if (!AreFieldsEmpty())
            {
                MessageBoxResult result = MessageBox.Show("¿Deseas finalizar tu reporte?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if (SaveMensualReport())
                    {
                        MessageBox.Show("Generaste tu reporte mensual exitosamente", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.GoBack();

                    }
                    else
                    {
                        MessageBox.Show("Error, no se pudo cargar el reporte, intente más tarde", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Uno o varios campos están vacíos. Por favor ingresa los datos necesarios", "", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private bool AreFieldsEmpty()
        {
            return (String.IsNullOrEmpty(documentName.Text) || String.IsNullOrEmpty(documentDescription.Text));
        }

        private MensualReport GetReport()
        {
            Project auxiliarProject = new Project
            {
                IdProject = practitioner.Assigned.IdProject
            };


            MensualReport mensualReport = new MensualReport
            {
                MensualReportName = documentName.Text,
                Description = documentDescription.Text,
                GeneratedBy = practitioner,
                DerivedFrom = auxiliarProject,
                MonthReportedDate = DateTime.Now.ToString("MM/dd/yyyy")
            };

            return mensualReport;
        }
    }
}
