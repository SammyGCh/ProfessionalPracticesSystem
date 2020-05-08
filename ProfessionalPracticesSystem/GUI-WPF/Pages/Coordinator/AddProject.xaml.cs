/*
    Date: 04/05/2020
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
using MaterialDesignThemes.Wpf;
using DataAccess.Implementation;
using BusinessDomain;
using BusinessLogic;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Lógica de interacción para AddProject.xaml
    /// </summary>
    public partial class AddProject : Page
    {
        private List<string> monthsList;
        public AddProject()
        {
            InitializeComponent();

            monthsList = new List<string>();
            monthsList.Add("Enero");
            monthsList.Add("Febrero");
            monthsList.Add("Marzo");
            monthsList.Add("Abril");
            monthsList.Add("Mayo");
            monthsList.Add("Junio");
            monthsList.Add("Julio");
            monthsList.Add("Agosto");
            monthsList.Add("Septiembre");
            monthsList.Add("Octubre");
            monthsList.Add("Noviembre");
            monthsList.Add("Diciembre");

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> allLinkedOrganizations = linkedOrganizationDao.GetAllLinkedOrganizations();

            months.ItemsSource = monthsList;
            linkedOrganizations.ItemsSource = allLinkedOrganizations;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddNewActivity(object sender, RoutedEventArgs e)
        {
            ListBoxItem newItem = new ListBoxItem();
            StackPanel itemContainer = new StackPanel();
            itemContainer.Orientation = Orientation.Horizontal;
            TextBox projectActivityName = new TextBox
            {
                Width = 220,
                Height = 25,
                TextWrapping = TextWrapping.Wrap
            };

            ComboBox projectActivityMonth = new ComboBox
            {
                Width = 140,
                Margin = new Thickness(20,0,0,0),
                ItemsSource = monthsList
            };

            StackPanel buttonContent = new StackPanel();
            PackIcon deleteIcon = new PackIcon
            {
                Kind = PackIconKind.Delete
            };

            buttonContent.Children.Add(deleteIcon);

            Button deleteProjectActivity = new Button
            {
                Width = 50,
                Height = 25,
                Margin = new Thickness(35,0,0,0),
                Content = buttonContent,
            };

            deleteProjectActivity.Click += DeleteProjectActivity;

            itemContainer.Children.Add(projectActivityName);
            itemContainer.Children.Add(projectActivityMonth);
            itemContainer.Children.Add(deleteProjectActivity);
            newItem.Content = itemContainer;

            projectActivitiesListBox.Items.Add(newItem);
        }

        private void DeleteProjectActivity(object sender, RoutedEventArgs e)
        {
            if(projectActivitiesListBox.SelectedItems.Count > 0)
            {
                projectActivitiesListBox.Items.RemoveAt(projectActivitiesListBox.SelectedIndex);
            }
        }

        private void AddNewProject(object sender, RoutedEventArgs e)
        {
            //responsabilities.Text = ((((sender as ListBox).SelectedItem as ListBoxItem).Content as StackPanel).Children[0] as TextBox).Text;
            
        }

        private void IsTelephoneNumber(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsTelephoneNumber(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
