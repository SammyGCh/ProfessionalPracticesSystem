/*
    Date: 20/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class LinkedOrganizationDAOTest
    {
        [TestMethod]
        public void SaveLinkedOrganizationSuccess()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            LinkedOrganization linkedOrganization = new LinkedOrganization
            {
                City = "XALAPA",
                Email = "promor@empresa.org.mx",
                State = "VERACRUZ",
                Name = "PROMOR",
                TelephoneNumber = "2288151157",
                Address = "FRANCISCO JAVIER CLAVIJERO 287",
                BelongsTo = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector)
            };

            bool isSaved = linkedOrganizationDao.SaveLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllLinkedOrganizationsSuccess()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetAllLinkedOrganizations();

            Assert.IsNotNull(linkedOrganizations);
        }

        [TestMethod]
        public void GetLinkedOrganizationByIdSuccess()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 1;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.IsNotNull(linkedOrganization);
        }

        [TestMethod]
        public void GetUnexistentLinkedOrganizationById()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 0;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationByNameSuccess()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.IsNotNull(linkedOrganization);
        }

        [TestMethod]
        public void GetUnexistentLinkedOrganizationByName()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR12344";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationBySectorSuccess()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            Assert.IsNotNull(linkedOrganizations);
        }

        [TestMethod]
        public void GetLinkedOrganizationByUnexistentSector()
        {

            OrganizationSector organizationSector = new OrganizationSector
            {
                IdOrganizationSector = 0
            };

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            int expectedResult = 0;
            int obtainedResult = linkedOrganizations.Count;

            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void UpdateLinkedOrganizationSuccess()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            int idLinkedOrganization = 1;

            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);
            linkedOrganization.Email = "promor123@empresa.org.mx";

            bool isUpdated = linkedOrganizationDao.UpdateLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isUpdated);
        }
    }
}
