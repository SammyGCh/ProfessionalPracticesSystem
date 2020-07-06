/*
    Date: 25/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
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
using BusinessLogic;
using Microsoft.Win32;
using BusinessDomain;
using DataAccess.Implementation;
using GUI_WPF.Windows;

namespace GUI_WPF.Pages.Practitioner
{
    /// <summary>
    /// Lógica de interacción para GenerateSelfassessment.xaml
    /// </summary>
    public partial class GenerateSelfassessment : Page
    {
        private String practitionerMatricula;

        public GenerateSelfassessment(String practitionerMatricula)
        {
            this.practitionerMatricula = practitionerMatricula;
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Seguro que deseas cancelar?");

            if (isConfirmed)
            {
                NavigationService.GoBack();
            }
        }

        private void Generate(object sender, RoutedEventArgs e)
        {
            bool isConfirmed = DialogWindowManager.ShowConfirmationWindow("¿Desea generar la autoevaluacion?");

            if (isConfirmed)
            {
                String destinyPath = PathToSave();

                if (destinyPath != "")
                {
                    DocumentManagement documentManagement = new DocumentManagement();
                    Selfassessment selfassesment = GetAssessment();

                    if (documentManagement.GenerateSelfAssessment(selfassesment, destinyPath))
                    {
                        DialogWindowManager.ShowSuccessWindow("Autoevaluacion generada exitosamente");
                    }
                    else
                    {
                        DialogWindowManager.ShowErrorWindow("Error al generar el PDF de la autoevaluacion");
                    }
                }
            }
        }

        private Selfassessment GetAssessment()
        {

            Selfassessment newAssessment = new Selfassessment()
            {
                AddBy = GetCurrentPractitioner(),
                QuestionsValues = GetQuestionsValues()
            };

            return newAssessment;
        }

        private List<int> GetQuestionsValues()
        {
            List<int> questionsValues = new List<int>();
            
            foreach (StackPanel panel in questionContainer.Children.OfType<StackPanel>())
            {
                foreach (ComboBox item in panel.Children.OfType<ComboBox>())
                {
                    questionsValues.Add(int.Parse(item.SelectionBoxItem.ToString()));
                }
            }

            return questionsValues;
        }

        private BusinessDomain.Practitioner GetCurrentPractitioner()
        {
            DocumentManagement documentManager = new DocumentManagement();
            BusinessDomain.Practitioner currentPRactitioner = documentManager.GetAllInformationPractitioner(practitionerMatricula);

            return currentPRactitioner;
        }

        private String PathToSave()
        {
            SaveFileDialog saveWindow = new SaveFileDialog();
            saveWindow.Filter = "PDF Document|*.pdf";
            saveWindow.Title = "Selecciona ruta de guardado";
            saveWindow.ShowDialog();

            return saveWindow.FileName;
        }
    }
}
