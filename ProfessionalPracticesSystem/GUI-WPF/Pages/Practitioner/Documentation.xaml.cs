/*
    Date: 06/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
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

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para Documentation.xaml
    /// </summary>
    public partial class Documentation : Page
    {
        private BusinessDomain.Practitioner practitioner;
        private const int IDPARTIALREPORT = 1;
        private const int IDFINALREPORT = 2;
        private const int IDSELFASSESSMENT = 3;

        public Documentation(BusinessDomain.Practitioner practitioner)
        {
            this.practitioner = practitioner;
            InitializeComponent();
        }

        public bool IsTheOptionActivated()
        {
            return (int.Parse(DateTime.Now.Day.ToString()) < 8);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GenerateMensualReport(object sender, RoutedEventArgs e)
        {
            if (IsTheOptionActivated())
            {
                NavigationService.Navigate(new GenerateMensualReport(practitioner));
            }
            else
            {
                MessageBox.Show("La opcion 'Generar reporte mensual' no esta activa","Funcion no habilitada",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void GenerateSelfassessment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenerateSelfassessment(practitioner));
        }

        private void AddPartialReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(IDPARTIALREPORT,practitioner.IdPractitioner));
        }

        private void AddFinalReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(IDFINALREPORT, practitioner.IdPractitioner));
        }

        private void AddSelfAssessment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(IDSELFASSESSMENT, practitioner.IdPractitioner));
        }

        private void GeneratePartialReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GeneratePartialReport(practitioner));
        }
    }
}
