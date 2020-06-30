/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IDocumentDAO
    {
        List<Document> GetAllDocument();
        List<Document> GetAllDocumentByPractitioner(int idPractitioner);
        List<Document> GetAllPartialReportByAcademic(int idAcademic);
        List<Document> GetAllSelfassessmentByAcademic(int idAcademic);
        Document GetDocument(int idDocument);
        bool SaveDocument(Document document);
        bool DeleteDocument(int idDocument);
        bool UpdateDocumentGrade(int idDocument, String grade);

    }
}
