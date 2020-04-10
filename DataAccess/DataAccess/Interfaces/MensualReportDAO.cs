/*
        Date: 10/04/2020                               
            Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface MensualReportDAO
    {
        List<MensualReport> GetAllMensualReports();
        List<MensualReport> GetReportByPractising(int idPractising);
        List<Document> GetReportByProject(int idProject);
        bool InsertMensualReport(MensualReport MensualReport);
        bool DeleteMensualReport(int idMensualReport);
	    bool UpdateMensualReport(MensualReport MensualReport);
	    MensualReport GetMensualReport(int idMensualReport);
    }
}