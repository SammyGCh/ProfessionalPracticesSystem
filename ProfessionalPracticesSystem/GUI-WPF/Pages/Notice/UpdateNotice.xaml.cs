/*
        Date: 07/02/2020                              
        Author:Ricardo Moguel Sanchez
 */
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
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
        private readonly Academic currentAcademic;

        public UpdateNotice(BusinessDomain.Notice selectedNotice, int currentUserID)
        {
            InitializeComponent();

            this.DataContext = selectedNotice;

            AcademicDAO academicHandler = new AcademicDAO();

            currentAcademic = academicHandler.GetAcademic(currentUserID);

            

            noticeID.Text = selectedNotice.IdNotice.ToString();
            noticeTitle.Text = selectedNotice.Title;
            noticeBody.Text = selectedNotice.Body;
            noticeCreator.Text = selectedNotice.CreatedBy.ToString();
            noticeCreationDate.Text = selectedNotice.CreationDate;
            
        }
        

        private void CancelUpdateNotice(object sender, RoutedEventArgs e)
        {
            bool cancelConfirmation = DialogWindowManager.ShowConfirmationWindow(
                                      "¿Seguro que deseas cancelar la actualización?");

            if (cancelConfirmation)
            {
                NavigationService.GoBack();
            }
        }

        private BusinessDomain.Notice GetNoticeData()
        {

            BusinessDomain.Notice noticeUpdate = new BusinessDomain.Notice
            {
                IdNotice = int.Parse(noticeID.Text),
                Title = noticeTitle.Text,
                Body = noticeBody.Text,
                CreatedBy = currentAcademic
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
                currentAcademic == null
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
            else if(AreFieldsValid())
            {
                bool isSaved = SaveNoticeUpdate();

                CleanTextFields();

                if (isSaved)
                {
                    DialogWindowManager.ShowSuccessWindow(
                        "El Aviso fue actualizado exitosamente."); 
                }
                else
                {
                    DialogWindowManager.ShowConnectionErrorWindow();
                }

                
                NavigationService.Navigate(new NoticeBoard(currentAcademic.PersonalNumber));
            }
            else
            {
                DialogWindowManager.ShowWrongFieldsErrorWindow();
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

        private bool AreFieldsValid()
        {
            bool isValid = false;

            if (IsValidNoticeText(noticeTitle.Text) 
                && IsValidNoticeText(noticeBody.Text))
            {
                isValid = true;
            }

            return isValid;
        }

        private bool IsValidNoticeText(String testString)
        {
            bool isWrong = false;

            String stringToValidate = testString;
            if (ValidatorText.IsTextRight(stringToValidate))
            {
                isWrong = true;
            }

            return isWrong;
        }

        private void CleanTextFields()
        {
            noticeTitle.Clear();
            noticeBody.Clear();
        }
    }
}