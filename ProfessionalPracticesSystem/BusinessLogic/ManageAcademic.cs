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
        bool isActionPerformed;

        public ManageAcademic()
        {
            academicDAO = null;
            academicTypeDAO = null;
            isActionPerformed = false;
            academicDAO = new AcademicDAO();
            academicTypeDAO = new AcademicTypeDAO();
        }

        public bool addAcademic(Academic academic)
        {
            Academic newAcademic = academic;
            isActionPerformed = academicDAO.SaveAcademic(newAcademic);
            return isActionPerformed;

        }

        public bool eliminateAcademic(int idAcademic)
        {
            int idOldAcademic = idAcademic;
            isActionPerformed = academicDAO.DeleteAcademic(idOldAcademic);
            return isActionPerformed;

        }

        public bool updateAcademic(Academic academic)
        {
            Academic changeAcademic = academic;
            isActionPerformed = academicDAO.UpdateAcademic(changeAcademic);
            return isActionPerformed;
        }


    }
}
