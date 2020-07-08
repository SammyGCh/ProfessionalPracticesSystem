/*
    Date: 18/06/2020
    Author(s): César Sergio Martinez Palacios
 */

using System;
using System.Windows;
using System.Windows.Controls;
using BusinessLogic;
using GUI_WPF.Windows;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Pages.Administrator;
using GUI_WPF.Pages.Practitioner;
using GUI_WPF.Pages.Professor;

namespace GUI_WPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();        
        }
        
       private void LogIn(object sender, RoutedEventArgs e)
        {
            String passwordEntered = password.Password;
            String usernameEntered = userTextBox.Text;

            int userID = LoginManager.UserLog(usernameEntered,passwordEntered);

            switch (userID)
            {
                case 0:

                    DialogWindowManager.ShowErrorWindow("No se ha ingresado un usario valido");

                    break;

                case 1:

                    PractitionerHome practitionerHome = new PractitionerHome(usernameEntered);
                    string practitionerFullName = UserManagement.GetUserName(userID, usernameEntered);
                    Home homeWindowPractitioner = new Home(practitionerHome, practitionerFullName);

                    homeWindowPractitioner.Show();
                    this.Close();

                    break;

                case 2:

                    CoordinatorHome coordinatorHome = new CoordinatorHome();
                    string coordinatorFullName = UserManagement.GetUserName(userID, usernameEntered);
                    Home coordinatorHomeWindow = new Home(coordinatorHome, coordinatorFullName);

                    coordinatorHomeWindow.Show();
                    this.Close();

                    break;

                case 3:

                    ProfessorHome professorHome = new ProfessorHome(usernameEntered);
                    string professorFullName = UserManagement.GetUserName(userID, usernameEntered);
                    Home professorHomeWindow = new Home(professorHome, professorFullName);

                    professorHomeWindow.Show();
                    this.Close();

                    break;

                case 4:

                    AdministratorHome administratorHome = new AdministratorHome();
                    Home adminHomeWindow = new Home(administratorHome, "Administrador");

                    adminHomeWindow.Show();
                    this.Close();

                    break;

                case 5:

                    DialogWindowManager.ShowErrorWindow("La contraseña o usuario es incorrecta");

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
    }
}
