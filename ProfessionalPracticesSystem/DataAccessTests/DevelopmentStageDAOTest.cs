/*
    Date: 17/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class DevelopmentStageDAOTest
    {
        [TestMethod]
        public void GetAllDevelopmentStagesSuccess()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> developmentStages = developmentStageDao.GetAllDevelopmentStages();
            Assert.IsNotNull(developmentStages);
        }

        [TestMethod]
        public void GetDevelopmentStageByIdSuccess()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 1;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.IsNotNull(developmentStage);
        }

        [TestMethod]
        public void GetAllDevelopmentStagesUnsuccess()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> developmentStages = developmentStageDao.GetAllDevelopmentStages();

            int expectedResult = 0;
            int obtainedResult = developmentStages.Count;
            Assert.AreNotEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetOrganizationSectorByIdUnsuccess()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 0;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.IsNull(developmentStage);
        }
    }
}
