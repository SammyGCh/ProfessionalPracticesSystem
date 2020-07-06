using System;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDomain;
using DataAccess.Implementation;

namespace DataAccessTests
{
    [TestClass]
    public class UpdateDataTest
    {

        [TestMethod]
        public void Update_MensualReport_Success()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();
            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                IdMensualReport = 5,
                Description = "Ahora me acaban de pedir que les haga tambien otros trabajos no puede ser" +
                "y lo peor es que estan en ensamblador como es eso ",
                MonthReportedDate = "2020 - 09 - 07 13:19:00",
                MensualReportName = "Reporte #419",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };



            bool isUpdated = mensualReportDao.UpdateMensualReport(mensualReport);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void Update_MensualReport_Unsuccess()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            ProjectDAO projectDao = new ProjectDAO();
            int projectId = 1;
            int practitionerId = 1;

            MensualReport mensualReport = new MensualReport
            {
                IdMensualReport = 5,
                Description = "Ya me voy a dar de baja la verdad esta bien feo todo esto" +
                "nos vemos en repite maestro",
                MonthReportedDate = "2020 - 09 - 08 22:10:00",
                MensualReportName = "Reporte #420",
                DerivedFrom = projectDao.GetProjectById(projectId),
                GeneratedBy = practitionerDao.GetPractitioner(practitionerId)
            };



            bool isUpdated = mensualReportDao.UpdateMensualReport(mensualReport);

            Assert.IsFalse(isUpdated);
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
