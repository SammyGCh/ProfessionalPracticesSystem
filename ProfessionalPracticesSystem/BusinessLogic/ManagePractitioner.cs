/*
        Date: 16/06/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System;
using BusinessDomain;
using DataAccess.Implementation;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class ManagePractitioner
    {
        private readonly PractitionerDAO practitionerDao;
        private const int INVALID_ID = 0;
        private const int EXISTS = 1;

        public ManagePractitioner()
        {
            practitionerDao = new PractitionerDAO();
        }

        public bool AddPractitioner(Practitioner newPractitioner)
        {
            bool isPractitionerSaved = false;

            HashManagement hashManager = new HashManagement();

            String encryptedPassword = hashManager.TextToHash(newPractitioner.Password);
            newPractitioner.Password = encryptedPassword;

            isPractitionerSaved = practitionerDao.SavePractitioner(newPractitioner);

            return isPractitionerSaved;
        }

        public bool UpdatePractitioner(Practitioner updatedPractitioner)
        {
            bool isUpdated = false;

            HashManagement hashManager = new HashManagement();

            String encryptedPassword = hashManager.TextToHash(updatedPractitioner.Password);
            updatedPractitioner.Password = encryptedPassword;

            isUpdated = practitionerDao.UpdatePractitioner(updatedPractitioner);

            return isUpdated;
        }

        public bool CanRequestProject(Practitioner practitionerRequester)
        {
            bool canRequest = true;

            if (practitionerRequester != null)
            {
                ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
                int existsProjectsRequest;
                int idPractitioner = practitionerRequester.IdPractitioner;

                existsProjectsRequest = projectsRequestDao.ExistsProjectsRequestFromPractitioner(idPractitioner);

                if (existsProjectsRequest == EXISTS)
                {
                    canRequest = false;
                }
            }

            return canRequest;
        }
    }
}
