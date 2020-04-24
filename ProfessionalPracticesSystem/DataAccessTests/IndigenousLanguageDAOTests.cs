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
    public class IndigenousLanguageDAOTests
    {

        [TestMethod]
        public void GetIndigenousLanguageById()
        {
            IndigenousLanguageDAO indigenousLanguageDao;
            indigenousLanguageDao = new IndigenousLanguageDAO();
            int idLanguage = 1;
            IndigenousLanguage indigenousLanguage = indigenousLanguageDao.GetIndigenousLanguageById(idLanguage);

            Assert.IsNull(indigenousLanguage);
        }
        [TestMethod]
        public void GetAllIndigenousLanguages()
        {
            IndigenousLanguageDAO indigenousLanguageDao;
            indigenousLanguageDao = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguages = indigenousLanguageDao.GetAllIndigenousLanguages();
            int expectedResult = 4;
            int obtainedResult = indigenousLanguages.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }
    }
}
