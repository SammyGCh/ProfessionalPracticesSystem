/*
        Date: 07/05/2020                              
        Author:Cesar Sergio Martinez Palacios
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

namespace GUI_WPF.Pages.Coordinator
{
    public partial class ProjectsConsult : Page
    {
        public ProjectsConsult()
        {
            InitializeComponent();
            ProjectDAO project = new ProjectDAO();
            List<Project> allProjects = project.GetAllProjects();
            tableProjects.ItemsSource = allProjects;

        }

        private void backButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void GoProjectDetails(object sender, RoutedEventArgs e)
        {
            Project projectSelected = (Project)tableProjects.SelectedItem;

            NavigationService.Navigate(new ProjectDetails(projectSelected));
        }
    }
}
