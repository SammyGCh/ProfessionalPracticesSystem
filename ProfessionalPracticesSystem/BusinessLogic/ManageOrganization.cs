/*
        Date: 10/06/2020                               
        Author: Cesar Sergio Martinez Palacios
 */
using System;
using System.Collections.Generic;
using System.Text;
using BusinessDomain;
using DataAccess.Implementation;

namespace BusinessLogic
{
    public class ManageOrganization
    {
        private LinkedOrganizationDAO linkedOrganization;

        public ManageOrganization()
        {
            linkedOrganization = new LinkedOrganizationDAO();
        }

        public bool OrganizationSave(LinkedOrganization newOrganization, string sectorName)
        {
            bool isSaved = false;
            OrganizationSectorDAO sectorDao = new OrganizationSectorDAO();
            OrganizationSector sectorOrg = sectorDao.GetOrganizationSectorByName(sectorName);
            newOrganization.BelongsTo = sectorOrg;
            isSaved = linkedOrganization.SaveLinkedOrganization(newOrganization);

            return isSaved;
        }

        public bool OrganizationUpdate(LinkedOrganization updatedOrganization)
        {
            bool isUpdated = false;

            isUpdated = linkedOrganization.UpdateLinkedOrganization(updatedOrganization);
            

            return isUpdated;
        }

    }
}
