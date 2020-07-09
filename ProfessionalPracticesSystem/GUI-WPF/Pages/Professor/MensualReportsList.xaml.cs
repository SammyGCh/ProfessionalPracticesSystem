/*
    Date: 04/07/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI_WPF.Pages.Professor
{
    /// <summary>
    /// Lógica de interacción para MensualReports.xaml
    /// </summary>
    public partial class MensualReportsList : Page
    {
        public MensualReportsList(String personalNumber)
        {
            InitializeComponent();
            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            List<BusinessDomain.MensualReport> mensualReportsList = mensualReportDAO.GetAllReportsByAcademic(personalNumber);


            if (mensualReportsList.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
            }
            else
            {
                mensualReportsTable.ItemsSource = mensualReportsList;           
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Evaluation(object sender, RoutedEventArgs e)
        {
            MensualReport reportSelected = (MensualReport)mensualReportsTable.SelectedItem;

            EvaluateMensualReport mensualReportPage = new EvaluateMensualReport()
            {
                DataContext = reportSelected
            };

            NavigationService.Navigate(mensualReportPage);
        }
    }
}