/*
        Date: 02/07/2020                              
        Author:Cesar Sergio Martinez Palacios
 */
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
using DataAccess.Implementation;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para DisplayPractitionersStatistics.xaml
    /// </summary>
    public partial class DisplayPractitionersStatistics : Page
    {
        private readonly List<string> genderList;
        public DisplayPractitionersStatistics()
        {
            InitializeComponent();
            PractitionerDAO practitionerDAO = new PractitionerDAO();
            List<BusinessDomain.Practitioner> allPractitioners = practitionerDAO.GetAllPractitionerByMatricula();
            if (allPractitioners.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
                NavigationService.GoBack();
            }
            else
            {
                tableOfPractitioners.ItemsSource = allPractitioners;
            }
            genderList = new List<String>
            {
                "Masculino",
                "Femenino"
            };
            gender.ItemsSource = genderList;

        }
        private void GeneratePDF(object sender, RoutedEventArgs e)
        {

        }

        private void CancelView(object sender, RoutedEventArgs e)
        {

        }
    }
}
