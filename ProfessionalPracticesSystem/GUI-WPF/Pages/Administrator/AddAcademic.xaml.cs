/*
  
 * */
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
    /// Register a new Academic to the database
    /// </summary>
    public partial class AddAcademic : Page
    {
        public AddAcademic()        
        {
            InitializeComponent();
            AcademicDAO academicDao = new AcademicDAO();
            AcademicTypeDAO academicTypeDAO = new AcademicTypeDAO();
            List<AcademicType> allAcademicTypes = academicTypeDAO.GetAllAcademicTypes();
            academicTypes.ItemsSource = allAcademicTypes;
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddNewAcademic(object sender, RoutedEventArgs e)
        {
            
        }

        private void IsPersonalNumber(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsTelephoneNumber(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
