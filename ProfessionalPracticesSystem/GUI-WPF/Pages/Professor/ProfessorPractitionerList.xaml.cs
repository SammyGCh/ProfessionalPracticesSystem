/*
    Date: 04/07/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using DataAccess.Implementation;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI_WPF.Pages.Professor
{
    /// <summary>
    /// Lógica de interacción para ProfessorPractitionerList.xaml
    /// </summary>
    public partial class ProfessorPractitionerList : Page
    {
        public ProfessorPractitionerList(String personalNumber)
        {
            InitializeComponent();
            PractitionerDAO practitionerDAO = new PractitionerDAO();
            List<BusinessDomain.Practitioner> practitionerList = practitionerDAO.GetAllPractitionerByAcademic(personalNumber);

            if (practitionerList.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
                NavigationService.GoBack();
            }
            else
            {
                practitionersTable.ItemsSource = practitionerList;
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShowDetails(object sender, RoutedEventArgs e)
        {
            BusinessDomain.Practitioner selectedPractitioner = (BusinessDomain.Practitioner)practitionersTable.SelectedItem;
            NavigationService.Navigate(new DisplayPractitioner(selectedPractitioner));
        }
    }
}
