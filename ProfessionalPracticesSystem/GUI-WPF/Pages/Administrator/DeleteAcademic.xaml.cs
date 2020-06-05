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
    /// Interaction logic for deleteAcademic.xaml
    /// </summary>
    public partial class DeleteAcademic : Page
    {
        int idSelectedAcademic;
        Academic selectedAcademic;
        ManageAcademic academicManager;
        bool isActionPerformed;

        public DeleteAcademic(Academic academic)
            
        {
            academicManager = new ManageAcademic();
            selectedAcademic = academic;
            InitializeComponent();
        }

        private void RemoveAcademic(object sender, RoutedEventArgs e)
        {
            isActionPerformed = academicManager.EliminateAcademic(selectedAcademic.IdAcademic);
            if (!isActionPerformed)
            {
                MessageBoxResult userResponse = System.Windows.MessageBox.Show("No se pudo eliminar el academico.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                if (userResponse == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Se elimino el Academico exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
