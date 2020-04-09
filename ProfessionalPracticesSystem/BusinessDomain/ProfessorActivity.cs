/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class ProfessorActivity
    {
        private int idProfessorActivity;
        private String description;
        private String name;
        private int valueActivity;
        private DateTime? performanceDate;
        private Academic generatedBy;
        private int UNDEFINED = 0;
        private String STRUNDEFINED = "";

        public ProfessorActivity()
        {
            idProfessorActivity = UNDEFINED;
            description = STRUNDEFINED;
            name = STRUNDEFINED;
            valueActivity = UNDEFINED;
            performanceDate = null;
            generatedBy = null;
        }
        public int IdProfessorActivity
        {
            get { return idProfessorActivity; }
            set { idProfessorActivity = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ValueActivity
        {
            get { return valueActivity; }
            set { valueActivity = value; }
        }

        public DateTime? PerformanceDate
        {
            get { return performanceDate; }
            set { performanceDate = value; }
        }

        public Academic GeneratedBy
        {
            get { return generatedBy; }
            set { generatedBy = value; }
        }
    }
}
