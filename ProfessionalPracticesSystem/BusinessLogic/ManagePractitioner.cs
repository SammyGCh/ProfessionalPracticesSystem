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
        private PractitionerDAO practitionerDao;
        private const int INVALID_ID = 0;

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

        public bool DeletePractitioner(int deletedPractitionerID)
        {
            bool isDeleted = false;

            isDeleted = practitionerDao.DeletePractitioner(deletedPractitionerID);

            return isDeleted;
        }
    }
}
