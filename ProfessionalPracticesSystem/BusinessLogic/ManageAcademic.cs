/*
    Date: 17/05/2020
    Author(s) : Ricardo Moguel Sanchez
*/
using System;
using BusinessDomain;
using DataAccess.Implementation;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLogic
{
    public class ManageAcademic
    {
        AcademicDAO academicDAO;
        private const int INVALID_ID = 0;

        public ManageAcademic()
        {
            academicDAO = new AcademicDAO();
        }

        public bool AddAcademic(Academic newAcademic)
        {
            bool isAcademicSaved = false;

            HashManagement hashManager = new HashManagement();

            String encryptedPassword = hashManager.TextToHash(newAcademic.Password);
            newAcademic.Password = encryptedPassword;

            isAcademicSaved = academicDAO.SaveAcademic(newAcademic);

            return isAcademicSaved;
        }

        public bool UpdateAcademic(Academic changedAcademic)
        {
            bool isAcademicUpdated = false;

            isAcademicUpdated = academicDAO.UpdateAcademic(changedAcademic);

            return isAcademicUpdated;
        }

        public bool DeleteAcademic(int deletedAcademicID)
        {
            bool isAcademicDeleted = false;

            if(deletedAcademicID != INVALID_ID)
            {
                isAcademicDeleted = academicDAO.DeleteAcademic(deletedAcademicID);
            }

            return isAcademicDeleted;
        }

        public bool isAcademicCountFull(Academic academic)
        {
            bool isFull = false;

            AcademicDAO academicHandler = new AcademicDAO();

            isFull = academicHandler.ActiveAcademicCountFull(academic.BelongTo.IdAcademicType);

            return isFull;
        }
    }
}
