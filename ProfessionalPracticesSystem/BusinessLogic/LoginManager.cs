/*
    Date: 27/04/2020
    Author(s): César Sergio Martinez Palacios
 */
 
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Implementation;
using BusinessDomain;
using DataAccess;
using System.Diagnostics;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class LoginManager
    {
        private const int PRACTITIONER_USER = 1;
        private const int COORDINADOR_USER = 2;
        private const int PROFESOR_USER = 3;
        private const int ADMINISTRATOR_USER = 4;
        private const int INCORRECT_PASSWORD = 5;
        
        private static int DetectUserRole(string username)
        {
            int roleNumber = 0;
            UserManagement userDetect = new UserManagement();
            roleNumber = userDetect.UserRoleNumber(username);
            return roleNumber;
        }

        public static int UserLog(string username,string password)
        {
            int userNumber = 0;
            int roleNumber = DetectUserRole(username);
            switch (roleNumber)
            {
                case 0:

                    return userNumber;

                case 1:
                    PractitionerDAO practitionerdao = new PractitionerDAO();
                    Practitioner practitioner = practitionerdao.GetPractitionerByMatricula(username);

                    if (password == practitioner.Password)
                    {
                        userNumber = PRACTITIONER_USER;
                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case 2:
                    AcademicDAO coordinatorDao = new AcademicDAO();
                    Academic coordinator = coordinatorDao.GetAcademicByPersonalNumber(username);
                    if (password == coordinator.Password)
                    {

                        userNumber = COORDINADOR_USER;

                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case 3:
                    AcademicDAO profesorDao = new AcademicDAO();
                    Academic profesor = profesorDao.GetAcademicByPersonalNumber(username);
                    if (password == profesor.Password)
                    {
                        userNumber = PROFESOR_USER;
                    }
                    else
                    {
                        userNumber = INCORRECT_PASSWORD;
                    }
                    break;

                case 4:
                    AdministratorDAO administratorDAO = new AdministratorDAO();
                    Administrator administrator = administratorDAO.GetAdministratorByUser(username);
                    if (password == administrator.Password)
                    {

                        userNumber = ADMINISTRATOR_USER;
                        /*AdministratorHome administratorHome = new AdministratorHome();
                        Home homeWindow = new Home(administratorHome, administrator.Username);
                        homeWindow.Show();

                        this.Close();*/
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
