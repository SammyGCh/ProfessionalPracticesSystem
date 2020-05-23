/*
    Date: 01/05/2020
    Author(s): Sammy Guadarrama Chavez
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
using GUI_WPF.Pages.Coordinator;
using GUI_WPF.Pages.Practitioner;

namespace GUI_WPF.Windows
{
    /// <summary>
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            homeFrame.Content = new GenerateSelfassessment();
        }

        public Home(Page homePage)
        {
            InitializeComponent();
            homeFrame.Content = homePage;
        }
    }
}
