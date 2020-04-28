/*
    Date: 22/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class ScholarPeriodDAOTest
    {
        [TestMethod]
        public void SaveScholarPeriodSuccess()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            ScholarPeriod scholarPeriod = new ScholarPeriod
            {
                Name = "AGOST0 2020 - ENERO 2021"
            };

            bool isSaved = scholarPeriodDao.SaveScholarPeriod(scholarPeriod);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllScholarPeriodSuccess()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            List<ScholarPeriod> scholarPeriods = scholarPeriodDao.GetAllScholarPeriods();

            Assert.IsTrue(scholarPeriods.Count > 0);
        }

        [TestMethod]
        public void GetScholarPeriodByIdSuccess()
        {
            int idScholarPeriod = 1;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.IsNotNull(scholarPeriod);
        }

        [TestMethod]
        public void NotGetScholarPeriodByUnknownId()
        {
            int idScholarPeriod = 0;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.IsNull(scholarPeriod);
        }
    }
}
