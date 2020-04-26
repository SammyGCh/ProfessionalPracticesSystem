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
    public class PractitionerDAOTest
    {
        PractitionerDAO practitionerDAO = new PractitionerDAO();

        [TestMethod]
        public void SavePractitionerSuccess()
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
                Grade = 8.5f,
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
        public void DeletePractitionerSuccess()
        {
            int idPractitioner = 4;

            bool result = practitionerDAO.DeletePractitioner(idPractitioner);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void AssignPractitionerSuccess()
        {
            int idPractitioner = 4;
            int idProject = 1;

            bool result = practitionerDAO.AssignPractitioner(idPractitioner, idProject);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllPractitionerSuccess()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitioner();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllPractitionerByindigenousLanguageSuccess()
        {
            List<Practitioner> result = practitionerDAO.GetAllPractitionerByindigenousLanguage();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPractitionerSuccess()
        {
            int idPractitioner = 1;

            Practitioner result = practitionerDAO.GetPractitioner(idPractitioner);

            Assert.IsNotNull(result);
        }
    }
}
