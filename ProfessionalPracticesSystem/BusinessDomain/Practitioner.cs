/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class Practitioner
    {
        private int idPractitioner;
        private String matricula;
        private String password;
        private String grade;
        private String gender;
        private String names;
        private String lastName;
        private IndigenousLanguage speaks;
        private Project assigned;
        private int status;
        private Academic instructed;
        private ScholarPeriod scholarPeriod;

        public Practitioner(){}

        public int IdPractitioner
        {
            get => idPractitioner;
            set => idPractitioner = value;
        }
        public String Matricula
        {
            get => matricula;
            set => matricula = value;
        }
        public String Password
        {
            get => password;
            set => password = value;
        }
        public String Grade
        {
            get => grade;
            set => grade = value;
        }
        public String Gender
        {
            get => gender;
            set => gender = value;
        }
        public String Names
        {
            get => names;
            set => names = value;
        }
        public String LastName
        {
            get => lastName;
            set => lastName = value;
        }
        public int Status
        {
            get => status;
            set => status = value;
        }
        public IndigenousLanguage Speaks
        {
            get => speaks;
            set => speaks = value;
        }
        public Project Assigned
        {
            get => assigned;
            set => assigned = value;
        }
        public Academic Instructed
        {
            get => instructed;
            set => instructed = value;
        }
        public ScholarPeriod ScholarPeriod
        {
            get => scholarPeriod;
            set => scholarPeriod = value;
        }
        public override string ToString()
        {
            return LastName + " " + Names;
        }
    }
}
