/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class Document
    {
        private int idDocument;
        private String name;
        private String path;
        private String matricula;
        private int idDocumentType;

        public int _idDocument
        {
            get { return idDocument; }
            set { idDocument = value; }
        }

        public String _name
        {
            get { return name; }
            set { name = value; }
        }

        public String _path
        {
            get { return path; }
            set { path = value; }
        }

        public String _matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public int _idDocumentType
        {
            get { return idDocumentType; }
            set { idDocumentType = value; }
        }
    }
}
