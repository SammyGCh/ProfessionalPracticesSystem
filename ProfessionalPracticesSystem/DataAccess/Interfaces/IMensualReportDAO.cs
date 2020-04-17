/*
        Date: 10/04/2020                               
            Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IMensualReportDAO
    {
        List<MensualReport> GetAllMensualReports();
        List<MensualReport> GetReportByPractitioner(int idPractitioner);
        List<MensualReport> GetReportByProject(int idProject);
        bool InsertMensualReport(MensualReport MensualReport);
        bool DeleteMensualReport(int idMensualReport);
	    bool UpdateMensualReport(MensualReport MensualReport);
	    MensualReport GetMensualReportById(int idMensualReport);
    }
}