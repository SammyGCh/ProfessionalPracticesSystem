/*
    Date: 03/07/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.IO;
using BusinessDomain;
using DataAccess;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;

namespace BusinessLogic
{
    public class LetterDocumentManager
    {
        private PdfWriter writer;
        private PdfDocument pdfDocument;
        private iText.Layout.Document document;

        public bool GenerateAsignmentLetter(Letter assignmentLetter, String finalPath)
        {
            bool isGenerated = false;

            try
            {
                writer = new PdfWriter(finalPath);
                pdfDocument = new PdfDocument(writer);
                document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(30, 35, 30, 15);

                Table assigmentLetterHeader = CreateAssigmentLetterHeader();
                Table receiverContainer = CreateReceiverContainer(assignmentLetter.ProjectSelected);
                Table contentTable = CreateAssigmentLetterContent(assignmentLetter);
                Table signContent = CreateAssigmentLetterSignContent(assignmentLetter.CoordinatorName);

                document.Add(assigmentLetterHeader);
                document.Add(receiverContainer);
                document.Add(contentTable);
                document.Add(signContent);

                document.Close();
                writer.Close();
                isGenerated = true;
            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/GenerateAssigmentLetter", ex);
            }

            return isGenerated;
        }

        private Table CreateAssigmentLetterHeader()
        {
            Table header = new Table(1).SetWidth(580);
            Cell headerCell = new Cell().SetBorder(Border.NO_BORDER);

            Style letterNumberStyle = new Style()
                    .SetFontSize(10f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetMarginTop(1);

            Style datePlaceStyle = new Style()
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            string lisProfessionalLogoPath = "..\\..\\\\image\\lisProfessionalLogo.png";
            Image lisProfessionalLogo = new Image(ImageDataFactory.Create(lisProfessionalLogoPath));

            lisProfessionalLogo.SetHeight(100f);
            lisProfessionalLogo.SetWidth(120f);
            lisProfessionalLogo.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);

            string letterNumer = "No. Oficio FEI-PP/17/2019";
            Paragraph letterNumberParagraph = new Paragraph(letterNumer)
                .AddStyle(letterNumberStyle);

            string datePlace = "\nXalapa Enríquez, Veracruz a " + DateTime.Now.ToString("dd MMMM yyyy");
            Paragraph datePlaceParagraph = new Paragraph(datePlace).AddStyle(datePlaceStyle);

            headerCell.Add(lisProfessionalLogo);
            headerCell.Add(letterNumberParagraph);
            headerCell.Add(datePlaceParagraph);

            header.AddCell(headerCell);

            return header;
        }

        private Table CreateReceiverContainer(Project projectAssigned)
        {
            Table receiverContainer = new Table(1).SetWidth(580);
            Cell receiverInformation = new Cell().SetBorder(Border.NO_BORDER);

            Style responsableInformationStyle = new Style()
                    .SetFontSize(8f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetMarginLeft(90);

            string responsableInformation = projectAssigned.ResponsableName + "\n" +
                    projectAssigned.ResponsableCharge + "\n" +
                    projectAssigned.ProposedBy.Address + "\n" +
                    projectAssigned.ProposedBy.City + ", " + projectAssigned.ProposedBy.State;

            Paragraph responsableInformationParagraph = new Paragraph(responsableInformation)
                .AddStyle(responsableInformationStyle);

            receiverInformation.Add(responsableInformationParagraph);

            receiverContainer.AddCell(receiverInformation);

            return receiverContainer;
        }

        private Table CreateAssigmentLetterContent(Letter assignmentLetter)
        {
            Style contentTableStyle = new Style()
                    .SetWidth(580)
                    .SetBorder(Border.NO_BORDER);
            Style paragraphStyle = new Style()
                    .SetFontSize(9f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetMarginLeft(20)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            int cellParagraphsWidth = 800;
            int firstLineIndent = 40;

            Table contentTable = new Table(2).AddStyle(contentTableStyle);

            Cell feiInformationCell = CreateFEIInformationContainer();

            Cell assigmentInformation = new Cell().SetWidth(cellParagraphsWidth).SetBorder(Border.NO_BORDER);
            string firstParagraphText = "En atención a su solicitud expresada a la Coordinación de " +
                "Prácticas Profesionales de la Licenciatura en Ingeniería de Software, hacemos de su " +
                "conocimiento que el C. " + assignmentLetter.PractitionerSelected.Names + " " +
                assignmentLetter.PractitionerSelected.LastName + " estudiante de la Licenciatura con matrícula " +
                assignmentLetter.PractitionerSelected.Matricula + ", ha sido asignado " +
                "al proyecto " + assignmentLetter.ProjectSelected.Name + " de " + assignmentLetter.ProjectSelected.ProposedBy.Name +
                " a su digno cargo a partir del __________ del presente hasta cubrir 200 horas. " +
                "Cabe mencionar que el estudiante cuenta con la formación y el perfil para las actividades a " +
                "desempeñar.\n\n ";

            string secondParagraphText = "Anexo a este documento usted encontrará una copia del horario" +
                "de las experiencias educativas que el estudiante asignado se encuentra cursando para " +
                "que sea respetado y tomado en cuenta al momento de establecer el horario de " +
                "realización de sus prácticas profesionales. Por otra parte, le solicito de la manera " +
                "más atenta, haga llegar a la brevedad con el estudiante, el oficio de aceptación así " +
                "como el plan de trabajo detallado del estudiante, así como el horario que cubrirá. " +
                "Deberá indicar además, la forma en que se registrará la evidencia de asistencia y " +
                "número de horas cubiertas. Es importante mencionar que el estudiante deberá presentar " +
                "mensualmente un reporte de avances de sus prácticas. Este reporte de avances puede " +
                "entregarse hasta con una semana de atraso por lo que le solicito de la manera más " +
                "atenta sean elaborados y avalados (incluyendo sello si aplica) de manera oportuna " +
                "para su entrega al Académico responsable de la experiencia de Prácticas de Ingeniería " +
                "de Software. En relación a lo anterior, es importante que en el oficio de aceptación " +
                "proporcione el nombre de la persona que supervisará y avalará en su dependencia la " +
                "prestación de las Prácticas profesionales así como número telefónico, extensión " +
                "(cuando aplique) y correo electrónico. Lo anterior con el fin de contar con el canal " +
                "de comunicación que permita dar seguimiento al desempeño del estudiante.\n\n";

            string thirdParagraphText = "Le informo que las prácticas de Ingeniería de Software forman " +
                "parte de la currícula de la Licenciatura en Ingeniería de Software, por lo cual es " +
                "necesaria su evaluación y de ahí la necesidad de realizar el seguimiento " +
                "correspondiente. Es por ello que, durante el semestre el Académico a cargo de la " +
                "experiencia educativa de prácticas de Ingeniería de Software realizará al menos un " +
                "seguimiento de las actividades del estudiante por lo que será necesario mostrar " +
                "evidencias de la asistencia del estudiante, así como de sus actividades. Este " +
                "seguimiento podrá ser vía correo electrónico, teléfono o incluso mediante una visita " +
                "a sus oficinas, por lo que le solicito de la manera más atenta, proporcione las " +
                "facilidades requeridas en su caso.\n\n ";

            string fourthParagraphText = "Sin más por el momento, agradezco su atención al presente " +
                "reiterándome a sus apreciables órdenes.";

            Paragraph firstParagraph = new Paragraph(firstParagraphText)
                .AddStyle(paragraphStyle).SetFirstLineIndent(firstLineIndent);
            Paragraph secondParagraph = new Paragraph(secondParagraphText)
                .AddStyle(paragraphStyle).SetFirstLineIndent(firstLineIndent);
            Paragraph thirdParagraph = new Paragraph(thirdParagraphText)
                .AddStyle(paragraphStyle).SetFirstLineIndent(firstLineIndent);
            Paragraph fourthParagraph = new Paragraph(fourthParagraphText)
                .AddStyle(paragraphStyle).SetFirstLineIndent(firstLineIndent);

            assigmentInformation.Add(firstParagraph);
            assigmentInformation.Add(secondParagraph);
            assigmentInformation.Add(thirdParagraph);
            assigmentInformation.Add(fourthParagraph);

            contentTable.AddCell(feiInformationCell);
            contentTable.AddCell(assigmentInformation);

            return contentTable;
        }

        private Cell CreateFEIInformationContainer()
        {
            int CellFeiInformationWidth = 90;
            Style universityInformationStyle = new Style()
                    .SetFontSize(7f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);
            Style universityInformationTitleStyle = new Style()
                    .SetFontSize(7f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            Cell feiInformation = new Cell().SetWidth(CellFeiInformationWidth).SetBorder(Border.NO_BORDER);
            Paragraph feiName = new Paragraph("Facultad de Estadística e Informática")
                .AddStyle(universityInformationStyle);
            Paragraph addressTitle = new Paragraph("\n\nDirección\n").AddStyle(universityInformationTitleStyle);
            Paragraph feiAddress = new Paragraph("Av. Xalapa esq. Ávila Camacho S/N\n Col. Obrero Campesina\n" +
                "CP 91020\n Xalapa de Enríquez\n Veracruz, México")
                .AddStyle(universityInformationStyle);

            Paragraph telephoneTitle = new Paragraph("\n\nTeléfonos\n").AddStyle(universityInformationTitleStyle);
            Paragraph feiTelephoneNumbers = new Paragraph("(288) 8421700\nExts. 14155, 14250, 14108, 14106\n" +
                "Fax\n(288) 8149990")
                .AddStyle(universityInformationStyle);

            Paragraph internetInformacionTitle = new Paragraph("\n\nInternet").AddStyle(universityInformationTitleStyle);
            Paragraph internetInformacion = new Paragraph("fei@uv.mx\nhttp://www.uv.mx/fei").AddStyle(universityInformationStyle);

            feiInformation.Add(feiName);
            feiInformation.Add(addressTitle);
            feiInformation.Add(feiAddress);
            feiInformation.Add(telephoneTitle);
            feiInformation.Add(feiTelephoneNumbers);
            feiInformation.Add(internetInformacionTitle);
            feiInformation.Add(internetInformacion);

            return feiInformation;
        }

        private Table CreateAssigmentLetterSignContent(string coordinatorName)
        {
            Style signContenStyle = new Style()
                    .SetFontSize(9f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            Table signContent = new Table(1).SetWidth(580);
            Cell signInformationCell = new Cell().SetBorder(Border.NO_BORDER);

            Paragraph signInformation = new Paragraph("Atentamente\n\n_______________________________" +
                    "__________").AddStyle(signContenStyle).SetMarginTop(30);

            string coordinatorInformationText = coordinatorName + "\nCoordinador de Prácticas " +
                "Profesionales\nLicenciatura en Ingeniería de Software";
            Paragraph coordinatorInformation = new Paragraph(coordinatorInformationText)
                .AddStyle(signContenStyle);

            signInformationCell.Add(signInformation);
            signInformationCell.Add(coordinatorInformation);

            signContent.AddCell(signInformationCell);

            return signContent;
        }

        public bool GenerateAcceptanceLetter(Letter acceptanceLetter, string finalPath)
        {
            bool isGenerated = false;

            try
            {
                writer = new PdfWriter(finalPath);
                pdfDocument = new PdfDocument(writer);
                document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(40, 90, 30, 90);

                Table headerContainer = CreateAcceptanceLetterHeader();
                Table receiverInformation = CrateAcceptanceLetterReceiverContainer(acceptanceLetter.CoordinatorName);
                Table bodyContainer = CreateAcceptanceLetterBody(acceptanceLetter);
                Table signContainer = CreateAcceptanceLetterSignContainer(acceptanceLetter.ProjectSelected);

                document.Add(headerContainer);
                document.Add(receiverInformation);
                document.Add(bodyContainer);
                document.Add(signContainer);

                document.Close();
                writer.Close();
                isGenerated = true;
            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/GenerateApprovementLetter", ex);
            }

            return isGenerated;
        }

        private Table CreateAcceptanceLetterHeader()
        {
            string uvLogoPath = "..\\..\\\\image\\uvlogo2.png";
            string lisLogoPath = "..\\..\\\\image\\lislogo2.jpg";
            int imageWidth = 60;
            int imageHeight = 50;

            Style imageStyle = new Style()
                .SetWidth(imageWidth)
                .SetHeight(imageHeight);
            Style dateTimeStyle = new Style()
                    .SetFontSize(11f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            Table headerContainer = new Table(2).SetWidth(420);

            Image uvLogo = new Image(ImageDataFactory.Create(uvLogoPath))
                .AddStyle(imageStyle)
                .SetMarginLeft(0)
                .SetMarginTop(0);
            Image lisLogo = new Image(ImageDataFactory.Create(lisLogoPath))
                .AddStyle(imageStyle)
                .SetMarginLeft(0)
                .SetMarginBottom(0)
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.RIGHT);


            Cell uvLogoCell = new Cell().SetBorder(Border.NO_BORDER);
            uvLogoCell.Add(uvLogo);

            Cell lisLogoCell = new Cell().SetBorder(Border.NO_BORDER);
            lisLogoCell.Add(lisLogo);

            string currentDateTimeTex = "\n\nXalapa-Enríquez, Ver., a ________________________";
            Paragraph currentDateTime = new Paragraph(currentDateTimeTex)
                .AddStyle(dateTimeStyle);
            lisLogoCell.Add(currentDateTime);

            headerContainer.AddCell(uvLogoCell);
            headerContainer.AddCell(lisLogoCell);

            return headerContainer;
        }

        private Table CrateAcceptanceLetterReceiverContainer(string coordinatorName)
        {
            Style receiverInformationStyle = new Style()
                    .SetFontSize(11f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN));

            Table receiverContainer = new Table(1).SetWidth(400);
            Cell receiverCell = new Cell().SetBorder(Border.NO_BORDER);

            string receiverInformationText = coordinatorName.ToUpper() + "\n" +
                    "COORDINADOR DE PRÁCTICAS PROFESIONALES\n" +
                    "LICENCIATURA EN INGENIERÍA DE SOFTWARE\n" +
                    "FACULTAD DE ESTADISTICA E INFORMATICA UNIVERSIDAD VERACRUZANA\n\n" +
                    "P R  E S  E N  T E";
            Paragraph receiverInformation = new Paragraph(receiverInformationText)
                .AddStyle(receiverInformationStyle);

            receiverCell.Add(receiverInformation);

            receiverContainer.AddCell(receiverCell);

            return receiverContainer;
        }

        private Table CreateAcceptanceLetterBody(Letter acceptanceLetter)
        {
            Style letterBodyStyle = new Style()
                    .SetFontSize(12f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            int firstLineIndent = 40;

            Table bodyContainer = new Table(1).SetWidth(460);
            Cell bodyContainerCell = new Cell().SetBorder(Border.NO_BORDER);

            string letterBodyText = "Por medio de la presente le informo que el C. " + acceptanceLetter.PractitionerSelected.Names +
                acceptanceLetter.PractitionerSelected.LastName + ", alumno de la Facultad de Estadística e Informática con " +
                "matrícula " + acceptanceLetter.PractitionerSelected.Matricula + ", " +
                "ha sido aceptado para realizar sus prácticas profesionales en " + acceptanceLetter.ProjectSelected.ProposedBy.Name +
                ", teniendo como fecha de inicio _______ y aproximada de terminación _________, " +
                "en el cual cubrirá un total de 200 horas, en las que realizará actividades afines a su carrera.\n\n";
            Paragraph letterBody = new Paragraph(letterBodyText)
                .AddStyle(letterBodyStyle).SetFirstLineIndent(firstLineIndent);

            string tableTitleText = "El horario pactado para realizar el servicio social es el siguiente:\n\n";
            Paragraph tableTitle = new Paragraph(tableTitleText)
                .AddStyle(letterBodyStyle).SetFirstLineIndent(firstLineIndent);

            Table scheduleTable = CreateScheduleTable();

            string responsableDataText = "Los datos de contacto del responsable del servicio social " +
                    "son: correo electrónico: " + acceptanceLetter.ProjectSelected.ResponsableEmail + " y  teléfono: " +
                    acceptanceLetter.ProjectSelected.ResponsableTelephone + ".\n\n";
            Paragraph responsableData = new Paragraph(responsableDataText)
                .AddStyle(letterBodyStyle).SetFirstLineIndent(firstLineIndent);

            string goodbyeText = "Sin más por el momento quedo a su disposición para cualquier aclaración.\n\n";
            Paragraph goodbye = new Paragraph(goodbyeText)
                .AddStyle(letterBodyStyle).SetFirstLineIndent(firstLineIndent);

            bodyContainerCell.Add(letterBody);
            bodyContainerCell.Add(tableTitle);
            bodyContainerCell.Add(scheduleTable);
            bodyContainerCell.Add(responsableData);
            bodyContainerCell.Add(goodbye);

            bodyContainer.AddCell(bodyContainerCell);

            return bodyContainer;
        }

        private Table CreateAcceptanceLetterSignContainer(Project projectAssigned)
        {
            Style signContenStyle = new Style()
                    .SetFontSize(12f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Table signContainer = new Table(1).SetWidth(460);
            Cell signCell = new Cell().SetBorder(Border.NO_BORDER);

            Paragraph signInformation = new Paragraph("Atentamente\n\n_______________________________" +
                    "__________").AddStyle(signContenStyle).SetMarginTop(30);

            string responsableInformationText = projectAssigned.ResponsableName + "\n" +
                projectAssigned.ResponsableCharge + "\n" + projectAssigned.ProposedBy.Name;
            Paragraph responsableInformation = new Paragraph(responsableInformationText)
                .AddStyle(signContenStyle);

            signCell.Add(signInformation);
            signCell.Add(responsableInformation);

            signContainer.AddCell(signCell);

            return signContainer;
        }

        private Table CreateScheduleTable()
        {
            Style letterBodyStyle = new Style()
                .SetFontSize(12f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);

            Table scheduleTable = new Table(5).SetWidth(400);

            Cell mondayCell = new Cell();
            string dayName = "Lunes";
            mondayCell.Add(new Paragraph(dayName).AddStyle(letterBodyStyle));
            scheduleTable.AddCell(mondayCell);

            Cell tuesdayCell = new Cell();
            dayName = "Martes";
            tuesdayCell.Add(new Paragraph(dayName).AddStyle(letterBodyStyle));
            scheduleTable.AddCell(tuesdayCell);

            Cell wednesdayCell = new Cell();
            dayName = "Miércoles";
            wednesdayCell.Add(new Paragraph(dayName).AddStyle(letterBodyStyle));
            scheduleTable.AddCell(wednesdayCell);

            Cell thursdayCell = new Cell();
            dayName = "Jueves";
            thursdayCell.Add(new Paragraph(dayName).AddStyle(letterBodyStyle));
            scheduleTable.AddCell(thursdayCell);

            Cell fridayCell = new Cell();
            dayName = "Viernes";
            fridayCell.Add(new Paragraph(dayName).AddStyle(letterBodyStyle));
            scheduleTable.AddCell(fridayCell);

            return scheduleTable;
        }
    }
}
