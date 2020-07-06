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
        List<MensualReport> GetAllReportsByPractitioner(String matricula);
        List<MensualReport> GetAllReportsByProject(int idProject);
        bool InsertMensualReport(MensualReport mensualReport);
        bool DeleteMensualReport(int idMensualReport);
	    bool UpdateMensualReport(MensualReport MensualReport);
	    MensualReport GetMensualReportById(int idMensualReport);
        List<MensualReport> GetAllReportsByAcademic(String personalNumberAcademic);
    }
}