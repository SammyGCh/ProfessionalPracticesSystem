/*
    Date: 23/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDomain;
using DataAccess.Implementation;

namespace DataAccessTests
{
    [TestClass]
    public class ProfessorActivityDAOTest
    {
        ProfessorActivityDAO professorActivityDAO = new ProfessorActivityDAO();

        [TestMethod]
        public void SaveProfessorActivity_ProfessorActivityIsComplete_ReturnTrue() 
        {
            Academic academic = new Academic
            {
                IdAcademic = 1
            };

            ProfessorActivity activity = new ProfessorActivity
            {
                Description = "Descripcion de actividad",
                Name = "Nombre de actividad",
                PerformanceDate = "2020-04-04 22:10:00",
                GeneratedBy = academic,
                Observations = "La actividad fue realizada de una buena manera y la redaccion hecha esta adecuada al tipo de actividad",
                Status = 1
            };

            bool result = professorActivityDAO.SaveProfessorActivity(activity);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteProfessorActivity_ProfessorActivityExist_ReturnTrue()
        {
            int idActivity = 1;

            bool result = professorActivityDAO.DeleteProfessorActivity(idActivity);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllProfessorActivity_ExistAtLeastOneProfessorActivity_ReturnProfessorActivityList()
        {
            List<ProfessorActivity> result = professorActivityDAO.GetAllProfessorActivity();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetAllProfessorActivityByAcademic_AcademicHasAtLeastOneActivity_ReturnProfessorActivityList()
        {
            int idAcademic = 1;
            List<ProfessorActivity> result = professorActivityDAO.GetAllProfessorActivityByAcademic(idAcademic);

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetProfessorActivity_ProfessorActivityExist_ReturnProfessorActivity()
        {
            int idActivity = 1;

            ProfessorActivity result = professorActivityDAO.GetProfessorActivity(idActivity);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateProfessorActivity_ProfessorActivityIsComplete_ReturnTrue()
        {
            ProfessorActivity activity = new ProfessorActivity
            {
                IdProfessorActivity = 8,
                Description = "Se cambio la descripcion de la actividad",
                Name = "El nombre de la actividad fue cambiado",
                PerformanceDate = "2025-05-05 22:10:00",
                Observations = "La actividad fue realizada de una buena manera y la redaccion hecha esta adecuada al tipo de actividad"
            };

            bool result = professorActivityDAO.UpdateProfessorActivity(activity);

            Assert.IsTrue(result);
        }
    }
}
