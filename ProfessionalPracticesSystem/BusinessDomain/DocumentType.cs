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
        private int UNDEFINED = 0;
        private String STRUNDEFINED = "";

        public DocumentType()
        {
            idDocumentType = UNDEFINED;
            name = STRUNDEFINED;
        }
        public int IdDocumentType
        {
            get { return idDocumentType; }
            set { idDocumentType = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
