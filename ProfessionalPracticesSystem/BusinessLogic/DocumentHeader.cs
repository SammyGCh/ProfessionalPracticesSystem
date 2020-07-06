/*
    Date: 18/06/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using iText.Kernel.Font;
using iText.Layout.Properties;
using Style = iText.Layout.Style;
using iText.IO.Image;
using Border = iText.Layout.Borders.Border;
using iText.IO.Font.Constants;

namespace BusinessLogic
{
    class DocumentHeader : IEventHandler
    {
        private int HEADERPOSITION_X = 35;
        private int HEADERPOSITION_Y = 84;
        private int HEADERWIDTH = 70;
        private int HEADERHEIGHT = 60;
        private String FACULTYNAME = "FACULTAD DE ESTADÍSTICA E INFORMÁTICA";
        private String DEGREENAME = "Licenciatura en Ingeniería de Software";
        private String formatName;

        public DocumentHeader(String formatName)
        {
            this.formatName = formatName;
        }

        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfPage page = docEvent.GetPage();

            iText.Kernel.Geom.Rectangle rootArea = new iText.Kernel.Geom.Rectangle(HEADERPOSITION_X, page.GetPageSize().GetTop() - HEADERPOSITION_Y, page.GetPageSize().GetRight() - HEADERWIDTH, HEADERHEIGHT);
            iText.Layout.Canvas canvas = new iText.Layout.Canvas(page, rootArea);
            canvas.Add(GetHeader()).Close();
        }

        public Table GetHeader()
        {
            String pathUvLogo = "..\\..\\\\image\\uvlogo.png";
            String pathLisLogo = "..\\..\\\\image\\lisLogo.jpg";
            iText.Layout.Element.Image UVLOGO = new iText.Layout.Element.Image(ImageDataFactory.Create(pathUvLogo));
            iText.Layout.Element.Image LISLOGO = new iText.Layout.Element.Image(ImageDataFactory.Create(pathLisLogo));

            float[] cellWidth = { 20f, 60f, 20f };
            Table headerTable = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            Style estiloCelda = new Style().SetBorder(Border.NO_BORDER);
            Style estiloTexto = new Style()
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(9f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            Cell celda = new Cell().Add(UVLOGO.SetAutoScale(true));
            headerTable.AddCell(celda.AddStyle(estiloCelda));

            celda = new Cell()
                .Add(new Paragraph(FACULTYNAME))
                .Add(new Paragraph(DEGREENAME))
                .Add(new Paragraph(formatName));
            headerTable.AddCell(celda.AddStyle(estiloCelda).AddStyle(estiloTexto));

            celda = new Cell().Add(LISLOGO.SetAutoScale(true));
            headerTable.AddCell(celda.AddStyle(estiloCelda));

            return headerTable;
        }
    }
}
