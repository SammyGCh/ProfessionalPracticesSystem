/*
    Date: 24/06/2020
    Author(s): Sammy Guadarrama Chávez
 */

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

        private static void GetHomeFrame()
        {
            var homeWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(
                    window => window is Home
                ) as Home;

            homeFrame = homeWindow.homeFrame;
        }

        public static RequestProject GetRequestProjectPage()
        {
            RequestProject requestProjectPage;

            GetHomeFrame();

            requestProjectPage = homeFrame.Content as RequestProject;

            return requestProjectPage;
        }

        public static Requests GetRequestsPage()
        {
            Requests requestsPage;

            GetHomeFrame();

            requestsPage = homeFrame.Content as Requests;

            return requestsPage;
        }
    }
}
