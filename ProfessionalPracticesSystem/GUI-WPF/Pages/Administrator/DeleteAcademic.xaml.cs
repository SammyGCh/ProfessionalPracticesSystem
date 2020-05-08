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
    /// Interaction logic for deleteAcademic.xaml
    /// </summary>
    public partial class DeleteAcademic : Page
    {
        private Academic selected;
        public DeleteAcademic(Academic academic)
            
        {
            InitializeComponent();
        }

        private void RemoveAcademic(object sender, RoutedEventArgs e)
        {
           
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
