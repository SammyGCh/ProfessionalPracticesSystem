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
        public void SaveDocumentType_DocumentTypeIsComplete_ReturnTrue()
        {
            DocumentType NewDocumentType = new DocumentType()
            {
                Name = "Reporte mensual"
            };


            bool result = documentTypeDAO.SaveDocumentType(NewDocumentType);


            Assert.IsTrue(result);

        }

        [TestMethod]
        public void DeleteDocumentType_DocumentTypeExist_ReturnTrue()
        {
            int idDocumentType = 5;


            bool result = documentTypeDAO.DeleteDocumentType(idDocumentType);


            Assert.IsTrue(result);

        }

        [TestMethod]
        public void GetDocumentType_DocumentTypeExist_DocumentType()
        {
            int idDocumentType = 1;


            DocumentType result = documentTypeDAO.GetDocumentType(idDocumentType);


            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetAllDocumentType_WhatDocumentTypesExist_ReturnDocumentTypeList()
        {
            List<DocumentType> result = documentTypeDAO.GetAllDocumentType();


            Assert.IsTrue(result.Count > 0);

        }

    }
}
