/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;


namespace DataAccess.Interfaces
{
    public interface IOrganizationSectorDAO
    {
        List<OrganizationSector> GetAllOrganizationSectors();
        OrganizationSector GetOrganizationSectorById(int idOrganizationSector);
        OrganizationSector GetOrganizationSectorByName(String sectorName);
    }
}
