/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using System.Windows;
using System.Windows.Controls;
using BusinessLogic;
using System.Windows.Forms;
using BusinessDomain;
using DataAccess;

namespace GUI_WPF
{
    public partial class AddDocument : Page
    {
        private String sourcePath;
        private int idDocumentType;
        private int idPractitioner;
        private DocumentManagement documentManager;

        public AddDocument(int documentType, int idPractitioner)
        {
            this.idDocumentType = documentType;
            this.idPractitioner = idPractitioner;
            InitializeComponent();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("¿Está seguro de subir el documento?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if(sourcePath != null)
                {
                    String functionResult = saveDocument();
                    System.Windows.MessageBox.Show(functionResult, "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    System.Windows.MessageBox.Show("Por favor, seleccione un documento", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public String saveDocument()
        {
            documentManager = new DocumentManagement();
            Document newDocument = GetDocument();
            String result = documentManager.AddDocument(newDocument, sourcePath);

            return result;
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            OpenFileDialog explorador = new OpenFileDialog
            {
                Filter = "pdf files (*.pdf)|*.pdf"
            };

            try
            {
                if (explorador.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    sourcePath = explorador.FileName;
                    pdfViewer.Navigate(sourcePath);
                }
            }
            catch (ArgumentException ex)
            {

                System.Windows.MessageBox.Show("Ocurrio un error al abrir el explorador, intente mas tarde", "", MessageBoxButton.OK, MessageBoxImage.Error);
                LogManager.WriteLog("Something went wrong in  GUI-WPF/Pages/DocumentManagement/AddDocument:", ex);
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

        private Document GetDocument()
        {
            String documentName = sourcePath.Substring(sourcePath.LastIndexOf(@"\"));
            String documentsDirectory = "..\\..\\..\\\\BusinessLogic\\Documents";
            String finalPath = documentsDirectory + documentName;
            DocumentType auxiliarDocumentType = new DocumentType { IdDocumentType = idDocumentType };
            Practitioner auxiliarPractitioner = new Practitioner { IdPractitioner = idPractitioner };

            Document newDocument = new Document
            {
                Name = documentName,
                Path = finalPath,
                TypeOf = auxiliarDocumentType,
                AddBy = auxiliarPractitioner
            };

            return newDocument;
        }
    }
}
