/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class DocumentType
    {
        private int idDocumentType;
        private String name;

        public DocumentType(){}
        public int IdDocumentType
        {
            get => idDocumentType;
            set => idDocumentType = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }
    }
}
