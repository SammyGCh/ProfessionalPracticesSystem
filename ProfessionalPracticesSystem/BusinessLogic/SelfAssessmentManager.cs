﻿/*
    Date: 03/07/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
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
using System.Linq;

namespace BusinessLogic
{
    public class SelfAssessmentManager
    {
        private Selfassessment assessment;

        public SelfAssessmentManager(Selfassessment assessment)
        {
            this.assessment = assessment;
        }

        public bool GenerateSelfAssessment(String finalPath)
        {
            bool isGenerated = false;
            try
            {
                PdfWriter writer = new PdfWriter(finalPath);
                PdfDocument pdfDocument = new PdfDocument(writer);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument, PageSize.LETTER);
                document.SetMargins(75, 35, 70, 35);

                Style styleText = new Style()
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(8f)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

                String formatName = "Formato: EVALUACIÓN DEL ALUMNO. EE Prácticas de Ingeniería de Software";
                pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new DocumentHeader(formatName));


                document.Add(GenerateInformationTableSelfassessment(assessment).SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));

                document.Add(new Paragraph("Responde a cada una de las afirmaciones presentadas, marcando con una “X” la casilla correspondiente de acuerdo a los siguientes criterios:").AddStyle(styleText).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(GenerateCriteriaTable().SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER));

                document.Add(GenerateQuestionsTableSelfassessment(assessment));

                document.Add(new Paragraph("LUGAR Y FECHA: Xalapa veracruz a " + DateTime.Now.ToString("MM/dd/yyyy")).AddStyle(styleText).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new Paragraph("__________________________________________________").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                document.Add(new Paragraph("NOMBRE Y FIRMA DEL ALUMNO").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                document.Close();
                isGenerated = true;
            }
            catch (IOException ex)
            {
                LogManager.WriteLog("Something went wrong in BussinessLogic/DocumentManagement/GenerateSelfAssessment", ex);
            }

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
    }
}
