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
    /// Lógica de interacción para MensualReports.xaml
    /// </summary>
    public partial class MensualReportsList : Page
    {
        private String personalNumber;
        public MensualReportsList()
        {
            InitializeComponent();
            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            List<BusinessDomain.MensualReport> mensualReportsList = mensualReportDAO.GetAllReportsByAcademic(personalNumber);
            mensualReportsTable.ItemsSource = mensualReportsList;
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