/*
    Date: 27/04/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BusinessLogic;
using BusinessDomain;
using GUI_WPF.Windows;
using GUI_WPF.Pages.Coordinator;
using System.Diagnostics;
using DataAccess.Implementation;
using GUI_WPF.Pages.Administrator;

namespace GUI_WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void LogIn(object sender, RoutedEventArgs e)
        {
            string username = userTextBox.Text;
            int roleNumber = DetectUserRole(username);
            switch (roleNumber)
            {
                case 1:
                    PractitionerDAO practitionerdao = new PractitionerDAO();
                    Practitioner practitioner = practitionerdao.GetPractitionerByMatricula(username);

                    if (password.Password == practitioner.Password)
                    {
                        //aqui va el home de practicante
                    }
                    else
                    {
                       MessageBoxResult logInError = System.Windows.MessageBox.Show("Error contraseña o usuarios no validos", "", MessageBoxButton.OK);
                    }
                    break;

                case 2:
                    AcademicDAO coordinatorDao = new AcademicDAO();
                    Academic coordinator = coordinatorDao.GetAcademicByPersonalNumber(username);
                    if (password.Password == coordinator.Password)
                    {
                        CoordinatorHome coordinatorHome = new CoordinatorHome(coordinator);
                        string coordinatorFullName = coordinator.Names +" " + coordinator.LastName;
                        Home homeWindow = new Home(coordinatorHome, coordinatorFullName);
                        homeWindow.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBoxResult logInError = System.Windows.MessageBox.Show("Error contraseña o usuarios no validos", "", MessageBoxButton.OK);
                    }
                    break;

                case 3:
                    AcademicDAO profesorDao = new AcademicDAO();
                    Academic profesor = profesorDao.GetAcademicByPersonalNumber(username);
                    if (password.Password == profesor.Password)
                    {
                        //aqui va el home de profesor
                        
                        this.Close();
                    }
                    else
                    {
                        MessageBoxResult logInError = System.Windows.MessageBox.Show("Error contraseña o usuarios no validos", "", MessageBoxButton.OK);
                    }
                    break;

                case 4:
                    AdministratorDAO administratorDAO = new AdministratorDAO();
                    Administrator administrator = administratorDAO.GetAdministratorByUser(username);
                    if (password.Password == administrator.Password)
                    {
                        AdministratorHome administratorHome = new AdministratorHome();
                        Home homeWindow = new Home(administratorHome, administrator.Username);
                        homeWindow.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBoxResult logInError = System.Windows.MessageBox.Show("Error contraseña o usuarios no validos", "", MessageBoxButton.OK);
                    }
                    break;
            }
        }

        private void EnableLoginButton(object sender, RoutedEventArgs e)
        {
            if (password.SecurePassword.Length == 0 || userTextBox.Text.Length == 0)
            {
                logInButton.IsEnabled = false;
            }
            else
            {
                logInButton.IsEnabled = true;
            }

        }

        private void ValidateUsername(object sender, TextChangedEventArgs e)
        {
            if (!ValidatorText.IsUserName(userTextBox.Text))
            {
                invalidUsernameLabel.Visibility = Visibility.Visible;
            }
            else
            {
                invalidUsernameLabel.Visibility = Visibility.Hidden;
            }
        }

        private int DetectUserRole(string username)
        {
            int roleNumber = 0;
            UserManagement userDetect = new UserManagement();
            roleNumber = userDetect.UserRoleNumber(username);
            return roleNumber;
        }
    }
}
