/*
    Date: 06/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System;

namespace BusinessDomain
{
    public class ProfessorActivity
    {
        private int idProfessorActivity;
        private String description;
        private String name;
        private String performanceDate;
        private Academic generatedBy;
        private String observations;
        private int status;

        public ProfessorActivity(){}
        public int IdProfessorActivity
        {
            get => idProfessorActivity;
            set => idProfessorActivity = value;
        }

        public String Description
        {
            get => description; 
            set => description = value;
        }

        public String Name
        {
            get => name;
            set => name = value;
        }

        public String PerformanceDate
        {
            get => performanceDate;
            set => performanceDate = value;
        }

        public Academic GeneratedBy
        {
            get => generatedBy;
            set => generatedBy = value;
        }

        public String Observations
        {
            get => observations;
            set => observations = value;
        }

        public int Status
        {
            get => status;
            set => status = value;
        }
    }
}
