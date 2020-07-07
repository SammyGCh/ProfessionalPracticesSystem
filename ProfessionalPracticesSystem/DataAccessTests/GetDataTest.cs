using System;
using BusinessDomain;
using System.Collections.Generic;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class GetDataTest
    {
        readonly PractitionerDAO practitionerDAO = new PractitionerDAO();
        readonly DocumentDAO documentDAO = new DocumentDAO();
        readonly DocumentTypeDAO documentTypeDAO = new DocumentTypeDAO();
        readonly AcademicDAO academicDAO = new AcademicDAO();

        [TestMethod]
        public void GetAllPractitioner_HowManyPractitionerExist_ReturnPractitionerList()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitioner();

            Assert.IsTrue(result.Count >= 0);
        }

        [TestMethod]
        public void GetAllPractitionerByindigenousLanguage_WhatPractitionerSpeaksAnotherLanguage_ReturnPractitionerList()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitionerByindigenousLanguage();

            Assert.IsTrue(result.Count >= 0);
        }

        [TestMethod]
        public void GetPractitioner_PractitionerExist_ReturnPractitioner()
        {
            int idPractitioner = 1;

            Practitioner result = practitionerDAO.GetPractitioner(idPractitioner);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PractitionerHasProject_PractitionerIsAssingned_ReturnTrue()
        {
            String matricula = "S19012186";

            bool result = practitionerDAO.PractitionerHasProject(matricula);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PractitionerHasProject_PractitionerIsNotAssingned_ReturnTrue()
        {
            String matricula = "s18012168";

            bool result = practitionerDAO.PractitionerHasProject(matricula);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetAllPractitionerByAcademic_AcademicHasStudents_ReturnPractitionerList()
        {
            String personalNumber = "S99999999";

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByAcademic(personalNumber);

            Assert.IsTrue(result.Count >= 0);
        }

        [TestMethod]
        public void GetAllPractitionerByProject_ProjectHasPractitioner_ReturnPractitionerList()
        {
            int idProject = 1;

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByProject(idProject);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllPractitionerByLinkedOrganization_OrganizationHasPractitioners_ReturnPractitionerList()
        {
            int idOrganization = 1;

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByLinkedOrganization(idOrganization);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetPractitionerPersonalInfo_PractitionerExist_ReturnPractitionerObject()
        {
            int idPractitioner = 1;

            Practitioner result = practitionerDAO.GetPractitionerPersonalInfo(idPractitioner);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllPractitionerByMatricula_ThereAreStudentsRegistered_ReturnPractitionerList()
        {

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByMatricula();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetPractitionerByMatricula_ThereAreStudentsRegistered_ReturnPractitionerObject()
        {
            String matricula = "S19012186";

            Practitioner result = practitionerDAO.GetPractitionerByMatricula(matricula);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllDocument_ExistDocuments_ReturnDocumentList()
        {
            List<Document> result = documentDAO.GetAllDocument();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllDocumentByPractitioner_PractitionerHasDocuments_ReturnDocumentList()
        {
            int idpractitioner = 1;

            List<Document> result = documentDAO.GetAllDocumentByPractitioner(idpractitioner);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetDocument_DocumentExist_ReturnDocument()
        {
            int idDocument = 14;

            Document result = documentDAO.GetDocument(idDocument);

            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void GetAllPartialReportByPractitioner_PractitionerHasPartialReports_ReturnDocument()
        {
            String matricula = "S19012186";

            int result = documentDAO.GetAllPartialReportByPractitioner(matricula);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void GetAllPartialReportByAcademic_AcademicHasPractitionersWithPartialsReports_ReturnDocumentList()
        {
            String personalNumber = "S99999999";

            List<Document> result = documentDAO.GetAllPartialReportByAcademic(personalNumber);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllSelfassessmentByAcademic_AcademicHasPractitionersWithSelfassessment_ReturnDocumentList()
        {
            String personalNumber = "S99999999";

            List<Document> result = documentDAO.GetAllSelfassessmentByAcademic(personalNumber);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetDocumentType_DocumentTypeExist_DocumentType()
        {
            int idDocumentType = 1;

            DocumentType result = documentTypeDAO.GetDocumentType(idDocumentType);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAcademic_AcademicExist_ReturnAcademic()
        {
            int idAcademic = 1;

            Academic result = academicDAO.GetAcademic(idAcademic);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllAcademic_AcademicExist_ReturnAcademicList()
        {
            List<Academic> result = academicDAO.GetAllAcademic();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAcademicByPersonalNumber_AcademicExist_ReturnAcademicList()
        {
            String personalNumber = "S99999999";

            Academic result = academicDAO.GetAcademicByPersonalNumber(personalNumber);

            Assert.IsNotNull(result);
        }

    }
}
