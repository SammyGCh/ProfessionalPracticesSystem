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
    public class OrganizationSectorDAOTest
    {
        [TestMethod]
        public void GetAllOrganizationSectors_AvailableOrganizationSectors_ListWithElements()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> organizationSectors = organizationSectorDao.GetAllOrganizationSectors();

            Assert.IsTrue(organizationSectors.Count > 0);
        }

        [TestMethod]
        public void GetOrganizationSectorById_KnownOrganizationSector_OrganizationSectorWithSameId()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.AreEqual(idOrganizationSector, organizationSector.IdOrganizationSector);
        }

        [TestMethod]
        public void GetOrganizationSectorById_UnknownOrganizationSector_NullObject()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 0;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.IsNull(organizationSector);
        }
    }
}
