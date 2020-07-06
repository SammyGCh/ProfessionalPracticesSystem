/*
        Date: 10/06/2020                               
        Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using DataAccess.Implementation;

namespace BusinessLogic
{
    public class ManageOrganization
    {
        private readonly LinkedOrganizationDAO linkedOrganization;

        public ManageOrganization()
        {
            linkedOrganization = new LinkedOrganizationDAO();
        }

        public bool OrganizationSave(LinkedOrganization newOrganization)
        {
            bool isSaved = false;
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
