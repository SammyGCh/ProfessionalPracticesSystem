/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using BusinessDomain;
using Microsoft.VisualBasic.Devices;
using DataAccess.Implementation;
using System.IO;

namespace BusinessLogic
{
    public class DocumentManagement
    {
        Computer thisPC = new Computer();
        DocumentDAO documentDAO;
        DocumentTypeDAO documentTypeDAO;
        PractitionerDAO practitionerDAO;

        public DocumentManagement(){
            documentDAO = null;
            documentTypeDAO = null;
            practitionerDAO = null;
        }

        public String AddDocument(String SourcePath, int idDocumentType, int idPractitioner)
        {
            String result;
            String documentName = SourcePath.Substring(SourcePath.LastIndexOf(@"\"));
            String documentsDirectory = "..\\..\\..\\\\BusinessLogic\\Documents";
            String finalPath = documentsDirectory + documentName;

            if (!File.Exists(finalPath))
            {
                documentDAO = new DocumentDAO();
                documentTypeDAO = new DocumentTypeDAO();
                practitionerDAO = new PractitionerDAO();

                Document newDocument = new Document()
                {
                    Name = documentName,
                    Path = finalPath,
                    TypeOf = documentTypeDAO.GetDocumentType(idDocumentType),
                    AddBy = practitionerDAO.GetPractitioner(idPractitioner)
                };

                if (documentDAO.SaveDocument(newDocument))
                {
                    thisPC.FileSystem.CopyFile(SourcePath, finalPath);
                    result = "Archivo guardado exitosamente";
                }
                else
                {
                    result = "Error, no se pudo cargar el documento, intente más tarde";
                }

            }
            else
            {
                result = "El archivo ya existe en la plataforma, por favor ingrese el archivo con otro nombre";
            }

            return result;
        }
    }
}
