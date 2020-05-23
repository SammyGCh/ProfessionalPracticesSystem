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
    /// Actualizar los atributos de un Academico registrado
    /// </summary>
    public partial class UpdateAcademic : Page
    {
        ManageAcademic academicManager;
        Academic selectedAcademic;
        AcademicDAO academicDAO;
        AcademicTypeDAO academicTypeDAO;
        public UpdateAcademic(Academic academic)
        {
            InitializeComponent();
            this.academicManager = new ManageAcademic();
            this.selectedAcademic = academic;
            this.academicDAO = new AcademicDAO();
            this.academicTypeDAO = new AcademicTypeDAO();
            List<AcademicType> allAcademicTypes = academicTypeDAO.GetAllAcademicTypes();
            academicTypes.ItemsSource = allAcademicTypes;
            AcademicDAO detailAcademicDAO = new AcademicDAO();
            Academic detailAcademic = detailAcademicDAO.GetAcademic(academic.IdAcademic);
            this.DataContext = detailAcademic;
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChangeAcademic(object sender, RoutedEventArgs e)
        {
            bool isChanged;
            Academic changeAcademic = new Academic()
            {
                IdAcademic = selectedAcademic.IdAcademic,
                Names = academicNames.ToString(),
                LastName = academicLastName.ToString(),
                PersonalNumber = academicPersonalNumber.ToString(),
                Password = academicPassword.ToString(),
                Cubicle = academicCubicle.ToString(),
                Gender = academicGender.ToString(),
                Shift = academicShift.ToString(),
                BelongTo = academicTypeDAO.GetAcademicTypeById(academicTypes.SelectedIndex)

            };
            isChanged = academicManager.UpdateAcademic(changeAcademic);

            if (!isChanged)
            {
                MessageBoxResult userResponse = System.Windows.MessageBox.Show("No se pudo cambiar el academico.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                if (userResponse == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Se guardo el cambio de datos para el Academico exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void IsPersonalNumber(object sender, TextCompositionEventArgs e)
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
