/*
    Date: 16/04/2020
    Author(s) : Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class ScholarPeriod
    {
        private int idScholarPeriod;
        private String name;
        private int status;

        public int IdScholarPeriod
        {
            get => idScholarPeriod;
            set => idScholarPeriod = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public int Status
        {
            get => status;
            set => status = value;
        }
    }
}
