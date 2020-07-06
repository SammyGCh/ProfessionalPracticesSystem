/*
        Date: 10/04/2020                               
        Author: Cesar Sergio Martinez Palacios
 */
 
using System;

namespace BusinessDomain
{
    public class MensualReport
    {
        private int idMensualReport;
        private String description;
        private String monthReportedDate;
        private String mensualReportName;
        private Practitioner generatedBy;
        private Project derivedFrom;
        private String grade;

        public int IdMensualReport
        {
            get => idMensualReport;
            set => idMensualReport = value;
        }

        public String Description
        {
            get => description;
            set => description = value;
        }

        public String MonthReportedDate
        {
            get => monthReportedDate;
            set => monthReportedDate = value;
        }

        public String MensualReportName
        {
            get => mensualReportName;
            set => mensualReportName = value;
        }

        public String Grade
        {
            get => grade;
            set => grade = value;
        }

        public Practitioner GeneratedBy
		{
			get => generatedBy;
            set => generatedBy = value;
		}

    	public Project DerivedFrom
    	{
			get => derivedFrom;
            set => derivedFrom = value;
    	}
	}
}