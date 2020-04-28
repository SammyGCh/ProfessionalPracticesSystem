/*
    Date: 23/04/2020
    Author(s) : César Sergio Martinez Palacios
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataAccessTests
{
    [TestClass]
    public class ActivityPerformedDAOTest
    {
        ActivityPerformedDAO activityPerformedDao = new ActivityPerformedDAO();

        [TestMethod]
        public void GetById_ActivityPerformed_Success()
        {
            int idPractitioner = 1;
            int idProfesor = 1;
            ActivityPerformed activityPerformed = activityPerformedDao.GetActivityPerformed(idProfesor, idPractitioner);
            Assert.IsNotNull(activityPerformed);
        }

        [TestMethod]
        public void GetByPractitioner_AllActivitiesyPerformed_Success() 
        {
            int idPractitioner = 1;
            List<ActivityPerformed> activitiesPerformed = activityPerformedDao.GetAllActivitiesyPerformedByPractitioner(idPractitioner);
            int expectedResult = 1;
            int obtainedResult = activitiesPerformed.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetByProfessor_AllActivitiesyPerformed_Success()
        {
            int idProfessor = 1;
            List<ActivityPerformed> activitiesPerformed = activityPerformedDao.GetAllActivitiesyPerformedByPractitioner(idProfessor);
            int expectedResult = 1;
            int obtainedResult = activitiesPerformed.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void Register_ActivityPerformed_Success()
        {
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProfessorActivityDAO professorActivityDao = new ProfessorActivityDAO();

            int idProfesorActivity = 1;
            int idPractitioner = 1;

            ActivityPerformed activityPerformed = new ActivityPerformed
            {
                
                GeneratedBy = professorActivityDao.GetProfessorActivity(idProfesorActivity),
                PerformedBy = practitionerDao.GetPractitioner(idPractitioner),
                PerformedDate = "2020-06-02 00:00:00",
                ActivityReply = "Esta es mi nueva actividad recien echa para usted profesor",
            };

            bool isSaved = activityPerformedDao.NewActivityPerformed(activityPerformed);

            Assert.IsTrue(isSaved);
        
        }

        [TestMethod]
        public void Update_ActivityPerformed_Success()
        {
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProfessorActivityDAO professorActivityDao = new ProfessorActivityDAO();

            int idProfesorActivity = 1;
            int idPractitioner = 1;

            ActivityPerformed activityPerformed = new ActivityPerformed
            {

                GeneratedBy = professorActivityDao.GetProfessorActivity(idProfesorActivity),
                PerformedBy = practitionerDao.GetPractitioner(idPractitioner),
                PerformedDate = "2020-02-02 00:00:00",
                ActivityReply = "Esta es mi actualizacion de actividad",
            };

            bool isUpdated = activityPerformedDao.UpdateActivityPerformed(activityPerformed);

            Assert.IsTrue(isUpdated);
        }
        [TestMethod]
        public void UpdateObservations_ActivityPerformed_Success()
        {
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProfessorActivityDAO professorActivityDao = new ProfessorActivityDAO();

            int idProfesorActivity = 1;
            int idPractitioner = 1;

            ActivityPerformed activityPerformed = new ActivityPerformed
            {

                GeneratedBy = professorActivityDao.GetProfessorActivity(idProfesorActivity),
                PerformedBy = practitionerDao.GetPractitioner(idPractitioner),
                Observations = "Buen trabajo sigue asi!"
            };

            bool isUpdated = activityPerformedDao.AddObservationsActivityPerformed(activityPerformed);

            Assert.IsTrue(isUpdated);
        }

    }
}
