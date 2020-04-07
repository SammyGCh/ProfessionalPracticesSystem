/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class Academic
    {
        private int idAcademic;
        private String personalNumber;
        private String names;
        private String cubicle;
        private String lastName;
        private String gender;
        private String password;
        private int idAcademicType;
        private String shift;

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

        public String _names
        {
            get { return names; }
            set { names = value; }
        }

        public String _cubicle
        {
            get { return cubicle; }
            set { cubicle = value; }
        }

        public String _lastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String _gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public String _password
        {
            get { return password; }
            set { password = value; }
        }

        public int _idAcademicType
        {
            get { return idAcademicType; }
            set { idAcademicType = value; }
        }

        public String _shift
        {
            get { return shift; }
            set { shift = value; }
        }
    }
}
