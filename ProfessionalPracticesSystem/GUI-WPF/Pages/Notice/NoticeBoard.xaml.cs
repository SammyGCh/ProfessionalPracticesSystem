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
    /// Interaction logic for NoticeBoard.xaml
    /// </summary>
    public partial class NoticeBoard : Page
    {
        private readonly ObservableCollection<BusinessDomain.Notice> notices;
        //private readonly String currentUserName = " ";
        private readonly int currentUserID = 2;
        private BusinessDomain.Notice selectedNotice;

        public NoticeBoard()
        {
            InitializeComponent();

            //AcademicDAO academicHandler = new AcademicDAO();
            //currentUserName = WindowManager.GetCurrentUserName();
            //Academic currentAcademic = academicHandler.GetAcademicByPersonalNumber(currentUserName);
            //currentUserID = currentAcademic.IdAcademic;
            currentUserID = 2;
            NoticeDAO noticeDAO = new NoticeDAO();
            List<BusinessDomain.Notice> allNotices = noticeDAO.GetAllNotices();

            //if (allNotices.Count == 0)
            //{
            //    bool wantToCreateNotice = false;
            //    wantToCreateNotice = DialogWindowManager.ShowConfirmationWindow("No se encuentran avisos. ¿Desea crear un aviso?");

            //    if (wantToCreateNotice)
            //    {
            //        NavigationService.Navigate(new AddNewNotice(currentUserID));
            //    }
            //    else
            //    {

            //    }
            //}
            //else
            //{
                notices = new ObservableCollection<BusinessDomain.Notice>(allNotices);
                tableOfNotices.ItemsSource = notices;
           // }
        }

        private void CancelViewNotices(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DisplayNoticeData(object sender, RoutedEventArgs e)
        {
            selectedNotice = (BusinessDomain.Notice)tableOfNotices.SelectedItem;
            NavigationService.Navigate(new DisplayNotice(selectedNotice));
        }

        private void UpdateNoticeData(object sender, RoutedEventArgs e)
        {
            selectedNotice = (BusinessDomain.Notice)tableOfNotices.SelectedItem;
            NavigationService.Navigate(new UpdateNotice(selectedNotice,currentUserID));
        }

        private void AddNewNotice(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewNotice(currentUserID));
        }
    }
}
