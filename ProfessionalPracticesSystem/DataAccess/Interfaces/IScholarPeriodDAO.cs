/*
    Date: 16/04/2020
    Author(s) : Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IScholarPeriodDAO
    {
        bool SaveScholarPeriod(ScholarPeriod scholarPeriod);
        List<ScholarPeriod> GetAllScholarPeriods();
        ScholarPeriod GetScholarPeriodById(int idScholarPeriod);
    }
}
