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

        private const int UNDEFINED = 0;
        private const String UNKNOWN = "";

        public DevelopmentStage()
        {
            idDevelopmentStage = UNDEFINED;
            name = UNKNOWN;
        }

        public int IdDevelopmentStage
        {
            get => idDevelopmentStage;
            set => idDevelopmentStage = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }
    }
}
