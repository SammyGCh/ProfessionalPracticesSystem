/*
    Date: 13/05/2020
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
using BusinessDomain;
using DataAccess.Implementation;

namespace GUI_WPF.UserControls.Project
{
    /// <summary>
    /// Lógica de interacción para DataProjectControl.xaml
    /// </summary>
    public partial class DataProjectControl : UserControl
    {
        public DataProjectControl()
        {
            InitializeComponent();
            this.DataContext = this;

            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> allDevelopmentStages = developmentStageDao.GetAllDevelopmentStages();

            developmentStages.ItemsSource = allDevelopmentStages;
        }

        public bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                projectData.Children.OfType<StackPanel>().Any(
                    projectSections => projectSections.Children.OfType<TextBox>().Any(
                        projectFields => String.IsNullOrWhiteSpace(projectFields.Text)
                    )
                )
                ||
                developmentStages.SelectedItem == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        public void ClearFields()
        {
            projectName.Clear();
            projectDescription.Clear();
            projectGeneralGoals.Clear();
            inmediateGoals.Clear();
            mediateGoals.Clear();
            metology.Clear();
            neededResources.Clear();
            responsabilities.Clear();
            duration.Clear();
            directUserNumber.Clear();
            indirectUserNumber.Clear();
            practitionerNumber.Clear();
            developmentStages.SelectedItem = null;
        }
    }
}
