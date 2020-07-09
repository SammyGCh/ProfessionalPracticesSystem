/*
    Date: 02/07/2020                              
    Author:Cesar Sergio Martinez Palacios
 */
using DataAccess.Implementation;
using GUI_WPF.Windows;
using GUI_WPF.Pages.Notice;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace GUI_WPF.Pages.Professor
{
    /// <summary>
    /// Lógica de interacción para ProfessorHome.xaml
    /// </summary>
    
    public partial class ProfessorHome : Page
    {
        private readonly String personalNumber;

        private bool IsCheckPartialReportsAvailable()
        {
            bool isActive = false;

            DocumentDAO documentDAO = new DocumentDAO();
            List<BusinessDomain.Document> partialReportsList = documentDAO.GetAllPartialReportByAcademic(personalNumber);

            if (partialReportsList.Count > 0)
            {
                isActive = true;
            }

            return isActive;
        }

        private bool IsCheckMensualReportsAvailable()
        {
            bool isActive = false;

            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            List<BusinessDomain.MensualReport> mensualReportsList = mensualReportDAO.GetAllReportsByAcademic(personalNumber);

            if (mensualReportsList.Count > 0)
            {
                isActive = true;
            }

            return isActive;
        }

        public ProfessorHome(String personalNumber)
        {
            InitializeComponent();
            this.personalNumber = personalNumber;
        }

        private void CheckPractitioners(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfessorPractitionerList(personalNumber));
        }

        private void CheckNotices(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NoticeBoard( personalNumber));
        }

        private void CheckMensualReports(object sender, RoutedEventArgs e)
        {
            if (IsCheckMensualReportsAvailable())
            {
                NavigationService.Navigate(new MensualReportsList(personalNumber));
            }
            else
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
            }
        }

        private void CheckPartialReports(object sender, RoutedEventArgs e)
        {
            if (IsCheckPartialReportsAvailable())
            {
                NavigationService.Navigate(new PartialReportsList(personalNumber));
            }
            else
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
            }
        }
    }
}
