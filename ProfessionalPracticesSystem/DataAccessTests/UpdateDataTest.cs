using System;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class UpdateDataTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void UpdateProject_ProjectUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.Duration = "100";
            project.ResponsableTelephone = "2888842323";

            bool isUpdated = projectDao.UpdateProject(project);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateProjectData_ProjectUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.Duration = "300";
            project.IndirectUsersNumber = "20";
            project.NeededResources = "Computadora alto rendimiento";

            bool isUpdated = projectDao.UpdateProject(project);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateProjectResponsableData_ProjectUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.ResponsableName = "María del Carmen Rodriguez Jimenez";
            project.ResponsableTelephone = "5544332211";

            bool isUpdated = projectDao.UpdateProject(project);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateProjectActivity_ProjectActivityUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.ProjectActivities[0].Month = "OCTUBRE";

            bool isUpdated = projectDao.UpdateProjectActivity(project.ProjectActivities[0], idProject);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateAvailabilityProjectStatus_KnownProject_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            bool isUpdated = projectDao.UpdateAvailabilityProjectStatus(idProject);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void DeleteProject_KnownProject_SuccessDeleting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            bool isDeleted = projectDao.DeleteProject(idProject);

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void DeleteProjectActivity_ProjectActivityUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            int idProjectActivity = project.ProjectActivities[0].IdProjectActivity;

            bool isDeleted = projectDao.DeleteProjectActivity(idProjectActivity);

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public void UpdateLinkedOrganization_LinkedOrganizationUpdated_SuccessUpdated()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            int idLinkedOrganization = 8;

            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);
            linkedOrganization.Email = "promor123456@empresa.org.mx";

            bool isUpdated = linkedOrganizationDao.UpdateLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateProjectsRequest_ProjectsRequestUpdated_SuccesUpdating()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            int idProjectsRequest = 4;

            bool isUpdated = projectsRequestDao.UpdateProjectsRequestStatus(idProjectsRequest);

            Assert.IsTrue(isUpdated);
        }
    }
}
