using System;
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class InsertDataTest
    {
        readonly PractitionerDAO practitionerDAO = new PractitionerDAO();
        readonly DocumentDAO documentDAO = new DocumentDAO();
        readonly AcademicDAO academicDAO = new AcademicDAO();

        [TestMethod]
        public void SavePractitioner_PractitionerIsComplete_ReturnTrue()
        {
            IndigenousLanguage lengua = new IndigenousLanguage
            {
                IdIndigenousLanguage = 1
            };

            Academic academic = new Academic
            {
                IdAcademic = 2
            };

            ScholarPeriod period = new ScholarPeriod
            {
                IdScholarPeriod = 1
            };

            Practitioner newPractitioner = new Practitioner
            {
                Matricula = "s18012168",
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
                Name = "Reporte parcial hecho por administrador como prueba",
                Path = "La ruta de guardado no esta disponible",
                TypeOf = documentType,
                AddBy = practitioner
            };

            bool result = documentDAO.SaveDocument(newDocument);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SaveAcademic_AcademicIsComplete_ReturnTrue()
        {
            AcademicType type = new AcademicType
            {
                IdAcademicType = 1
            };

            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            Academic newAcademic = new Academic
            {
                PersonalNumber = "1234564",
                Names = "Mario",
                Cubicle = "41",
                LastName = "Contreras",
                Gender = "Masculino",
                Password = newPassword,
                BelongTo = type,
                Shift = "Matutino",
                Status = 1
            };

            bool result = academicDAO.SaveAcademic(newAcademic);

            Assert.IsTrue(result);
        }
    }
}
