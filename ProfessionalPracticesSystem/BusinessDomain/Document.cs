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
        private DocumentType typeOf;
        private Practising addBy;
        private int UNDEFINED = 0;
        private String STRUNDEFINED = "";

        public Document()
        {
            idDocument = UNDEFINED;
            name = STRUNDEFINED;
            path = STRUNDEFINED;
            addBy = null;
            typeOf = null;
        }
        public int IdDocument
        {
            get { return idDocument; }
            set { idDocument = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Path
        {
            get { return path; }
            set { path = value; }
        }

        public Practising AddBy
        {
            get { return addBy; }
            set { addBy = value; }
        }

        public DocumentType TypeOf
        {
            get { return typeOf; }
            set { typeOf = value; }
        }
    }
}
