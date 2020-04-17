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
    }
}
