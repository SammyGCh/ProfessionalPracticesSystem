/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System;

namespace DataAccessTests
{
    [TestClass]
    public class DocumentDAOTest
    {
        DocumentDAO documentDAO = new DocumentDAO();

        [TestMethod]
        public void SaveDocument_DocumentIsComplete_ReturnTrue()
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
                Path = "D:\\Escuela\\Principios de construccion\\Proyecto congreso\\Proyecto\\CRUD",
                TypeOf = documentType,
                AddBy = practitioner,
                Observations = "El alumno respondio de manera adecuada a todas las preguntas del documento que se adjunto",
                Grade = "8.5"
            };

            bool result = documentDAO.SaveDocument(newDocument);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteDocument_DocumentExist_ReturnTrue()
        {
            int idDocument = 6;

            bool result = documentDAO.DeleteDocument(idDocument);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllDocument_WhatDocumentsExist_ReturnDocumentList()
        {
            List<Document> result = documentDAO.GetAllDocument();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetDocument_DocumentExist_ReturnDocument()
        {
            int idDocument = 5;

            Document result = documentDAO.GetDocument(idDocument);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllDocumentByPractitioner_HowManyDocumentsBelongsToPractitioner_ReturnDocumentList()
        {
            int idpractitioner = 1;

            List<Document> result = documentDAO.GetAllDocumentByPractitioner(idpractitioner);

            Assert.IsTrue(result.Count > 0);
        }

        /*
        [TestMethod]
        public void GetAllDocumentByType_WhatDocumentsOfThatTypeExist_ReturnDocumentList()
        {
            int idDocumentType = 1;

            List<Document> result = documentDAO.GetAllDocumentByType(idDocumentType);

            Assert.IsTrue(result.Count > 0);
        }
        */

        [TestMethod]
        public void UpdateDocumentGrade_DocumentExist_ReturnTrue()
        {
            int idDocument = 7;
            String grade = "10";

            bool result = documentDAO.UpdateDocumentGrade(idDocument, grade);

            Assert.IsTrue(result);
        }
    }
}
