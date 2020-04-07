/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;

namespace BusinessDomain
{
    public class DevelopmentStage
    {
        private int idDevelopmentStage;
        private String name;

        public DevelopmentStage()
        {
            idDevelopmentStage = 0;
            name = "";
        }

        public int IdDevelopmentStage
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }
}
