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
        private DateTime performanceDate;
        private int idAcademic;
        private String personalNumber;

        public int _idProfessorActivity
        {
            get { return idProfessorActivity; }
            set { idProfessorActivity = value; }
        }

        public String _description
        {
            get { return description; }
            set { description = value; }
        }

        public String _name
        {
            get { return name; }
            set { name = value; }
        }

        public int _valueActivity
        {
            get { return valueActivity; }
            set { valueActivity = value; }
        }

        public DateTime _performanceDate
        {
            get { return performanceDate; }
            set { performanceDate = value; }
        }

        public int _idAcademic
        {
            get { return idAcademic; }
            set { idAcademic = value; }
        }

        public String _personalNumber
        {
            get { return personalNumber; }
            set { personalNumber = value; }
        }
    }
}
