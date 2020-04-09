/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class Practising
    {
        private int idPractising;
        private String matricula;
        private String password;
        private float grade;
        private String gender;
        private String names;
        private String lastName;
        private IndigenousLanguage speaks;
        private Project assigned;
        private String status;
        private Academic instructed;
        private int UNDEFINED = 0;
        private String STRUNDEFINED = "";

        public Practising()
        {
            idPractising = UNDEFINED;
            matricula = STRUNDEFINED;
            password = STRUNDEFINED;
            grade = UNDEFINED;
            gender = STRUNDEFINED;
            names = STRUNDEFINED;
            lastName = STRUNDEFINED;
            status = STRUNDEFINED;
            speaks = null;
            assigned = null;
            instructed = null;
        }

        public int IdPractising
        {
            get { return idPractising; }
            set { idPractising = value; }
        }
        public String Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        public float Grade
        {
            get { return grade; }
            set { grade = value; }
        }
        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public String Names
        {
            get { return names; }
            set { names = value; }
        }
        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        public IndigenousLanguage Speaks
        {
            get { return speaks; }
            set { speaks = value; }
        }
        public Project Assigned
        {
            get { return assigned; }
            set { assigned = value; }
        }
        public Academic Instructed
        {
            get { return instructed; }
            set { instructed = value; }
        }
    }
}
