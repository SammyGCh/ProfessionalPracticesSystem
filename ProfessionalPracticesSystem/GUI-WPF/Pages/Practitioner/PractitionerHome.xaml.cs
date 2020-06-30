/*
    Date: 15/05/2020
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
    /// Lógica de interacción para PractitionerHome.xaml
    /// </summary>
    public partial class PractitionerHome : Page
    {
        private BusinessDomain.Practitioner practitioner;

        public PractitionerHome(BusinessDomain.Practitioner practitioner)
        {
            this.practitioner = practitioner;
            InitializeComponent();
        }

        private void ConsultDocumentation(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Documentation(practitioner));
        }

        private void GoToRequestProject(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RequestProject());
        }
    }
}
