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
        public void GetAllDevelopmentStages_AvailableDevelopmentStages_ListWithElements()
        {
            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> developmentStages = developmentStageDao.GetAllDevelopmentStages();

            Assert.IsTrue(developmentStages.Count > 0);
        }

        [TestMethod]
        public void GetDevelopmentStageById_KnownDevelopmentStage_DevelopmentStageWithSameId()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 1;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.AreEqual(idDevelopmentStage, developmentStage.IdDevelopmentStage);
        }

        [TestMethod]
        public void GetDevelopmentStageById_UnkownDevelopmentStage_NullObject()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 0;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.IsNull(developmentStage);
        }
    }
}
