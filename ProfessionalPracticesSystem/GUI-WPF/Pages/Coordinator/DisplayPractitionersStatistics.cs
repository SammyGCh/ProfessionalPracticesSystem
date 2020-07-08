/*
        Date: 02/07/2020                              
        Author:Cesar Sergio Martinez Palacios
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.ComponentModel;
using BusinessDomain;
using BusinessLogic;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Coordinator
{
    
    public partial class DisplayPractitionersStatistics : Page
    {
        readonly ICollectionView practitionerView;
        private string filterValue = " ";
        private int boxNumber = 0;
        private const int GENDER_BOX = 1;
        private const int PERIOD_BOX = 2;
        private const int STAGE_BOX = 3;
        private const int ORGANIZATION_BOX = 4;
        private const int SECTOR_BOX = 5;

        public DisplayPractitionersStatistics()
        {
            InitializeComponent();
            List<BusinessDomain.Practitioner> allPractitioners = StatisticsListsManage.GetPractitioners();
            
            if (allPractitioners.Count == 0)
            {
                DialogWindowManager.ShowEmptyListErrorWindow();
                NavigationService.GoBack();
            }
            else
            {
                practitionerView = CollectionViewSource.GetDefaultView(allPractitioners);
                tableOfPractitioners.ItemsSource = practitionerView;
                periodsBox.ItemsSource = StatisticsListsManage.GetScholarPeriods();
                stagesBox.ItemsSource = StatisticsListsManage.GetDevelopmentStages();
                organizationBox.ItemsSource = StatisticsListsManage.GetLinkedOrganizations();
                sectorsBox.ItemsSource = StatisticsListsManage.GetOrganizationSectors();
            }
        }

        public bool FilterGender(object o)
        {
            BusinessDomain.Practitioner practitioner = (o as BusinessDomain.Practitioner);

            if (practitioner != null)
            {
                if (practitioner.Gender.Contains(filterValue))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FilterOrganization(object o)
        {
            BusinessDomain.Practitioner practitioner = (o as BusinessDomain.Practitioner);

            if (practitioner.Assigned != null)
            {

                if (practitioner.Assigned.ProposedBy.Name.Contains(filterValue))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FilterSector(object o)
        {
            BusinessDomain.Practitioner practitioner = (o as BusinessDomain.Practitioner);

            if (practitioner.Assigned != null && practitioner.Assigned.ProposedBy.BelongsTo != null)
            {
                if (practitioner.Assigned.ProposedBy.BelongsTo.Name.Contains(filterValue)) 
                { 
                    return true;
                }
            }
            return false;
        }
        public bool FilterStage(object o)
        {
            BusinessDomain.Practitioner practitioner = (o as BusinessDomain.Practitioner);

            if (practitioner.Assigned != null)
            {
                if (practitioner.Assigned.BelongsTo.Name.Contains(filterValue))
                {
                    return true;
                }
            }
            return false;
        }

        public bool FilterPeriod(object o)
        {
            BusinessDomain.Practitioner practitioner = (o as BusinessDomain.Practitioner);

            if (practitioner.ScholarPeriod != null)
            {
                if (practitioner.ScholarPeriod.Name.Contains(filterValue))
                {
                    return true;
                }
            }
            return false;
        }


        private void GenderSelected(object sender, RoutedEventArgs e)
        {
            if (boxNumber != 0 && boxNumber != GENDER_BOX)
            {
                BoxClean();
            }
            if (genderBox.SelectedValue != null)
            {
                filterValue = genderBox.SelectedValue as String;
                practitionerView.Filter = FilterGender;
                boxNumber = GENDER_BOX;
            }
        }

         private void SectorSelected(object sender, RoutedEventArgs e)
        {
            if (boxNumber != 0 && boxNumber != SECTOR_BOX)
            {
                BoxClean();
            }
            if (sectorsBox.SelectedValue != null)
            {
                OrganizationSector sector = sectorsBox.SelectedValue as OrganizationSector;
                filterValue = sector.Name;
                practitionerView.Filter = FilterSector;
                boxNumber = SECTOR_BOX;
            }
        }

        private void OrganizationSelected(object sender, RoutedEventArgs e)
        {
            if (boxNumber != 0 && boxNumber != ORGANIZATION_BOX)
            {
                BoxClean();
            }
            if (organizationBox.SelectedValue != null)
            {
                LinkedOrganization organization = organizationBox.SelectedValue as LinkedOrganization;
                filterValue = organization.Name;
                practitionerView.Filter = FilterOrganization;
                boxNumber = ORGANIZATION_BOX;
            }
        }

        private void StageSelected(object sender, RoutedEventArgs e)
        {
            if (boxNumber != 0 && boxNumber != STAGE_BOX)
            {
                BoxClean();
            }
            if (stagesBox.SelectedValue != null)
            {
                DevelopmentStage stage = stagesBox.SelectedValue as DevelopmentStage;
                filterValue = stage.Name;
                practitionerView.Filter = FilterStage;
                boxNumber = STAGE_BOX;
            }
        }

        private void PeriodSelected(object sender, RoutedEventArgs e)
        {
            if (boxNumber != 0 && boxNumber != PERIOD_BOX)
            {
                BoxClean();
            }

            if (periodsBox.SelectedValue != null)
            {
                ScholarPeriod period = periodsBox.SelectedValue as ScholarPeriod;
                filterValue = period.Name;
                practitionerView.Filter = FilterPeriod;
                boxNumber = PERIOD_BOX;
            }
        }

        private void BoxClean()
        {
            switch(boxNumber)
            {
                case GENDER_BOX:
                    genderBox.SelectedItem = null;
                    break;

                case PERIOD_BOX:
                    periodsBox.SelectedItem = null;
                    break;

                case STAGE_BOX:
                    stagesBox.SelectedItem = null;
                    break;

                case ORGANIZATION_BOX:
                    organizationBox.SelectedItem = null;
                    break;

                case SECTOR_BOX:
                    sectorsBox.SelectedItem = null;
                    break;
            }
        }

        private void FilterClean(object sender, RoutedEventArgs e)
        {
            practitionerView.Filter = null;
            BoxClean();
        }

        private void GeneratePDF(object sender, RoutedEventArgs e)
        {

        }

        private void CancelView(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
