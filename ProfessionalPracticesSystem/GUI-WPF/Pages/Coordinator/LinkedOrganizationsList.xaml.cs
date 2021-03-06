﻿/*
        Date: 07/05/2020                              
        Author:Cesar Sergio Martinez Palacios
 */

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    /// <summary>
    /// Interaction logic for LinkedOrganizationsList.xaml
    /// </summary>
    
    public partial class LinkedOrganizationsList : Page
    {
        readonly ICollectionView organizationsView;
        
        public LinkedOrganizationsList()
        {
            InitializeComponent();
            List<LinkedOrganization> allLinkedOrganizations = StatisticsListsManage.GetLinkedOrganizations();

            if (allLinkedOrganizations.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
                NavigationService.GoBack();
            }
            else
            {
                organizationsView = CollectionViewSource.GetDefaultView(allLinkedOrganizations);
                tableLinkedOrganizations.ItemsSource = organizationsView;
                
                
            }
        }
        public bool FilterOrganizationName(object o)
        {
            LinkedOrganization organization = (o as LinkedOrganization);

            if (organization != null)
            {

                if (organization.Name.Contains(searchText.Text))
                    return true;
            }
            return false;
        }

        private void SearchName(object sender, RoutedEventArgs e)
        {
            organizationsView.Filter = FilterOrganizationName;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CleanFilter(object sender, RoutedEventArgs e)
        {
            searchText.Text = null;
            organizationsView.Filter = null;
        }

        private void ClickDetailsOv(object sender, RoutedEventArgs e)
        {
            DataGrid organizationsDataGrid = tableLinkedOrganizations;
            LinkedOrganization selectedOrganization = (LinkedOrganization)organizationsDataGrid.SelectedItem;
            NavigationService.Navigate(new DisplayLinkedOrganization(selectedOrganization));

        }

        private void ClickUpdateOv(object sender, RoutedEventArgs e)
        {
            DataGrid organizationsDataGrid = tableLinkedOrganizations;
            LinkedOrganization selectedOrganization = (LinkedOrganization)organizationsDataGrid.SelectedItem;
            NavigationService.Navigate(new UpdateOrganization(selectedOrganization));
        }
    }
}
