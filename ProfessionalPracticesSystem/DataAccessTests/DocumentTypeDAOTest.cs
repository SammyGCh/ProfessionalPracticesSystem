/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System.Collections.Generic;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class DocumentTypeDAOTest
    {
        private DocumentTypeDAO documentTypeDAO = new DocumentTypeDAO();

        [TestMethod]
        public void SaveDocumentTypeSuccess()
        {
            DocumentType NewDocumentType = new DocumentType()
            {
                Name = "Reporte mensual"
            };


            bool result = documentTypeDAO.SaveDocumentType(NewDocumentType);


            Assert.IsTrue(result);

        }

        [TestMethod]
        public void DeleteDocumentTypeSuccess()
        {
            int idDocumentType = 1;


            bool result = documentTypeDAO.DeleteDocumentType(idDocumentType);


            Assert.IsTrue(result);

        }

        [TestMethod]
        public void GetDocumentTypeSuccess()
        {
            int idDocumentType = 1;


            DocumentType result = documentTypeDAO.GetDocumentType(idDocumentType);


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetAllDocumentTypeSuccess()
        {
            List<DocumentType> result = documentTypeDAO.GetAllDocumentType();


            Assert.IsNotNull(result);

        }

    }
}
