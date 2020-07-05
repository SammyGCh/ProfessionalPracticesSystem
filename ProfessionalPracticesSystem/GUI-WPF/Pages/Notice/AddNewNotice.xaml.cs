/*
        Date: 02/07/2020                              
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
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Notice
{
    /// <summary>
    /// Interaction logic for AddNewNotice.xaml
    /// </summary>
    public partial class AddNewNotice : Page
    {
        private readonly Academic creatorAcademic;

        public AddNewNotice(int currentUserID)
        {
            InitializeComponent();

            AcademicDAO academicHandler = new AcademicDAO();
            creatorAcademic = academicHandler.GetAcademic(currentUserID);
            creator.Text = creatorAcademic.ToString();
        }

        private void CancelAddNotice(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar el registro?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private BusinessDomain.Notice GetNoticeData()
        {

            BusinessDomain.Notice newNotice = new BusinessDomain.Notice
            {
                Title = noticeTitle.Text,
                Body = noticeBody.Text,
                CreationDate = DateTime.Now.ToString("MM/dd/yyyy"),
                CreatedBy = creatorAcademic
            };
            return newNotice;
        }

        private bool AreFieldsEmpty()
        {
            bool areEmpty = false;

            if (
                noticeData.Children.OfType<StackPanel>().Any(
                    noticeSections => noticeSections.Children.OfType<TextBox>().Any(
                        noticeFields => String.IsNullOrWhiteSpace(noticeFields.Text)
                    )
                )
                ||
                creatorAcademic == null
            )
            {
                areEmpty = true;
            }

            return areEmpty;
        }

        private void ConfirmAddNotice(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else
            {
                bool isSaved = SaveNotice();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow("El Aviso fue registrado exitosamente.");

                    NavigationService.GoBack();
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }
            }
        }

        private bool SaveNotice()
        {
            bool isNoticeSaved = false;

            BusinessDomain.Notice newNotice = GetNoticeData();

            ManageNotice manageNotice = new ManageNotice();

            isNoticeSaved = manageNotice.AddNotice(newNotice);

            return isNoticeSaved;

        }

        private void IsText(object sender, TextCompositionEventArgs e)
        {
            if (!ValidatorText.IsTextRight(e.Text))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }

        private void CleanTextFields()
        {
            noticeTitle.Clear();
            noticeBody.Clear();
        }
    }
}
