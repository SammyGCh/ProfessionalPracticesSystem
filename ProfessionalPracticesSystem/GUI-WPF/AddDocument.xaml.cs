/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using System.Windows;
using System.Windows.Controls;
using BusinessLogic;
using System.Windows.Forms;

namespace GUI_WPF
{
    public partial class AddDocument : Page
    {
        OpenFileDialog explorador = new OpenFileDialog();
        String sourcePath;
        int idDocumentType;
        int idPractitioner;
        DocumentManagement documentManager;

        public AddDocument(int documentType, int idPractitioner)
        {
            this.idDocumentType = documentType;
            this.idPractitioner = idPractitioner;
            documentManager = new DocumentManagement();
            InitializeComponent();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Está seguro de subir el documento?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if(sourcePath != null)
                {
                    String functionResult = documentManager.AddDocument(sourcePath, idPractitioner, idDocumentType);
                    System.Windows.MessageBox.Show(functionResult, "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor, seleccione un documento", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            explorador.Filter = "pdf files (*.pdf)|*.pdf";
            var result = explorador.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                sourcePath = explorador.FileName;
                pdfViewer.Navigate(sourcePath);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Seguro que deseas salir?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }
    }
}
