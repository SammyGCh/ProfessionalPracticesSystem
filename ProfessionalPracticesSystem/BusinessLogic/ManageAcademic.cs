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
        AcademicTypeDAO academicTypeDAO;

        public ManageAcademic()
        {
            academicDAO = new AcademicDAO();
            academicTypeDAO = new AcademicTypeDAO();
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

        public bool EliminateAcademic(int idOldAcademic)
        {
            bool isAcademicDeleted = false;
            isAcademicDeleted = academicDAO.DeleteAcademic(idOldAcademic);
            return isAcademicDeleted;

        }

        public bool UpdateAcademic(Academic changedAcademic)
        {
            bool isAcademicUpdated = false;

            HashManagement hashManager = new HashManagement();

            String encryptedPassword = hashManager.TextToHash(changedAcademic.Password);
            changedAcademic.Password = encryptedPassword;

            isAcademicUpdated = academicDAO.UpdateAcademic(changedAcademic);
            return isAcademicUpdated;
        }


    }
}
