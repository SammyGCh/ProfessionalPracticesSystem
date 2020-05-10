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
using MaterialDesignThemes.Wpf;
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;

namespace GUI_WPF.Pages.Administrator
{
    /// <summary>
    /// Interaction logic for AdministratorHome.xaml
    /// </summary>
    public partial class AdministratorHome : Page
    {
        private Academic selectAcademic;

        public AdministratorHome()
        {
            AcademicDAO academicDAO = new AcademicDAO();
            AcademicTypeDAO academicType = new AcademicTypeDAO();
            List<Academic> academic = academicDAO.GetAllAcademic();
            this.DataContext = academic;


        }
        private void detailButtonClick(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new DetailAcademic(selectAcademic));
        }
        private void updateButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdateAcademic(selectAcademic));
        }
        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteAcademic(selectAcademic));
        }

        private void addButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAcademic());
        }

        
    }
}
