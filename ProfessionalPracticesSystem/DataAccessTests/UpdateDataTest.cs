using System;
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class UpdateDataTest
    {
        readonly PractitionerDAO practitionerDAO = new PractitionerDAO();
        readonly DocumentDAO documentDAO = new DocumentDAO();
        readonly AcademicDAO academicDAO = new AcademicDAO();

        [TestMethod]
        public void UpdatePractitionerGrade_PractitionerExist_ReturnTrue()
        {
            bool result = practitionerDAO.UpdatePractitionerGrade(9);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdatePractitioner_PractitionerExist_ReturnTrue()
        {
            IndigenousLanguage auxiliarIndigenousLanguage = new IndigenousLanguage()
            {
                IdIndigenousLanguage = 1
            };

            Academic auxiliarAcademic = new Academic()
            {
                IdAcademic = 1
            };

            ScholarPeriod auxiliarScholarPeriod = new ScholarPeriod()
            {
                IdScholarPeriod = 1
            };

            Practitioner newPractitioner = new Practitioner()
            {
                IdPractitioner = 9,
                Matricula = "S56565656",
                Gender = "Masculino",
                Names = "Usbaldo",
                LastName = "Martinez",
                Speaks = auxiliarIndigenousLanguage,
                Status = 1,
                Instructed = auxiliarAcademic,
                ScholarPeriod = auxiliarScholarPeriod

            };

            bool result = practitionerDAO.UpdatePractitioner(newPractitioner);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdatePractitionerPassword_PractitionerExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            int idPractitioner = 9;

            bool result = practitionerDAO.UpdatePractitionerPassword(idPractitioner, newPassword);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeletePractitioner_PractitionerExist_ReturnTrue()
        {
            int idPractitioner = 9;

            bool result = practitionerDAO.DeletePractitioner(idPractitioner);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void AssignPractitioner_ProjectAndPractitionerExist_ReturnTrue()
        {
            int idPractitioner = 9;
            int idProject = 9;

            bool result = practitionerDAO.AssignPractitioner(idPractitioner, idProject);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateDocumentGrade_DocumentExist_ReturnTrue()
        {
            int idDocument = 15;
            String grade = "10";

            bool result = documentDAO.UpdateDocumentGrade(idDocument, grade);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAcademic_AcademicExist_ReturnTrue()
        {
            int idAcademic = 4;

            bool result = academicDAO.DeleteAcademic(idAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAcademic_AcademicExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            AcademicType auxiliarAcademicType = new AcademicType()
            {
                IdAcademicType = 2
            };

            Academic changeAcademic = new Academic()
            {
                IdAcademic = 4,
                Names = "Laura",
                LastName = "Sanchez Morales",
                PersonalNumber = "S12345678",
                Password = newPassword,
                Cubicle = "00",
                Gender = "Femenino",
                Shift = "Pendiente",
                BelongTo = auxiliarAcademicType

            };

            bool result = academicDAO.UpdateAcademic(changeAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAcademicPassword_AcademicExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            int idAcademic = 4;

            bool result = academicDAO.UpdateAcademicPassword(idAcademic,newPassword);

            Assert.IsTrue(result);
        }
    }
}
