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
using BusinessLogic;

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para GenerateSelfassessment.xaml
    /// </summary>
    public partial class GenerateSelfassessment : Page
    {
        private DocumentManagement documentManager;
        public GenerateSelfassessment()
        {
            documentManager = new DocumentManagement();
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Seguro que deseas cancelar?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
