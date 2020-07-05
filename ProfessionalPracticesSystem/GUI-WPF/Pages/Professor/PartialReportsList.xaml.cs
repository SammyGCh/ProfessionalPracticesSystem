using BusinessDomain;
using DataAccess.Implementation;
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
