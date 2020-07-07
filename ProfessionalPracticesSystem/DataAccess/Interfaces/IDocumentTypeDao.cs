﻿/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IDocumentTypeDAO
    {
        DocumentType GetDocumentType(int _idDocumentType);
    }
}
