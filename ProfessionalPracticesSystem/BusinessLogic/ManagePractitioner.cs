/*
        Date: 16/06/2020                              
        Author:Ricardo Moguel Sanchez
 */
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
            bool isPractitionerSaved;

            isPractitionerSaved = practitionerDao.SavePractitioner(newPractitioner);

            return isPractitionerSaved;
        }

        public bool UpdatePractitioner(Practitioner updatedPractitioner)
        {
            bool isUpdated;

            isUpdated = practitionerDao.UpdatePractitionerData(updatedPractitioner);

            return isUpdated;
        }
    }
}
