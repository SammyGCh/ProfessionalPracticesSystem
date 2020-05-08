﻿using System;
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

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para CoordinatorHome.xaml
    /// </summary>
    public partial class CoordinatorHome : Page
    {
        public CoordinatorHome()
        {
            InitializeComponent();
        }

        private void AddProject(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProject());
        }

        private void ConsultarOrg(object sender, RoutedEventArgs e)
        { 
            NavigationService.Navigate(new VinculatedOrganizations());
        }
    }
}
