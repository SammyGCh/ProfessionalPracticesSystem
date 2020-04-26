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
        public void SaveProfessorActivitySuccess() 
        {
            Academic academic = new Academic
            {
                IdAcademic = 1
            };

            ProfessorActivity activity = new ProfessorActivity
            {
                Description = "Descripcion de actividad",
                Name = "Nombre de actividad",
                ValueActivity = 7,
                PerformanceDate = "2020-04-04 22:10:00",
                GeneratedBy = academic
            };

            bool result = professorActivityDAO.SaveProfessorActivity(activity);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteProfessorActivitySuccess()
        {
            int idActivity = 3;

            bool result = professorActivityDAO.DeleteProfessorActivity(idActivity);

            Assert.IsTrue(result);
        }

        [TestMethod]

        public void GetAllProfessorActivitySuccess()
        {
            int idAcademic = 1;
            List<ProfessorActivity> result = professorActivityDAO.GetAllProfessorActivityByAcademic(idAcademic);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetProfessorActivitySuccess()
        {
            int idActivity = 1;

            ProfessorActivity result = professorActivityDAO.GetProfessorActivity(idActivity);

            Assert.IsNotNull(result);
        }
    }
}
