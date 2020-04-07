/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IDocumentDao
    {
        List<Document> getAllDocument();
        List<Document> getDocumentByPractising(Practising practising);
        List<Document> getDocumentByType(DocumentType documentType);
        Document getDocument(Document document);
        bool updateDocument(Document document);
        bool saveDocument(Document document);
        bool deleteDocument(Document document);

    }
}
