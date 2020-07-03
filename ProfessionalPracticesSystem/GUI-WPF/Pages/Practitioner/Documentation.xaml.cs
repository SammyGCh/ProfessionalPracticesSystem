/*
    Date: 06/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using BusinessDomain;
using GUI_WPF.Windows;
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
        private String practitionerMatricula;
        private const int IDPARTIALREPORT = 1;
        private const int IDSELFASSESSMENT = 3;

        public Documentation(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
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
                NavigationService.Navigate(new GenerateMensualReport(practitionerMatricula));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("La opcion 'Generar reporte mensual' no esta activa");
            }
        }

        private void GenerateSelfassessment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GenerateSelfassessment(practitionerMatricula));
        }

        private void AddPartialReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(IDPARTIALREPORT, practitionerMatricula));
        }

        private void AddSelfAssessment(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDocument(IDSELFASSESSMENT, practitionerMatricula));
        }

        private void GeneratePartialReport(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GeneratePartialReport(practitionerMatricula));
        }
    }
}
