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
        private AcademicType belongTo;
        private String shift;
        private int status;


        public Academic(){}
        public int IdAcademic
        {
            get => idAcademic; 
            set => idAcademic = value; 
        }

        public String PersonalNumber
        {
            get => personalNumber; 
            set => personalNumber = value; 
        }

        public String Names
        {
            get => names; 
            set => names = value; 
        }

        public String Cubicle
        {
            get => cubicle; 
            set => cubicle = value; 
        }

        public String LastName
        {
            get => lastName; 
            set => lastName = value; 
        }

        public String Gender
        {
            get => gender; 
            set => gender = value; 
        }

        public String Password
        {
            get => password; 
            set => password = value; 
        }
        public AcademicType BelongTo
        {
            get => belongTo; 
            set => belongTo = value; 
        }
        public String Shift
        {
            get => shift; 
            set => shift = value; 
        }
        public int Status
        {
            get => status; 
            set => status = value; 
        }
        public override string ToString()
        {
            return LastName + " " + Names;
        }
    }
}
