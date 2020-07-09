/*
        Date: 02/07/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
