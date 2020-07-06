/*
    Date: 25/04/2020
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
    public class AcademicTypeDAOTest
    {
        /*
        AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();

        [TestMethod]
        public void GetById_AcademicType_Sucess()
        {
            int idType = 2;
            AcademicType academicType = academicTypeDao.GetAcademicTypeById(idType);
            Assert.IsNotNull(academicType);
        }

        [TestMethod]
        public void ListAll_AcademicType_Succese()
        {
            
            List<AcademicType> academicTypes = academicTypeDao.GetAllAcademicTypes();
            int expectedResult = 3;
            int obtainedResult = academicTypes.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void Register_AcademicType_True()
        {
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            AcademicType type = new AcademicType
            {
                AcademicTypeName = "coordinador"
            };

            bool isSaved = academicTypeDao.InsertAcademicType(type);
            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void Delete_AcademicType_True()
        {
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            int idType = 3;
            bool isDeleted = academicTypeDao.DeleteAcademicTypeById(idType);

            Assert.IsTrue(isDeleted);
        }
        */
    }

}
