/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;

namespace DataAccessTests
{
    [TestClass]
    public class DocumentDAOTest
    {
        DocumentDAO documentDAO = new DocumentDAO();

        [TestMethod]
        public void SaveDocumentSuccess()
        {
            Practitioner practitioner = new Practitioner()
            {
                IdPractitioner = 1
            };

            DocumentType documentType = new DocumentType()
            {
                IdDocumentType = 1,
            };

            Document newDocument = new Document()
            {
                Name = "Documento nuevo",
                Path = "Ruta de guardado",
                TypeOf = documentType,
                AddBy = practitioner
            };

            bool result = documentDAO.SaveDocument(newDocument);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteDocumentSuccess()
        {
            int idDocument = 4;

            bool result = documentDAO.DeleteDocument(idDocument);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllDocumentSuccess()
        {
            List<Document> documentListResult = documentDAO.GetAllDocument();

            Assert.IsNotNull(documentListResult);
        }

        [TestMethod]
        public void GetDocumentSuccess()
        {
            int idDocument = 4;

            Document result = documentDAO.GetDocument(idDocument);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDocumentByPractitionerSuccess()
        {
            int idpractitioner = 1;

            List<Document> result = documentDAO.GetDocumentByPractitioner(idpractitioner);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDocumentByTypeSuccess()
        {
            int idDocumentType = 1;

            List<Document> result = documentDAO.GetDocumentByType(idDocumentType);

            Assert.IsNotNull(result);
        }
    }
}
