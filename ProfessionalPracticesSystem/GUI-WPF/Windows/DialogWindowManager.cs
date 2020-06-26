/*
    Date: 30/05/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace GUI_WPF.Windows
{
    public class DialogWindowManager
    {
        public static void ShowErrorWindow(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool ShowConfirmationWindow(string confirmMessage)
        {
            bool confirmation = false;

            MessageBoxResult userAnswer = MessageBox.Show(confirmMessage, "Confirmación",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (userAnswer == MessageBoxResult.Yes)
            {
                confirmation = true;
            }

            return confirmation;
        }

        public static void ShowSuccessWindow(string successMessage)
        {
            MessageBox.Show(successMessage, "Éxito",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static void ShowEmptyFieldsErrorWindow()
        {
            MessageBox.Show("Uno o varios campos están vacíos. Por favor ingresa los datos necesarios.", "Error", 
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowWrongFieldsErrorWindow()
        {
            MessageBox.Show("La información en uno o varios campos es incorrecta. Por favor verifica la información.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowEmptyListErrorWindow()
        {
            MessageBox.Show("No se encuentra ninguna entrada registrada.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowConnectionErrorWindow()
        {
            MessageBox.Show("Ocurrió un fallo al intentar conectarse a la base de datos. Intente de nuevo más tarde.",
                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
