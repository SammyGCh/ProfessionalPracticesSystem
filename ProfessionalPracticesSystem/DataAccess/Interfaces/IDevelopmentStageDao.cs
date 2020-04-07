/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using BusinessDomain;

namespace DataAccess.Interfaces
{
    public interface IDevelopmentStageDao
    {
        List<DevelopmentStage> GetAllDevelopmentStages();
        DevelopmentStage GetDevelopmentStage(int idDevelopmentStage);
    }
}
