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
        List<DocumentType> getAllDocumentType();
        bool saveDocumentType(DocumentType documentType);
        bool deleteDocumentType(DocumentType documentType);

    }
}
