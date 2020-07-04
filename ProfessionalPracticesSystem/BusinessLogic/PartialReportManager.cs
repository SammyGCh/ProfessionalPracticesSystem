using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.Kernel.Events;
using iText.Layout.Element;
using iText.Kernel.Colors;
using Style = iText.Layout.Style;
using iText.IO.Font.Constants;
using BusinessDomain;
using DataAccess;

namespace BusinessLogic
{
    public class PartialReportManager
    {
        private PartialReport newReport;

        public PartialReportManager(PartialReport partialReport)
        {
            this.newReport = partialReport;
        }

        public bool GeneratePartialReport(String finalPath)
        {
            bool isGenerated = false;

            try
            {
                PdfWriter writer;
                writer = new PdfWriter(finalPath);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                String formatName = "Formato: INFORME PARCIAL EE Prácticas de Ingeniería Software";
                pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new DocumentHeader(formatName));

                document.Add(GenerateInformationTablePartialReport(newReport).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GenerateObjectiveAndMethodologyTable(newReport).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GenerateProjectActivitiesTable(newReport).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(new AreaBreak());
                document.Add(GeneratePractitionerAnswersTable(newReport).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(new AreaBreak());
                document.Add(GenerateTechnicalSupportTable().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GenerateCriteriaTable().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GeneratePerformanceEvaluationTable().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));
                document.Add(GenerateSignatureTable().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));

                document.Close();
                isGenerated = true;
            }
            catch (IOException ex)
            {

                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/GenerateSelfAssessment", ex);
            }

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
            informationCell = new Cell();
            informationTable.AddCell(informationCell);

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
            informationCell = new Cell();
            informationTable.AddCell(informationCell);
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
            informationCell = new Cell();
            informationTable.AddCell(informationCell);

            informationCell = new Cell().Add(new Paragraph("Fecha del reporte: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell().Add(new Paragraph(DateTime.Now.ToString("MM/dd/yyyy")));
            informationTable.AddCell(informationCell.AddStyle(styleText));
            informationCell = new Cell().Add(new Paragraph("Número del informe: "));
            informationTable.AddCell(informationCell.AddStyle(styleLabelText));
            informationCell = new Cell();
            informationTable.AddCell(informationCell);

            return informationTable;
        }

        private Table GenerateObjectiveAndMethodologyTable(PartialReport partialReport)
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

        private Table GenerateProjectActivitiesTable(PartialReport partialReport)
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

            List<ProjectActivity> activityList = partialReport.GeneratedBy.Assigned.ProjectActivities;
            int NUMBER_OF_COLUMNS = 8;

            foreach (ProjectActivity activity in activityList)
            {
                informationCell = new Cell(2, 1).Add(new Paragraph(activity.Name));
                planActivities.AddCell(informationCell.AddStyle(styleText));

                informationCell = new Cell().Add(new Paragraph("Plan"));
                planActivities.AddCell(informationCell.AddStyle(styleText));

                for (int i = 0; i < NUMBER_OF_COLUMNS; i++)
                {
                    informationCell = new Cell().Add(new Paragraph(" "));
                    planActivities.AddCell(informationCell.AddStyle(styleText));
                }

                informationCell = new Cell().Add(new Paragraph("Real"));
                planActivities.AddCell(informationCell.AddStyle(styleText));


                for (int i = 0; i < NUMBER_OF_COLUMNS; i++)
                {
                    informationCell = new Cell().Add(new Paragraph(" "));
                    planActivities.AddCell(informationCell.AddStyle(styleText));
                }
            }



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
        }
    }
}
