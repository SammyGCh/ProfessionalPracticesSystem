/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using BusinessDomain;
using DataAccess.Implementation;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using DataAccess;
using System.Net;
using iText.Layout.Borders;
using iText.IO.Image;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BusinessLogic
{
    public class DocumentManagement
    {
        private const String USER_CREDENTIAL = "UsuarioFTP";
        private const String PASSWORD_CREDENTIAL = "246810";


        public DocumentManagement() {
        }

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
            bool isGenerated;

            SelfAssessmentManager manager = new SelfAssessmentManager(assessment);

            isGenerated = manager.GenerateSelfAssessment(finalPath);

            return isGenerated;
        }

        public bool GeneratePartialReport(String finalPath, PartialReport partialReport)
        {
            bool isGenerated;

            PartialReportManager create = new PartialReportManager(partialReport);
            isGenerated = create.GeneratePartialReport(finalPath);

            return isGenerated;
        }

        public Practitioner GetAllInformationPractitioner(String matricula)
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();

            Practitioner practitioner = practitionerDAO.GetPractitionerByMatricula(matricula);

            return practitioner;

        }

        public bool AssingGradeToPartialReport(Document partialReport)
        {
            bool isUpdated;

            DocumentDAO documentDAO = new DocumentDAO();
            isUpdated = documentDAO.UpdateDocumentGrade(partialReport.IdDocument, partialReport.Grade);
        }
      
        public bool AssingGradeToMensualReport(MensualReport mensualReport)
        {
            bool isUpdated;
            MensualReportDAO mensualReportDAO = new MensualReportDAO();

            isUpdated = mensualReportDAO.UpdateMensualReport(mensualReport);

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