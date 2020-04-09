/*
    Date: 08/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class ProjectActivity
    {
        private int idProjectActivity;
        private String name;
        private String month;

        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";

        public ProjectActivity()
        {
            idProjectActivity = UNDEFINED;
            name = UNKNOWN;
            month = UNKNOWN;
        }

        public int IdProjectActivity
        {
            get => idProjectActivity;
            set => idProjectActivity = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public String Month
        {
            get => month;
            set => month = value;
        }
    }
}
