/*
    Date: 22/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class ProjectsRequestDAOTest
    {
        [TestMethod]
        public void SaveProjectsRequestSuccess()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            List<Project> projects = new List<Project>();
            ProjectsRequest projectsRequest = new ProjectsRequest();
            int idPractitioner = 3;

            projects.Add(new Project {
                IdProject = 1,
                Name = "DESARROLLO DE UN SISTEMA DE NUTRICIÓN"
            });

            projects.Add(new Project
            {
                IdProject = 2,
                Name = "DESARROLLO DE UN SISTEMA ADMINISTRATIVO DEL H. AYUNTAMIENTO"
            });

            projects.Add(new Project
            {
                IdProject = 3,
                Name = "PRUEBAS PARA EL SISTEMA DE CALIFICACIONES DE ESTUDIANTES DE LA UNIVERSIDAD VERACRUZANA"
            });

            projectsRequest.ProjectsRequested = projects;
            projectsRequest.RequestedBy = practitionerDao.GetPractitioner(idPractitioner);

            bool isSaved = projectsRequestDao.SaveProjectsRequest(projectsRequest);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllProjectsRequestSuccess()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();

            List<ProjectsRequest> projectsRequests = projectsRequestDao.GetAllProjectsRequest();

            Assert.IsTrue(projectsRequests.Count > 0);
        }
        
        [TestMethod]
        public void UpdateProjectsRequestSucces()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            int idProjectsRequest = 2;

            bool isUpdated = projectsRequestDao.UpdateProjectsRequest(idProjectsRequest);

            Assert.IsTrue(isUpdated);
        }
    }
}
