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
    	private String mensualReportName;;
    	private Practising generatedBy;
    	private Project derivedFrom;
		private int UNDEFINED = 0;
        private String UNKNOWN = "";

    	public MensualReport()
    	{
    		dMensualReport = UNDEFINED;
        	description = UNKNOWN;
        	monthReportedDate = UNKNOWN;
        	mensualReportName = UNKNOWN;
        	generatedBy = null;
        	derivedFrom = null;
    	}

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

    	public String generatedBy
		{
			get => generatedBy;
            set => generatedBy = value;
		}

    	public int derivedFrom
    	{
			get => derivedFrom;
            set => derivedFrom = value;
    	}
	}
}