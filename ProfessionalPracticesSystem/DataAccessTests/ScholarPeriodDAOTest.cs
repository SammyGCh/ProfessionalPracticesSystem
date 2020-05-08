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
        public void SaveScholarPeriod_NewScholarPeriod_SuccessInsert()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            ScholarPeriod scholarPeriod = new ScholarPeriod
            {
                Name = "AGOST0 2021 - ENERO 2022"
            };

            bool isSaved = scholarPeriodDao.SaveScholarPeriod(scholarPeriod);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllScholarPeriod_AvailableScholarPeriods_ListWithElements()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            List<ScholarPeriod> scholarPeriods = scholarPeriodDao.GetAllScholarPeriods();

            Assert.IsTrue(scholarPeriods.Count > 0);
        }

        [TestMethod]
        public void GetScholarPeriodById_KnownScholarPeriod_ScholarPeriodWithSameId()
        {
            int idScholarPeriod = 1;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.AreEqual(idScholarPeriod, scholarPeriod.IdScholarPeriod);
        }

        [TestMethod]
        public void GetScholarPeriodId_UnknownScholarPeriodId_NullObject()
        {
            int idScholarPeriod = 0;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.IsNull(scholarPeriod);
        }
    }
}
