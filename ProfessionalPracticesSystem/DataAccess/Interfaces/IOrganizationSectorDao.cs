/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;


namespace DataAccess.Interfaces
{
    public interface IOrganizationSectorDao
    {
        List<OrganizationSector> GetAllOrganizationSectors();
        OrganizationSector GetOrganizationSector(int idOrganizationSector);
    }
}
