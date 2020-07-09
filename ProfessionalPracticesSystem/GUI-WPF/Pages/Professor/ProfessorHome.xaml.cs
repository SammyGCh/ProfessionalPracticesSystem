/*
    Date: 02/07/2020                              
    Author:Cesar Sergio Martinez Palacios
 */
using GUI_WPF.Pages.Notice;
using System;
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
            NavigationService.Navigate(new MensualReportsList(personalNumber));
        }

        private void CheckPartialReports(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PartialReportsList(personalNumber));
        }
    }
}
