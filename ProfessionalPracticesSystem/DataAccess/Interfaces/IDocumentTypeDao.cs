/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IDocumentTypeDao
    {
        List<DocumentType> GetAllDocumentType();
        DocumentType GetDocumentType(int _idDocumentType);
        bool SaveDocumentType(DocumentType documentType);
        bool DeleteDocumentType(int _documentType);

    }
}
