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
                Name = "PROYECTO DE PRUEBA 1"
            });

            projects.Add(new Project
            {
                IdProject = 2,
                Name = "PROYECTO DE PRUEBA 2"
            });

            projects.Add(new Project
            {
                IdProject = 3,
                Name = "PROYECTO DE PRUEBA 3"
            });

            projectsRequest.ProjectsRequested = projects;
            projectsRequest.RequestedBy = practitionerDao.GetPractitioner(idPractitioner);

            bool isSaved = projectsRequestDao.SaveProjectsRequest(projectsRequest);

            Assert.IsTrue(isSaved);
        }
    }
}
