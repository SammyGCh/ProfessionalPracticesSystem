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

            int idMensualReport = 1;
            MensualReport mensualReport = mensualReportDao.GetMensualReportById(idMensualReport);
            Assert.IsNotNull(mensualReport);
        }

        [TestMethod]
        public void GetAllMensualReport()
        {
            
            List<MensualReport> mensualReports = mensualReportDao.GetAllMensualReports();
            int expectedResult = 3;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void Register_MensualReport_Success()
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
                MensualReportName = "Reporte #412",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };

            bool isSaved = mensualReportDao.InsertMensualReport(mensualReport);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void Delete_MensualReport_Success()
        {
            int idReport = 2;
            bool isDeleted = mensualReportDao.DeleteMensualReport(idReport);
            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void Update_MensualReport_Success()
        {
            int idProject = 6;
            MensualReport mensualReport = mensualReportDao.GetMensualReportById(idProject);
            mensualReport.Description = "Hoy decidi por fin hacer el nuevo codigo que mis compañeros de trabajo no hicieron y se aprovechan" +
                "de mi como practicante al que no pagan y no es justo doctor";
            mensualReport.MonthReportedDate = "2020 - 01 - 04 22:10:00";



            bool isUpdated = mensualReportDao.UpdateMensualReport(mensualReport);

            Assert.IsTrue(isUpdated);
        }

        //[TestMethod]
        //public void ByPractitioner_GetMensualReport_Success()
        //{
        //    int idPractitioner = 1;
        //    List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByPractitioner(idPractitioner);

        //    int expectedResult = 5;
        //    int obtainedResult = mensualReports.Count;
        //    Assert.AreEqual(expectedResult, obtainedResult);
        //}

        //[TestMethod]
        //public void ByProject_GetMensualReport_Success()
        //{
        //    int idProject = 1;
        //    List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByPractitioner(idProject);

        //    int expectedResult = 5;
        //    int obtainedResult = mensualReports.Count;
        //    Assert.AreEqual(expectedResult, obtainedResult);
        //}
    }
}
