using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDomain;
using DataAccess.Implementation;

namespace DataAccessTests
{
    [TestClass]
    public class UpdateDataTest
    {

        [TestMethod]
        public void UpdateMensualReport_EditMensualReportNoConnection_Success()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();
            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                IdMensualReport = 5,
                Description = "Ahora me acaban de pedir que les haga tambien otros trabajos no puede ser" +
                "y lo peor es que estan en ensamblador como es eso ",
                MonthReportedDate = "2020 - 09 - 07 13:19:00",
                MensualReportName = "Reporte #419",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };



            bool isUpdated = mensualReportDao.UpdateMensualReport(mensualReport);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateMensualReport_EditMensualReportNoConnection_Unsuccess()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();
            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                IdMensualReport = 5,
                Description = "Ya me voy a dar de baja la verdad esta bien feo todo esto" +
                "nos vemos en repite maestro",
                MonthReportedDate = "2020 - 09 - 08 22:10:00",
                MensualReportName = "Reporte #420",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };



            bool isUpdated = mensualReportDao.UpdateMensualReport(mensualReport);

            Assert.IsFalse(isUpdated);
        }
    }
}
