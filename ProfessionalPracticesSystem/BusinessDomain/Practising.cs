/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class Practising
    {
        private String matricula;
        private String password;
        private float grade;
        private bool speakIndigenousLanguage;
        private String gender;
        private String names;
        private String lastName;
        private int idIndigenousLanguage;
        private int idProject;
        private int idAcademic;
        private String personalNumber;

        public String _matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        public String _password
        {
            get { return password; }
            set { password = value; }
        }

        public float _grade
        {
            get { return grade; }
            set { grade = value; }
        }

        public bool _speakIndigenousLanguage
        {
            get { return speakIndigenousLanguage; }
            set { speakIndigenousLanguage = value; }
        }

        public String _gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public String _names
        {
            get { return names; }
            set { names = value; }
        }

        public String _lastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int _idIndigenousLanguage
        {
            get { return idIndigenousLanguage; }
            set { idIndigenousLanguage = value; }
        }

        public int _idProject
        {
            get { return idProject; }
            set { idProject = value; }
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
