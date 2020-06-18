using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI_WPF.Pages
{
    public static class PageMannagerNavigation
    {
        public static void NavigateTo(this Page currentPage, Page newPage)
        {
            Frame pageFrame = null;
            DependencyObject currentPageParent = VisualTreeHelper.GetParent(currentPage);

            while (currentPageParent != null && pageFrame == null)
            {
                pageFrame = currentPageParent as Frame;
                currentPageParent = VisualTreeHelper.GetParent(currentPageParent);
            }

            if (pageFrame != null)
            {
                pageFrame.Navigate(newPage);
            }
        }
    }
}
