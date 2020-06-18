/*
        Date: 05/06/2020                               
        Author: Cesar Sergio Martinez Palacios
 */
using System;
using System.Collections.Generic;
using System.Text;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualBasic.ApplicationServices;

namespace BusinessLogic
{
    public class UserManagement
    {
        private PractitionerDAO practitionerDao;
        private AcademicDAO academicDao;
        private AdministratorDAO administratorDao;

        public UserManagement()
        {
            practitionerDao = new PractitionerDAO();
            academicDao = new AcademicDAO();
            administratorDao = new AdministratorDAO();
        }

        public bool IsPractitioner(string matricula)
        {
            bool isPractitioner = false;
            Practitioner practitioner = practitionerDao.GetPractitionerByMatricula(matricula);
            if (practitioner != null)
            {
                isPractitioner = true;
            }

            return isPractitioner;
        }

        public bool IsAdministrator(string user)
        {
            bool isAdministrator = false;
            Administrator admin = administratorDao.GetAdministratorByUser(user);

            if(admin != null)
            {
                isAdministrator = true;
            }
            return isAdministrator;
        }

        public bool IsProfesor(string personalNumber)
        {
            bool isProfessor = false;
            Academic professor = academicDao.GetAcademicByPersonalNumber(personalNumber);
            if(professor != null)
            {
                if(professor.BelongTo.IdAcademicType == 2)
                {
                    isProfessor = true;
                }
            }
            return isProfessor;
        }

        public bool IsCoordinator(string personalNumber)
        {
            bool isCoordinator = false;
            Academic coordinator = academicDao.GetAcademicByPersonalNumber(personalNumber);
            if (coordinator != null)
            {
                if (coordinator.BelongTo.IdAcademicType == 1)
                {
                    isCoordinator = true;
                }
            }
            return isCoordinator;
        }

        public int UserRoleNumber(string username)
        {
            bool userDetector = false;

            userDetector = IsPractitioner(username);
            if(userDetector == true)
            {
                return 1;
            }

            userDetector = IsCoordinator(username);
            if (userDetector == true)
            {
                return 2;
            }
            userDetector = IsProfesor(username);
            if (userDetector == true)
            {
                return 3;
            }
            userDetector = IsAdministrator(username);
            if (userDetector == true)
            {
                return 4;
            }
            return 0;
        }
    }
}
