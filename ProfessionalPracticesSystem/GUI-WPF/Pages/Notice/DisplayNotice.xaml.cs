/*
        Date: 02/07/2020                              
        Author:Ricardo Moguel Sanchez
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
using BusinessDomain;
using BusinessLogic;
using System.Collections.ObjectModel;
using GUI_WPF.Windows;


namespace GUI_WPF.Pages.Notice
{
    /// <summary>
    /// Interaction logic for DisplayNotice.xaml
    /// </summary>
    public partial class DisplayNotice : Page
    {
        public DisplayNotice(BusinessDomain.Notice selectedNotice)
        {
            InitializeComponent();

            this.DataContext = selectedNotice;
        }

        private void CancelViewNoticeData(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
