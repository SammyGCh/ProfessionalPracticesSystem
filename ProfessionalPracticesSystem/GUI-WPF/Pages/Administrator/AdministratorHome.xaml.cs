/*
    Date: 05/05/2020
    Author(s): Ricardo Moguel Sabchez
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess.Implementation;
using System.Collections.ObjectModel;
namespace GUI_WPF.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorHome.xaml
    /// </summary>
    public partial class AdministratorHome : Page
    {
        private readonly ObservableCollection<Academic> academics = null;
        private Academic selectedAcademic;

        public AdministratorHome()
        {
            InitializeComponent();

            AcademicDAO academicHandler = new AcademicDAO();
            List<Academic> listOfAcademics = academicHandler.GetAllAcademic();

            if (listOfAcademics.Count == 0)
            {
                DialogWindowManager.ShowErrorWindow("No existen Academicos. Debe crear un Academico");
                NavigationService.Navigate(new AddAcademic());
            }
            else
            {
                academics = new ObservableCollection<Academic>(listOfAcademics);
                tableOfAcademics.ItemsSource = academics;
            }
        }

        private void AddNewAcademic(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAcademic());
        }

        private void DisplayAcademicData(object sender, RoutedEventArgs e)
        {
            selectedAcademic = (Academic)tableOfAcademics.SelectedItem;
            NavigationService.Navigate(new DetailAcademic(selectedAcademic));
        }

        private void UpdateAcademicData(object sender, RoutedEventArgs e)
        {
            selectedAcademic = (Academic)tableOfAcademics.SelectedItem;
            NavigationService.Navigate(new UpdateAcademic(selectedAcademic));
        }

        private void DeleteAcademic(object sender, RoutedEventArgs e)
        {
            selectedAcademic = (Academic)tableOfAcademics.SelectedItem;

            int academicToUpdateID = selectedAcademic.IdAcademic;

            NavigationService.Navigate(new DeleteAcademic(academicToUpdateID));
        }  
    }
}
