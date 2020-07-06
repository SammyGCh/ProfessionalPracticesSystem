using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;

namespace DataAccessTests
{
    [TestClass]
    public class InsertDataTest
    {
        [TestMethod]
        public void SaveMensualReport_NewReport_SuccesInserting()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();

            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                Description = "Hoy trabaje en las pruebas de las clases DAO de mi projecto en PROM, cada dia es mas dificil poder trabajar aqui" +
                "debido al mal trato de los trabajadores que al verme como estudiante se aprovechan mas de mi y tengo que trabajar mucho mas" +
                "para que poder pasar esta materia, auxilio doctor ocharan ayuda",
                MonthReportedDate = "2020 - 09 - 04 22:10:00",
                MensualReportName = "Reporte #412",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };

            bool isSaved = mensualReportDao.InsertMensualReport(mensualReport);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void SaveMensualReport_NewReport_UnsuccesInserting()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();

            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                Description = "Los trabajos son cada vez mas pesados, dios mio no creo aguantar esto" +
                "es imposible, exigo un cambio ayuda",
                MonthReportedDate = "2020 - 09 - 07 20:10:00",
                MensualReportName = "Reporte #420",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };

            bool isSaved = mensualReportDao.InsertMensualReport(mensualReport);

            Assert.IsFalse(isSaved);
        }
    }
}
