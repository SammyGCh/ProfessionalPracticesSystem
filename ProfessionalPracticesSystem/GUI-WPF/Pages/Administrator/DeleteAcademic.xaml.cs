/*
        Date: 15/06/2020                              
        Author:Ricardo Moguel Sanchez
 */
using BusinessLogic;
using GUI_WPF.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GUI_WPF.Pages.Administrator
{
    public partial class DeleteAcademic : Page
    {
        private int selectedAcademicID;
        private ManageAcademic academicManager;
        private const String SUCCESS_MESSAGE = "Académico eliminado exitosamente";
        private const String ERROR_MESSAGE = "Ocurrió un error al intentar eliminar el académico. Intente más tarde.";

        public DeleteAcademic(int academicID)
        {
            InitializeComponent();

            selectedAcademicID = academicID;
            academicManager = new ManageAcademic();
        }

        private void EliminateAcademic(object sender, RoutedEventArgs e)
        {
            bool isAcademicDeleted = false;

            isAcademicDeleted = academicManager.DeleteAcademic(selectedAcademicID);

            if (isAcademicDeleted)
            {

                DialogWindowManager.ShowSuccessWindow(SUCCESS_MESSAGE);
            }
            else
            {
                DialogWindowManager.ShowErrorWindow(ERROR_MESSAGE);
            }
        }

        private void CancelDelete(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}