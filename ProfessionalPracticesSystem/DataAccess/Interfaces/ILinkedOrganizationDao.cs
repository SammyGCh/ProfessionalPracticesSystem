/*
    Date: 07/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface ILinkedOrganizationDao
    {
        Boolean SaveLinkedOrganization(LinkedOrganization linkedOrganization);
        List<LinkedOrganization> GetAllLinkedOrganizations();
        LinkedOrganization GetLinkedOrganization(int idLinkedOrganization);
        LinkedOrganization GetLinkedOrganization(String name);
        List<LinkedOrganization> GetLinkedOrganizationBySector(OrganizationSector organizationSector);
        Boolean UpdateLinkedOrganization(LinkedOrganization linkedOrganizationUpdated);
    }
}
