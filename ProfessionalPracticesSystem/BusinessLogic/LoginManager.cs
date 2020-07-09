/*
    Date: 27/04/2020
    Author(s): César Sergio Martinez Palacios
 */
 
using System;
using DataAccess.Implementation;
using BusinessDomain;

namespace BusinessLogic
{
    public class LoginManager
    {
        private const int PRACTITIONER_USER = 1;
        private const int COORDINADOR_USER = 2;
        private const int PROFESOR_USER = 3;
        private const int ADMINISTRATOR_USER = 4;
        private const int INCORRECT_PASSWORD = 5;

        public static int UserLog(string username,string password)
        {
            int userNumber = 0;

            UserManagement userDetect = new UserManagement();
            int roleNumber = userDetect.UserRoleNumber(username);

            HashManagement hashPassword = new HashManagement();
            String hashedPassword = hashPassword.TextToHash(password);
            
            switch (roleNumber)
            {
                case PRACTITIONER_USER:

                    PractitionerDAO practitionerdao = new PractitionerDAO();
                    Practitioner practitioner = practitionerdao.GetPractitionerByMatricula(username);

                    if (hashPassword.CompareHashs(hashedPassword, practitioner.Password) == true)
                    {
                        userNumber = PRACTITIONER_USER;
                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case COORDINADOR_USER:

                    AcademicDAO coordinatorDao = new AcademicDAO();
                    Academic coordinator = coordinatorDao.GetAcademicByPersonalNumber(username);

                    if (hashPassword.CompareHashs(hashedPassword, coordinator.Password) == true)
                    {

                        userNumber = COORDINADOR_USER;

                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case PROFESOR_USER:

                    AcademicDAO profesorDao = new AcademicDAO();
                    Academic profesor = profesorDao.GetAcademicByPersonalNumber(username);

                    if (hashPassword.CompareHashs(hashedPassword, profesor.Password) == true)
                    {
                        userNumber = PROFESOR_USER;
                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case ADMINISTRATOR_USER:

                    AdministratorDAO administratorDAO = new AdministratorDAO();
                    Administrator administrator = administratorDAO.GetAdministratorByUser(username);

                    if (hashPassword.CompareHashs(hashedPassword, administrator.Password) == true)
                    {

                        userNumber = ADMINISTRATOR_USER;
                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                        break;
            }
            return userNumber;
        }


    }
}
