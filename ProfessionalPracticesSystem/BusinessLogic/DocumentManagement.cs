/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using BusinessDomain;
using Microsoft.VisualBasic.Devices;
using DataAccess.Implementation;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Events;
using iText.Layout.Element;
using iText.Kernel.Colors;
using Style = iText.Layout.Style;
using iText.IO.Font.Constants;
using DataAccess;
using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class DocumentManagement
    {
        private const String USER_CREDENTIAL = "UsuarioFTP";
        private const String PASSWORD_CREDENTIAL = "246810";

        public bool AddDocument(Document newDocument, String sourcePath)
        {
            bool isAdded = false;

            if (SaveDocumentInDataBase(newDocument))
            {
                CreateDirectoryInFTPServer(newDocument);
                isAdded = AddDocumentInFTPServer(newDocument, sourcePath);
            }

            return isAdded;
        }

        private bool SaveDocumentInDataBase(Document newDocument)
        {
            bool isSaveInDataBase;

            DocumentDAO documentDAO = new DocumentDAO();
            isSaveInDataBase = documentDAO.SaveDocument(newDocument);

            return isSaveInDataBase;
        }

        private bool AddDocumentInFTPServer(Document newDocument, String sourcePath)
        {
            bool isUpload;

            WebClient myClient = new WebClient();
            myClient.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);

            try
            {
                myClient.UploadFile(newDocument.Path + newDocument.Name, sourcePath);
                isUpload = true;
            }
            catch (ArgumentNullException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/AddDocumentInFTPServer", ex);
                isUpload = false;
            }
            catch (WebException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/AddDocumentInFTPServer", ex);
                isUpload = false;
            }


            return isUpload;
        }

        private void CreateDirectoryInFTPServer(Document newDocument)
        {
            /*
             
            Se espera que en el caso de existir el directorio para el archivo suceda una excepcion de tipo "WebException"
            De esta manera si no exite se crea y en caso contrario solo se loggea 

             */
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(newDocument.Path);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/CreateDirectoryInFTPServer", ex);
            }

        }

        public bool GenerateMensualReport(MensualReport newMensualReport)
        {
            bool isSaved;
            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            isSaved = mensualReportDAO.InsertMensualReport(newMensualReport);

            return isSaved;
        }

        public bool GenerateSelfAssessment(Selfassessment assessment, String finalPath)
        {
            bool isGenerated = false;

            SelfAssessmentManager manager = new SelfAssessmentManager(assessment);

            isGenerated = manager.GenerateSelfAssessment(finalPath);

            return isGenerated;
        }

        public bool GeneratePartialReport(String finalPath, PartialReport partialReport)
        {
            bool isGenerated = false;

            PartialReportManager create = new PartialReportManager(partialReport);
            isGenerated = create.GeneratePartialReport(finalPath);

            return isGenerated;
        }

        public bool GenerateAsignmentLetter(AssignmentLetter assignmentLetter, String finalPath)
        {
            bool isGenerated = false;

            try
            {
                /*
                PdfWriter writer = new PdfWriter(finalPath);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                String para = "En atención a su solicitud expresada a la Coordinación de Prácticas " +
                    "Profesionales de la Licenciatura en Ingeniería de Software, hacemos de su conocimiento " +
                    "que el C. " + assignmentLetter.PractitionerAssigned.Names + " "+ assignmentLetter.PractitionerAssigned.LastName 
                    + " estudiante de la Licenciatura con matrícula " +
                    "S15011634, ha sido asignado al proyecto Sistema integral clínico de la clínica " +
                    "Universitaria Sexual y Reproductiva de la Universidad Veracruzana a su digno cargo a " +
                    "partir del 13 de Agosto del presente hasta cubrir 200 horas. Cabe mencionar que el " +
                    "estudiante cuenta con la formación y el perfil para las actividades a desempeñar. ";

                document.Add(new Paragraph(para).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                writer.Close();
                document.Close();
                isGenerated = true;
                */

                PdfWriter writer = new PdfWriter("C:\\Users\\sammy\\Desktop\\prueba.pdf");
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                String letras = "qué pedo banda";

                document.Add(new iText.Layout.Element.Paragraph(letras));

                document.Close();
                writer.Close();
                isGenerated = true;

            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/GenerateAssigmentLetter", ex);
            }

            return isGenerated;
        }

        public Practitioner GetAllInformationPractitioner(String matricula)
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();

            Practitioner practitioner = practitionerDAO.GetPractitionerByMatricula(matricula);

            return practitioner;
        }

        public bool AssingGradeToMensualReport(MensualReport mensualReport)
        {
            bool isUpdated;
            MensualReportDAO mensualReportDAO = new MensualReportDAO();

            isUpdated = mensualReportDAO.UpdateMensualReport(mensualReport);

            return isUpdated;
        }

        public bool AssingGradeToPartialReport(Document partialReport)
        {
            bool isUpdated;

            DocumentDAO documentDAO = new DocumentDAO();
            isUpdated = documentDAO.UpdateDocumentGrade(partialReport.IdDocument, partialReport.Grade);

            return isUpdated;
        }

        public bool DownloadDocument(Document currentDocument, String destiny)
        {
            bool isDownloaded = false;

            String serverRoute = currentDocument.Path + currentDocument.Name;
            String finalserverRoute = serverRoute.Replace('\\', '/');

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(finalserverRoute);
            request.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            try
            {

                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (Stream fileStream = File.Create(destiny))
                {
                    ftpStream.CopyTo(fileStream);
                }

                isDownloaded = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (ArgumentException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (PathTooLongException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (InvalidOperationException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }

            return isDownloaded;
        }
    }
}