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
        private String status;
        private int UNDEFINED = 0;
        private String STRUNDEFINED = "";

        public Academic()
        {
            idAcademic = UNDEFINED;
            personalNumber = STRUNDEFINED;
            names = STRUNDEFINED;
            cubicle = STRUNDEFINED;
            lastName = STRUNDEFINED;
            gender = STRUNDEFINED;
            password = STRUNDEFINED;
            belongTo = null;
            shift = STRUNDEFINED;
            status = STRUNDEFINED;

        }
        public int IdAcademic
        {
            get { return idAcademic; }
            set { idAcademic = value; }
        }

        public String PersonalNumber
        {
            get { return personalNumber; }
            set { personalNumber = value; }
        }

        public String Names
        {
            get { return names; }
            set { names = value; }
        }

        public String Cubicle
        {
            get { return cubicle; }
            set { cubicle = value; }
        }

        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        public AcademicType BelongTo
        {
            get { return belongTo; }
            set { belongTo = value; }
        }
        public String Shift
        {
            get { return shift; }
            set { shift = value; }
        }
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
