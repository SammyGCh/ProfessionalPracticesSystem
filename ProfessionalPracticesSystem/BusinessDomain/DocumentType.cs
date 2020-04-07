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

        public int _idDocumentType
        {
            get { return idDocumentType; }
            set { idDocumentType = value; }
        }

        public String _name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
