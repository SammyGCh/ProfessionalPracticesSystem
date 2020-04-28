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
        List <ActivityPerformed> GetAllActivityPerformedByProfessorActivity(int idProfessorActivity);
        List <ActivityPerformed> GetAllActivitiesyPerformedByPractitioner(int idPractitioner);
        ActivityPerformed GetActivityPerformed(int idProfessorActivity, int idPractitioner);
        bool NewActivityPerformed (ActivityPerformed activityPerformed);
        bool UpdateActivityPerformed(ActivityPerformed activityPerformed);
        bool AddObservationsActivityPerformed(ActivityPerformed activityPerformed);

    }
}
