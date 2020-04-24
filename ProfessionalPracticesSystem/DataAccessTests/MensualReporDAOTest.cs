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
        [TestMethod]
        public void GetMensualReportById()
        {
            MensualReportDAO mensualReportDao;
            mensualReportDao = new MensualReportDAO();
            int idMensualReport = 0;
            MensualReport mensualReport = mensualReportDao.GetMensualReportById(idMensualReport);
            Assert.IsNull(mensualReport);
        }

        public void GetAllMensualReport()
        {
            MensualReportDAO mensualReportDao;
            mensualReportDao = new MensualReportDAO();
            List<MensualReport> mensualReports = mensualReportDao.GetAllMensualReports();
            int expectedResult = 1;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }
    }
}
