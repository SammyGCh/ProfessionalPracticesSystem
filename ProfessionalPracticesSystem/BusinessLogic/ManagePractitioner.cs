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

            isUpdated = practitionerDao.UpdatePractitioner(updatedPractitioner);

            return isUpdated;
        }

        public bool CanRequestProject(String practitionerMatricula)
        {
            bool canRequest = true;

            if (!String.IsNullOrWhiteSpace(practitionerMatricula))
            {
                ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
                int existsProjectsRequest;

                existsProjectsRequest = projectsRequestDao.ExistsProjectsRequestFromPractitioner(practitionerMatricula);

                if (existsProjectsRequest == EXISTS)
                {
                    canRequest = false;
                }
            }

            return canRequest;
        }

        public bool DeletePractitioner(int deletedPractitionerID)
        {
            bool isDeleted = false;

            isDeleted = practitionerDao.DeletePractitioner(deletedPractitionerID);

            return isDeleted;
        }
    }
}
