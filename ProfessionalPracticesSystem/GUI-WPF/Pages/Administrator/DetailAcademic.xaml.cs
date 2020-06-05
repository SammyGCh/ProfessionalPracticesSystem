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
    /// Mostrar detalles para 
    /// </summary>
    public partial class DetailAcademic : Page
    {
        public DetailAcademic(Academic academic)
        {
            InitializeComponent();
            AcademicDAO detailAcademicDAO = new AcademicDAO();
            Academic detailAcademic = detailAcademicDAO.GetAcademic(academic.IdAcademic);
            this.DataContext = detailAcademic;
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
