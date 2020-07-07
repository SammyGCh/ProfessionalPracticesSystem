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
using GUI_WPF.Windows;

namespace GUI_WPF
{
    public partial class AddDocument : Page
    {
        private String sourcePath;
        private int idDocumentType;
        private String practitionerMatricula;
        private DocumentManagement documentManager;

        public AddDocument(int documentType, String practitionerMatricula)
        {
            this.idDocumentType = documentType;
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        private void Accept(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Está seguro de subir el documento?");

            if (isConfirmed)
            {
                if (sourcePath != null)
                {
                    bool isAdded = SaveDocument();

                    if (isAdded)
                    {
                        DialogWindowManager.ShowSuccessWindow("Documento añadido exitosamente");
                        NavigationService.GoBack();
                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("Error al añadir archivo, por favor intente más tarde");
                    }
                }
                else
                {
                    DialogWindowManager.ShowErrorWindow("Por favor, seleccione un documento");
                }
            }
        }

        public bool SaveDocument()
        {
            documentManager = new DocumentManagement();
            Document newDocument = GetDocument();
            bool result = documentManager.AddDocument(newDocument, sourcePath);
            sourcePath = "";

            return result;
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            OpenFileDialog explorer = new OpenFileDialog
            {
                Filter = "pdf files (*.pdf)|*.pdf"
            };

            try
            {
                if (explorer.ShowDialog() == DialogResult.OK)
                {
                    if (explorer.FileName.Length < 60)
                    {
                        sourcePath = explorer.FileName;
                        pdfViewer.Navigate(sourcePath);
                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("El nombre del archivo es demasiado grande");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                DialogWindowManager.ShowErrorWindow("Ocurrio un error al abrir el explorador, intente mas tarde");
                LogManager.WriteLog("Something went wrong in  GUI-WPF/Pages/DocumentManagement/AddDocument:", ex);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas salir?");

            if (isConfirmed)
            {
                NavigationService.GoBack();
            }
        }

        private Document GetDocument()
        {
            Practitioner currentPractitioner = GetCurrentPractitioner();
            String documentName = sourcePath.Substring(sourcePath.LastIndexOf(@"\"));
            String documentsDirectory = "ftp://192.168.100.100/DocumentsOfPractitioner" + currentPractitioner.IdPractitioner;
            DocumentType auxiliarDocumentType = new DocumentType { IdDocumentType = idDocumentType };


            Document newDocument = new Document
            {
                Name = documentName,
                Path = documentsDirectory,
                TypeOf = auxiliarDocumentType,
                AddBy = currentPractitioner
            };

            return newDocument;
        }

        private Practitioner GetCurrentPractitioner()
        {
            DocumentManagement documentManager = new DocumentManagement();
            Practitioner currentPRactitioner = documentManager.GetAllInformationPractitioner(practitionerMatricula);

            return currentPRactitioner;
        }
    }
}
