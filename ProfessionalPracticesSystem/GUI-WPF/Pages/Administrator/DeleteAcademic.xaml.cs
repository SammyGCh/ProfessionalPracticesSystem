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

        public DeleteAcademic(int academicID)
        {
            InitializeComponent();

            selectedAcademicID = academicID;
        }

        private void EliminateAcademic(object sender, RoutedEventArgs e)
        {
            bool isAcademicDeleted = false;

            ManageAcademic academicManager = new ManageAcademic();
            isAcademicDeleted = academicManager.DeleteAcademic(selectedAcademicID);

            if (isAcademicDeleted)
            {
                DialogWindowManager.ShowSuccessWindow("Académico eliminado exitosamente");
                NavigationService.Navigate(new AdministratorHome());
            }
            else
            {
                DialogWindowManager.ShowErrorWindow(
                "Ocurrió un error al intentar eliminar el académico. Intente más tarde.");
            }
        }

        private void CancelDelete(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}