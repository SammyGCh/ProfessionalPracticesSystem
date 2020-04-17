/*
        Date: 08/04/2020                              
        Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IActivityPerformedDAO
    {
        List <ActivityPerformed> GetAllActivityPerformed();
        List <ActivityPerformed> GetActivityByPractitioner(int idPractitioner);
        ActivityPerformed GetActivityPerformed(int idActivityPerformed);
        bool NewActivityPerformed (MensualReport MensualReport);
        bool UpdateActivityPerformed(MensualReport MensualReport);

    }
}
