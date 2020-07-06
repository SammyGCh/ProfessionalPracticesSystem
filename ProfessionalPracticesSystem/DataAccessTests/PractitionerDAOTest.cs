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
    /*
    [TestClass]
    public class PractitionerDAOTest
    {
        PractitionerDAO practitionerDAO = new PractitionerDAO();

        [TestMethod]
        public void SavePractitioner_PractitionerIsComplete_ReturnTrue()
        {
            IndigenousLanguage lengua = new IndigenousLanguage
            {
                IdIndigenousLanguage = 1
            };

            Academic academic = new Academic
            {
                IdAcademic = 1
            };

            ScholarPeriod period = new ScholarPeriod
            {
                IdScholarPeriod = 1
            };

            Practitioner newPractitioner = new Practitioner
            {
                Matricula = "s18012168",
                Password = "1235456",
                Grade = "8.5",
                Gender = "Masculino",
                Names = "Abizair",
                LastName = "Suarez carrasco",
                Speaks = lengua,
                Status = 1,
                Instructed = academic,
                ScholarPeriod = period
            };

            bool result = practitionerDAO.SavePractitioner(newPractitioner);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeletePractitioner_PractitionerExist_ReturnTrue()
        {
            int idPractitioner = 4;

            bool result = practitionerDAO.DeletePractitioner(idPractitioner);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void AssignPractitioner_ProjectAndPractitionerExist_ReturnTrue()
        {
            int idPractitioner = 4;
            int idProject = 1;

            bool result = practitionerDAO.AssignPractitioner(idPractitioner, idProject);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllPractitioner_HowManyPractitionerExist_ReturnPractitionerList()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitioner();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllPractitionerByindigenousLanguage_WhatPractitionerSpeaksAnotherLanguage_ReturnPractitionerList()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitionerByindigenousLanguage();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetPractitioner_PractitionerExist_ReturnPractitioner()
        {
            int idPractitioner = 1;

            Practitioner result = practitionerDAO.GetPractitioner(idPractitioner);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllPractitionerByAcademic_AcademicHasStudents_ReturnPractitionerList()
        {
            int idAcademic = 1;

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByAcademic(idAcademic);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllPractitionerByProject_ProjectHasPractitioner_ReturnPractitionerList()
        {
            int idProject = 1;

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByProject(idProject);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void UpdatePractitionerGrade_PractitionerExist_ReturnTrue()
        {
            bool result = practitionerDAO.UpdatePractitionerGrade(4);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllPractitionerByLinkedOrganization_OrganizationHasPractitioners_ReturnPractitionerList()
        {
            int idOrganization = 1;

            List<Practitioner> result = practitionerDAO.GetAllPractitionerByLinkedOrganization(idOrganization);

            Assert.IsTrue(result.Count > 0);
        }
    }
    */
}
