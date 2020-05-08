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
        private Practitioner addBy;
        private String grade;
        private String observations;

        public Document(){}
        public int IdDocument
        {
            get => idDocument;
            set => idDocument = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public String Path
        {
            get => path;
            set => path = value;
        }

        public Practitioner AddBy
        {
            get => addBy;
            set => addBy = value;
        }

        public DocumentType TypeOf
        {
            get => typeOf;
            set => typeOf = value;
        }

        public String Grade
        {
            get => grade;
            set => grade = value;
        }

        public String Observations
        {
            get => observations;
            set => observations = value;
        }
    }
}
