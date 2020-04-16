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
        private int valueActivity;
        private String performanceDate;
        private Academic generatedBy;

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

        public int ValueActivity
        {
            get => valueActivity;
            set => valueActivity = value;
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
    }
}
