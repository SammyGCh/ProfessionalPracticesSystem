/*
    Date: 17/04/2020
    Author(s) : Sammy Guadarrama Chávez
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
    public class OrganizationSectorDAOTest
    {
        [TestMethod]
        public void GetAllOrganizationSectorsSuccess()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> organizationSectors = organizationSectorDao.GetAllOrganizationSectors();
            int expectedResult = 1;
            int obtainedResult = organizationSectors.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetOrganizationSectorByIdSuccess()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.IsNotNull(organizationSector);
        }

        [TestMethod]
        public void GetAllOrganizationSectorsUnSuccess()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> organizationSectors = organizationSectorDao.GetAllOrganizationSectors();
            int expectedResult = 0;
            int obtainedResult = organizationSectors.Count;
            Assert.AreNotEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetOrganizationSectorByIdUnSuccess()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 0;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.IsNull(organizationSector);
        }
    }
}
