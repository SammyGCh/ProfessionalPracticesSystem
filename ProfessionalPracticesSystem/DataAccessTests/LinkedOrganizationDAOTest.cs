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
        public void SaveLinkedOrganization_NewLinkedOrganization_SuccessInsert()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            LinkedOrganization linkedOrganization = new LinkedOrganization
            {
                City = "XALAPA",
                Email = "casaahued@empresa.org.mx",
                State = "VERACRUZ",
                Name = "CASA AHUED",
                TelephoneNumber = "2288151157",
                Address = "FRANCISCO JAVIER CLAVIJERO 287",
                BelongsTo = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector)
            };

            bool isSaved = linkedOrganizationDao.SaveLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllLinkedOrganizations_AvailableLinkedOrganizations_ListWithElements()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetAllLinkedOrganizations();

            Assert.IsTrue(linkedOrganizations.Count > 0);
        }

        [TestMethod]
        public void GetLinkedOrganizationById_KownLinkedOrganizationId_LinkedOrganizationWithSameId()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 1;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.AreEqual(idLinkedOrganization, linkedOrganization.IdLinkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationById_UnknownLinkedOrganizationId_NullObject()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 0;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationByName_KnownLinkedOrganizationName_LinkedOrganizationWithSameName()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.AreEqual(linkedOrganizationName, linkedOrganization.Name);
        }

        [TestMethod]
        public void GetLinkedOrganizationByName_UnkownLinkedOrganizationName_NullObject()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR12344";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationBySector_KnownOrganizationSector_LinkedOrganizationsWithSameSector()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            Assert.AreEqual(organizationSector.Name, linkedOrganizations[0].BelongsTo.Name);
        }

        [TestMethod]
        public void GetLinkedOrganizationBySector_KnownOrganizationSector_ListWithElements()
        {

            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            Assert.IsTrue(linkedOrganizations.Count > 0);
        }

        [TestMethod]
        public void GetLinkedOrganizationBySector_UnkownOrganizationSector_ListWithoutElements()
        {

            OrganizationSector organizationSector = new OrganizationSector
            {
                IdOrganizationSector = 0
            };

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            Assert.IsFalse(linkedOrganizations.Count > 0);
        }

        [TestMethod]
        public void UpdateLinkedOrganization_LinkedOrganizationUpdated_SuccessUpdated()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            int idLinkedOrganization = 1;

            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);
            linkedOrganization.Email = "promor123456@empresa.org.mx";

            bool isUpdated = linkedOrganizationDao.UpdateLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isUpdated);
        }
    }
}
