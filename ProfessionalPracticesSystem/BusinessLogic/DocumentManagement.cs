/*
    Date: 01/05/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using BusinessDomain;
using DataAccess.Implementation;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using DataAccess;
using System.Net;
using iText.Layout.Borders;
using iText.IO.Image;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace BusinessLogic
{
    public class DocumentManagement
    {
        private const String USER_CREDENTIAL = "UsuarioFTP";
        private const String PASSWORD_CREDENTIAL = "246810";


        public DocumentManagement() {

        }

        public bool AddDocument(Document newDocument, String sourcePath)
        {
            bool isAdded = false;

            if (SaveDocumentInDataBase(newDocument))
            {
                CreateDirectoryInFTPServer(newDocument);
                isAdded = AddDocumentInFTPServer(newDocument, sourcePath);
            }

            return isAdded;
        }

        private bool SaveDocumentInDataBase(Document newDocument)
        {
            bool isSaveInDataBase;

            DocumentDAO documentDAO = new DocumentDAO();
            isSaveInDataBase = documentDAO.SaveDocument(newDocument);

            return isSaveInDataBase;
        }

        private bool AddDocumentInFTPServer(Document newDocument, String sourcePath)
        {
            bool isUpload;

            WebClient myClient = new WebClient();
            myClient.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);

            try
            {
                myClient.UploadFile(newDocument.Path + newDocument.Name, sourcePath);
                isUpload = true;
            }
            catch (ArgumentNullException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/AddDocumentInFTPServer", ex);
                isUpload = false;
            }
            catch (WebException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/AddDocumentInFTPServer", ex);
                isUpload = false;
            }


            return isUpload;
        }

        private void CreateDirectoryInFTPServer(Document newDocument)
        {
            /*
             
            Se espera que en el caso de existir el directorio para el archivo suceda una excepcion de tipo "WebException"
            De esta manera si no exite se crea y en caso contrario solo se loggea 

             */
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(newDocument.Path);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/CreateDirectoryInFTPServer", ex);
            }

        }

        public bool GenerateMensualReport(MensualReport newMensualReport)
        {
            bool isSaved;
            MensualReportDAO mensualReportDAO = new MensualReportDAO();
            isSaved = mensualReportDAO.InsertMensualReport(newMensualReport);

            return isSaved;
        }

        public bool GenerateSelfAssessment(Selfassessment assessment, String finalPath)
        {
            bool isGenerated;

            SelfAssessmentManager manager = new SelfAssessmentManager(assessment);

            isGenerated = manager.GenerateSelfAssessment(finalPath);

            return isGenerated;
        }

        private Table GenerateInformationTableSelfassessment(Selfassessment assessment)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table informationTable = new Table(2).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell().Add(new Paragraph("Nombre del alumno"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Names + " " + assessment.AddBy.LastName));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Matricula"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Matricula));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Organizacion vinculada"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Assigned.ProposedBy.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Departamento"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Assigned.BelongsTo.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Responsable del proyecto"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Assigned.ResponsableName));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Nombre del proyecto"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph(assessment.AddBy.Assigned.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            return informationTable;
        }

        private Table GenerateCriteriaTable()
        {

            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table evaluationCriteriaTable = new Table(2).SetStrokeWidth(100).SetMargin(15);

            Cell criteriaCell = new Cell(1, 2).Add(new Paragraph("CRITERIOS"));
            criteriaCell.AddStyle(styleHeaderText);
            evaluationCriteriaTable.AddHeaderCell(criteriaCell);

            criteriaCell = new Cell().Add(new Paragraph("Totalmente en desacuerdo"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));
            criteriaCell = new Cell().Add(new Paragraph("1"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));

            criteriaCell = new Cell().Add(new Paragraph("En desacuerdo"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));
            criteriaCell = new Cell().Add(new Paragraph("2"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));

            criteriaCell = new Cell().Add(new Paragraph("Indeciso"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));
            criteriaCell = new Cell().Add(new Paragraph("3"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));

            criteriaCell = new Cell().Add(new Paragraph("De acuerdo"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));
            criteriaCell = new Cell().Add(new Paragraph("4"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));

            criteriaCell = new Cell().Add(new Paragraph("Totalmente de acuerdo"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));
            criteriaCell = new Cell().Add(new Paragraph("5"));
            evaluationCriteriaTable.AddCell(criteriaCell.AddStyle(styleText));

            return evaluationCriteriaTable;
        }

        private Table GenerateQuestionsTableSelfassessment(Selfassessment assessment)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleTextValue = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table questionsTable = new Table(2).UseAllAvailableWidth();

            Cell leftColumnCell = new Cell().Add(new Paragraph("AFIRMACIONES"));
            questionsTable.AddHeaderCell(leftColumnCell.AddStyle(styleHeaderText));

            Cell rightColumnCell = new Cell().Add(new Paragraph("VALOR OTORGADO"));
            questionsTable.AddHeaderCell(rightColumnCell.AddStyle(styleHeaderText));

            int finalScore = 0;
            for (int i = 0; i < assessment.Questions.Count; i++)
            {
                leftColumnCell = new Cell().Add(new Paragraph(assessment.Questions.ElementAt(i)));
                questionsTable.AddCell(leftColumnCell.AddStyle(styleText));
                rightColumnCell = new Cell().Add(new Paragraph(assessment.QuestionsValues.ElementAt(i).ToString()));
                questionsTable.AddCell(rightColumnCell.AddStyle(styleTextValue));
                finalScore += assessment.QuestionsValues.ElementAt(i);
            }

            leftColumnCell = new Cell().Add(new Paragraph("Puntuacion final"));
            questionsTable.AddCell(leftColumnCell.AddStyle(styleHeaderText));
            rightColumnCell = new Cell().Add(new Paragraph(finalScore.ToString()));
            questionsTable.AddCell(rightColumnCell.AddStyle(styleHeaderText));


            return questionsTable;
        }

        public bool GeneratePartialReport(String finalPath, PartialReport partialReport)
        {
            bool isGenerated;

            PartialReportManager create = new PartialReportManager(partialReport);
            isGenerated = create.GeneratePartialReport(finalPath);

            return isGenerated;
        }

        private Table GenerateInformationTablePartialReport(PartialReport partialReport)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

            Style styleLabelText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table informationTable = new Table(4).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell(1, 4).Add(new Paragraph("Datos Generales de la EE"));
            informationTable.AddHeaderCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph("Carrera: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph("Ingeniería de software"));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("NRC: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(/*AQUI VA LA INFORMACION DEL REPORTE*/));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Profesor: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.Instructed.Names + " " + partialReport.GeneratedBy.Instructed.LastName));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Período escolar: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.ScholarPeriod.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell(1, 4).Add(new Paragraph("Datos del Proyecto"));
            informationTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph("Alumno(s): "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(/*AQUI VA LA INFORMACION DEL REPORTE*/));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Organización vinculada: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.Assigned.ProposedBy.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Proyecto: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.Assigned.Name));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Período del reporte y horas cubiertas: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(/*AQUI VA LA INFORMACION DEL REPORTE*/));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Fecha del reporte: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy")));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Número del informe: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(/*AQUI VA LA INFORMACION DEL REPORTE*/));
            informationTable.AddCell(informationCell.AddStyle(styleText));

            return informationTable;
        }

        private Table GenerateObjectiveAndMethodologyTable(PartialReport partialReport)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));


            Table projectTable = new Table(1).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell().Add(new Paragraph("Objetivo general del proyecto"));
            projectTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.Assigned.GeneralDescription));
            projectTable.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Metodología"));
            projectTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph(partialReport.GeneratedBy.Assigned.Metology));
            projectTable.AddCell(informationCell.AddStyle(styleText));

            return projectTable;
        }

        private Table GenerateProjectActivitiesTable()
        {

            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table planActivities = new Table(10).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell(1, 10).Add(new Paragraph("Avance de actividades realizadas en relación al plan de trabajo"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 1).Add(new Paragraph("Actividades"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(2, 1).Add(new Paragraph("Tiempo"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(1, 4).Add(new Paragraph("Mes 1"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell(1, 4).Add(new Paragraph("Mes 2"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S1"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S2"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S3"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S4"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S5"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S6"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S7"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("S8"));
            planActivities.AddCell(informationCell.AddStyle(styleHeaderText));

            /*ESTA ES LA FILA DE LA TABLA*/

            informationCell = new Cell(2, 1).Add(new Paragraph("AQUI VA LA ACTIVIDAD DEL PROYECTO"));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Plan"));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Real"));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("."));
            planActivities.AddCell(informationCell.AddStyle(styleText));

            return planActivities;
        }

        private Table GeneratePractitionerAnswersTable(PartialReport partialReport)
        {
            Style styleText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .SetMaxWidth(500);

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table practitionerAnswers = new Table(1).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell().Add(new Paragraph("Resultados obtenidos al momento"));
            practitionerAnswers.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph(partialReport.PractitionerResultsAnswer));
            practitionerAnswers.AddCell(informationCell.AddStyle(styleText));

            informationCell = new Cell().Add(new Paragraph("Observaciones"));
            practitionerAnswers.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph(partialReport.PractitionerObservationsAnswer));
            practitionerAnswers.AddCell(informationCell.AddStyle(styleText));

            return practitionerAnswers;
        }

        private Table GenerateTechnicalSupportTable()
        {
            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table technicalSupportTable = new Table(1).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell().Add(new Paragraph("Sección EXCLUSIVA para llenado por parte del Responsable Técnico designado por la Organización Vinculada"));
            technicalSupportTable.AddCell(informationCell.AddStyle(styleHeaderText));

            return technicalSupportTable;
        }

        private Table GeneratePerformanceEvaluationTable()
        {

            Style styleLabelText = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table performanceEvaluationTable = new Table(2).SetWidth(500).SetMargin(10);

            Cell informationCell = new Cell().Add(new Paragraph("FORMATO DE EVALUACIÓN DE DESMPEÑO"));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleHeaderText));
            informationCell = new Cell().Add(new Paragraph("VALOR ASIGNADO"));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph("Es responsable en las actividades asignadas."));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            performanceEvaluationTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Aporta ideas para la toma de decisiones en la solución."));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            performanceEvaluationTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Se organiza para el desarrollo del trabajo."));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            performanceEvaluationTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Aplica los conocimientos teórico-prácticos en el desarrollo de sus actividades."));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            performanceEvaluationTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Realiza las actividades encomendadas correctamente."));
            performanceEvaluationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            performanceEvaluationTable.AddCell(informationCell);

            return performanceEvaluationTable;
        }

        private Table GenerateSignatureTable()
        {
            Style styleHeaderText = new Style()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY)
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(10f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Table signatureTable = new Table(2).SetWidth(500).SetMargin(25);

            Cell informationCell = new Cell(3, 1).SetHeight(210);
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().SetHeight(70);
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Vo. Bo. Nombre, Puesto y Firma del Responsable Técnico designado por la organización vinculada "));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell();
            signatureTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Nombre(s) y Firma(s) de los Estudiantes miembros del equipo (si aplica)"));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            informationCell = new Cell().Add(new Paragraph("Sello de la organización vinculada"));
            signatureTable.AddCell(informationCell.AddStyle(styleHeaderText));

            return signatureTable;
        public bool GenerateAsignmentLetter(AssignmentLetter assignmentLetter, String finalPath)
        {
            bool isGenerated = false;

            try
            {
                /*
                PdfWriter writer = new PdfWriter(finalPath);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                String para = "En atención a su solicitud expresada a la Coordinación de Prácticas " +
                    "Profesionales de la Licenciatura en Ingeniería de Software, hacemos de su conocimiento " +
                    "que el C. " + assignmentLetter.PractitionerAssigned.Names + " "+ assignmentLetter.PractitionerAssigned.LastName 
                    + " estudiante de la Licenciatura con matrícula " +
                    "S15011634, ha sido asignado al proyecto Sistema integral clínico de la clínica " +
                    "Universitaria Sexual y Reproductiva de la Universidad Veracruzana a su digno cargo a " +
                    "partir del 13 de Agosto del presente hasta cubrir 200 horas. Cabe mencionar que el " +
                    "estudiante cuenta con la formación y el perfil para las actividades a desempeñar. ";

                document.Add(new Paragraph(para).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                writer.Close();
                document.Close();
                isGenerated = true;
                */

                PdfWriter writer = new PdfWriter("C:\\Users\\sammy\\Desktop\\prueba.pdf");
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                String letras = "qué pedo banda";

                document.Add(new iText.Layout.Element.Paragraph(letras));

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

        public Practitioner GetAllInformationPractitioner(String matricula)
        {
            PractitionerDAO practitionerDAO = new PractitionerDAO();

            Practitioner practitioner = practitionerDAO.GetPractitionerByMatricula(matricula);

            return practitioner;

        }

        public bool AssingGradeToMensualReport(MensualReport mensualReport)
        {
            bool isUpdated;
            MensualReportDAO mensualReportDAO = new MensualReportDAO();

            isUpdated = mensualReportDAO.UpdateMensualReport(mensualReport);

            return isUpdated;
        }

        public bool AssingGradeToPartialReport(Document partialReport)
        {
            bool isUpdated;

            DocumentDAO documentDAO = new DocumentDAO();
            isUpdated = documentDAO.UpdateDocumentGrade(partialReport.IdDocument, partialReport.Grade);

            return isUpdated;
        }

        public bool DownloadDocument(Document currentDocument, String destiny)
        {
            bool isDownloaded = false;

            String serverRoute = currentDocument.Path + currentDocument.Name;
            String finalserverRoute = serverRoute.Replace('\\', '/');

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(finalserverRoute);
            request.Credentials = new NetworkCredential(USER_CREDENTIAL, PASSWORD_CREDENTIAL);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            try
            {

                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (Stream fileStream = File.Create(destiny))
                {
                    ftpStream.CopyTo(fileStream);
                }

                isDownloaded = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (ArgumentException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (PathTooLongException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }
            catch (InvalidOperationException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/DownloadDocument", ex);
            }

            return isDownloaded;
        }
    }
}