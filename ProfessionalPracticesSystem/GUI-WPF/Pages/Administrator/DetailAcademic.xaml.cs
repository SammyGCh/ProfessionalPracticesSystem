/*
        Date: 20/06/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BusinessDomain;

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
