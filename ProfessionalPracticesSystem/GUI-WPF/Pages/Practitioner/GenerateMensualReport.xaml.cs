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

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para GenerateMonthlyReport.xaml
    /// </summary>
    public partial class GenerateMensualReport : Page
    {
        private DocumentManagement documentManager;
        private int idPractitioner;
        private int idPractitionerProject;

        public GenerateMensualReport(int idPractitioner, int idPractitionerProject)
        {
            this.idPractitioner = idPractitioner;
            this.idPractitionerProject = idPractitionerProject;
            documentManager = new DocumentManagement();
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
            MessageBoxResult result = MessageBox.Show("¿Deseas finalizar tu reporte?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                TextRange content = new TextRange(documentDescription.Document.ContentStart, documentDescription.Document.ContentEnd);

                String description = content.Text;
                String reportName = documentName.Text;

                if (!String.IsNullOrEmpty(reportName))
                {
                    String functionResult = documentManager.GenerateMensualReport(reportName, description, idPractitioner, idPractitionerProject);

                    MessageBox.Show(functionResult, "", MessageBoxButton.OK, MessageBoxImage.Information);

                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ingresa los campos que se piden", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
