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
        private static PractitionerDAO practitionerDao;
        private static AcademicDAO academicDao;
        private static AdministratorDAO administratorDao;
        private const int NO_USER = 0;
        private const int PRACTITIONER_USER = 1;
        private const int COORDINADOR_USER = 2;
        private const int PROFESOR_USER = 3;
        private const int ADMINISTRATOR_USER = 4;

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
                return PRACTITIONER_USER;
            }

            userDetector = IsCoordinator(username);
            if (userDetector == true)
            {
                return COORDINADOR_USER;
            }
            userDetector = IsProfesor(username);
            if (userDetector == true)
            {
                return PROFESOR_USER;
            }
            userDetector = IsAdministrator(username);
            if (userDetector == true)
            {
                return ADMINISTRATOR_USER;
            }
            return NO_USER;
        }

        public static string GetUserName(int userNumber, string userName)
        {
            string userCompleteName=" ";
            switch (userNumber)
            {
                case 1:
                    Practitioner practitioner = practitionerDao.GetPractitionerByMatricula(userName);
                    userCompleteName = practitioner.Names + " " + practitioner.LastName;
                    break;
                case 2:
                    Academic coordinator = academicDao.GetAcademicByPersonalNumber(userName);
                    userCompleteName = coordinator.Names + " " + coordinator.LastName;
                    break;
                case 3:
                    Academic professor = academicDao.GetAcademicByPersonalNumber(userName);
                    userCompleteName = professor.Names + " " + professor.LastName;
                    break;
                case 4:
                    userCompleteName = "Administrador";
                    break;
            }
            return userCompleteName;
        }
    }
}
