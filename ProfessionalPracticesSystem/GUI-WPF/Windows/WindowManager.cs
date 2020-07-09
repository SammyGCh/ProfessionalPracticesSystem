/*
    Date: 24/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GUI_WPF.Pages.Practitioner;
using GUI_WPF.Pages.Coordinator;

namespace GUI_WPF.Windows
{
    public static class WindowManager
    {
        private static Frame homeFrame;
        private static Home homeWindow;

        private static void GetHomeWindow()
        {
            var homeWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(
                    window => window is Home
                ) as Home;

            WindowManager.homeWindow = homeWindow;
        }

        public static Frame GetHomeFrame()
        {
            GetHomeWindow();
            Frame homeFrame = homeWindow.homeFrame;
            return homeFrame;
        }

        public static RequestProject GetRequestProjectPage()
        {
            RequestProject requestProjectPage;

            homeFrame = GetHomeFrame();

            requestProjectPage = homeFrame.Content as RequestProject;

            return requestProjectPage;
        }

        public static Requests GetRequestsPage()
        {
            Requests requestsPage;

            homeFrame = GetHomeFrame();

            requestsPage = homeFrame.Content as Requests;

            return requestsPage;
        }

        public static String GetCurrentUserName()
        {
            String currenUserName;
            GetHomeWindow();

            currenUserName = homeWindow.userName.Text;

            return currenUserName;
        }

        public static Page GetCurrentUserHomePage()
        {
            GetHomeWindow();

            return homeWindow.GetUserHomePage();
        }
    }
}
