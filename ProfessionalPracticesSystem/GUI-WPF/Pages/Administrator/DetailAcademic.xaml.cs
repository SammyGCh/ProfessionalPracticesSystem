/*
        Date: 20/06/2020                              
        Author:Ricardo Moguel Sanchez
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

namespace GUI_WPF.Pages.Administrator
{
    /// <summary>
    /// Mostrar detalles para 
    /// </summary>
    public partial class DetailAcademic : Page
    {
        private readonly Academic selectedAcademic;
        public DetailAcademic(Academic academic)
        {
            InitializeComponent();

            selectedAcademic = academic;

            this.DataContext = selectedAcademic;

            academicType.Text = selectedAcademic.BelongTo.AcademicTypeName;
        }

        private void ReturnToHome(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
