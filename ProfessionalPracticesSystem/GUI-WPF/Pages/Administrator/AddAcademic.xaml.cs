/*
  
 * */
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
    /// Register a new Academic to the database
    /// </summary>
    public partial class AddAcademic : Page
    {
        Academic academic;
        String academicType;
        AcademicDAO academicDAO;
        AcademicTypeDAO academicTypeDAO;
        ManageAcademic academicManager;
        int idAcademicType;
        

        public AddAcademic()        
        {
            this.academic = new Academic();
            this.academicDAO = new AcademicDAO();
            this.academicTypeDAO = new AcademicTypeDAO();
            academicManager = new ManageAcademic();
            List<AcademicType> allAcademicTypes = academicTypeDAO.GetAllAcademicTypes();
            academicTypes.ItemsSource = allAcademicTypes;
            InitializeComponent();
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddNewAcademic(object sender, RoutedEventArgs e)
        {
            List<Academic> allAcademics = academicDAO.GetAllAcademic();
            bool isSaved = false;

            Academic newAcademic = new Academic()
            {
                IdAcademic = allAcademics.Count(),
                Names = academicNames.ToString(),
                LastName = academicLastName.ToString(),
                PersonalNumber = academicPersonalNumber.ToString(),
                Password = academicPassword.ToString(),
                Cubicle = academicCubicle.ToString(),
                Gender = academicGender.ToString(),
                Shift = academicShift.ToString(),
                BelongTo = academicTypeDAO.GetAcademicTypeById(academicTypes.SelectedIndex)

            };
            isSaved = academicManager.AddAcademic(newAcademic);
            
            if (!isSaved )
            {
                MessageBoxResult userResponse = System.Windows.MessageBox.Show("No se pudo salvar el academico.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                if (userResponse == MessageBoxResult.OK)
                {
                    NavigationService.GoBack();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Se guardo el Academico exitosamente.", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
