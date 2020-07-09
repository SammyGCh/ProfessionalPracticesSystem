/*
        Date: 02/07/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataAccess.Implementation;
using System.Collections.ObjectModel;
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Pages.Professor;
using GUI_WPF.Windows;
using BusinessDomain;

namespace GUI_WPF.Pages.Notice
{
    /// <summary>
    /// Interaction logic for NoticeBoard.xaml
    /// </summary>
    public partial class NoticeBoard : Page
    {
        private BusinessDomain.Notice selectedNotice;
        private readonly ObservableCollection<BusinessDomain.Notice> notices;
        private readonly String currentPersonalNumber;
        private readonly int currentUserID;
        private const int COORDINATOR_TYPE_ID = 1;
        private const int PROFESSOR_TYPE_ID = 2;
        

        public NoticeBoard(string userPersonalNumber)
        {
            InitializeComponent();

            currentPersonalNumber = userPersonalNumber;

            AcademicDAO academicHandler = new AcademicDAO();
            Academic currentAcademic = academicHandler.GetAcademicByPersonalNumber(currentPersonalNumber);
            currentUserID = currentAcademic.IdAcademic;

            NoticeDAO noticeDAO = new NoticeDAO();
            List<BusinessDomain.Notice> allNotices = noticeDAO.GetAllNotices();

            
            if (allNotices.Count == 0)
            {
                DialogWindowManager.ShowErrorWindow("No se han registrados avisos.");
            }

            notices = new ObservableCollection<BusinessDomain.Notice>(allNotices);
            tableOfNotices.ItemsSource = notices;
        }

        private void CancelViewNotices(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(WindowManager.GetCurrentUserHomePage());
        }

        private void DisplayNoticeData(object sender, RoutedEventArgs e)
        {
            selectedNotice = (BusinessDomain.Notice)tableOfNotices.SelectedItem;

            NavigationService.Navigate(new DisplayNotice(selectedNotice));
        }

        private void UpdateNoticeData(object sender, RoutedEventArgs e)
        {
            selectedNotice = (BusinessDomain.Notice)tableOfNotices.SelectedItem;

            if(currentUserID == selectedNotice.CreatedBy.IdAcademic)
            {
                NavigationService.Navigate(new UpdateNotice(selectedNotice, currentUserID));
            }
            else
            {
                DialogWindowManager.ShowErrorWindow("Error. No eres el creador del aviso no lo puedes modificar.");
            }
        }

        private void AddNewNotice(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddNewNotice(currentUserID));
        }
    }
}
