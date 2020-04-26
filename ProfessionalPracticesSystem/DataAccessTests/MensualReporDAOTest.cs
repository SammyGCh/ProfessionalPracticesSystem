/*
    Date: 23/04/2020
    Author(s) : César Sergio Martinez Palacios
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataAccessTests 
{
    [TestClass]
    public class MensualReportDAOTest
    {
        MensualReportDAO mensualReportDao = new MensualReportDAO();

        [TestMethod]
        public void GetMensualReportById()
        {

            int idMensualReport = 0;
            MensualReport mensualReport = mensualReportDao.GetMensualReportById(idMensualReport);
            Assert.IsNull(mensualReport);
        }

        [TestMethod]
        public void GetAllMensualReport()
        {
            
            List<MensualReport> mensualReports = mensualReportDao.GetAllMensualReports();
            int expectedResult = 0;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void Register_MensualReport_Succes()
        {
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
                MensualReportName = "Reporte #2",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };

            bool isSaved = mensualReportDao.InsertMensualReport(mensualReport);

            Assert.IsTrue(isSaved);
        }
    }
}
