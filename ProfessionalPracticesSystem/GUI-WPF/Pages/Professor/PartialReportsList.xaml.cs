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
    /// Lógica de interacción para PartialReportsList.xaml
    /// </summary>
    public partial class PartialReportsList : Page
    {
        public PartialReportsList(String personalNumber)
        {
            InitializeComponent();

            DocumentDAO documentDAO = new DocumentDAO();
            List<BusinessDomain.Document> partialReportsList = documentDAO.GetAllPartialReportByAcademic(personalNumber);

            partialReportsTable.ItemsSource = partialReportsList;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Evaluation(object sender, RoutedEventArgs e)
        {
            Document reportSelected = (Document)partialReportsTable.SelectedItem;

            EvaluatePartialReport partialReportPage = new EvaluatePartialReport(reportSelected);

            NavigationService.Navigate(partialReportPage);
        }
    }
}
