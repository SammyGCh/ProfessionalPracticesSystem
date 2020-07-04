/*
        Date: 07/02/2020                              
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
using BusinessDomain;
using BusinessLogic;
using DataAccess.Implementation;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Notice
{
    /// <summary>
    /// Interaction logic for UpdateNotice.xaml
    /// </summary>
    public partial class UpdateNotice : Page
    {
        private readonly Academic creatorAcademic = null;

        public UpdateNotice(BusinessDomain.Notice selectedNotice, int currentUserID)
        {
            InitializeComponent();

            this.DataContext = selectedNotice;

            AcademicDAO academicHandler = new AcademicDAO();
            creatorAcademic = academicHandler.GetAcademic(currentUserID);
            
        }
        

        private void CancelUpdateNotice(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar la actualización?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private BusinessDomain.Notice GetNoticeData()
        {

            BusinessDomain.Notice noticeUpdate = new BusinessDomain.Notice
            {
                Title = noticeTitle.Text,
                Body = noticeBody.Text,
                CreationDate = DateTime.Now.ToString("MM/dd/yyyy"),
                CreatedBy = creatorAcademic
            };
            return noticeUpdate;
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

        private void ConfirmUpdateNotice(object sender, RoutedEventArgs e)
        {
            if (AreFieldsEmpty())
            {
                DialogWindowManager.ShowEmptyFieldsErrorWindow();
            }
            else
            {
                bool isSaved = SaveNoticeUpdate();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow("El Aviso fue actualizado exitosamente.");

                    NavigationService.GoBack();
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }
            }
        }

        private bool SaveNoticeUpdate()
        {
            bool isNoticeSaved = false;

            BusinessDomain.Notice updatedNotice = GetNoticeData();

            ManageNotice manageNotice = new ManageNotice();

            isNoticeSaved = manageNotice.UpdateNotice(updatedNotice);

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
