using System;
using BusinessLogic;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessDomain;
using DataAccess.Implementation;


namespace DataAccessTests
{
    [TestClass]
    public class UpdateDataTest
    {
        readonly PractitionerDAO practitionerDAO = new PractitionerDAO();
        readonly DocumentDAO documentDAO = new DocumentDAO();
        readonly AcademicDAO academicDAO = new AcademicDAO();

        [TestMethod]
        public void UpdatePractitionerGrade_PractitionerExist_ReturnTrue()
        {
            bool result = practitionerDAO.UpdatePractitionerGrade(9);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdatePractitioner_PractitionerExist_ReturnTrue()
        {
            IndigenousLanguage auxiliarIndigenousLanguage = new IndigenousLanguage()
            {
                IdIndigenousLanguage = 1
            };

            Academic auxiliarAcademic = new Academic()
            {
                IdAcademic = 1
            };

            ScholarPeriod auxiliarScholarPeriod = new ScholarPeriod()
            {
                IdScholarPeriod = 1
            };

            Practitioner newPractitioner = new Practitioner()
            {
                IdPractitioner = 9,
                Matricula = "S56565656",
                Gender = "Masculino",
                Names = "Usbaldo",
                LastName = "Martinez",
                Speaks = auxiliarIndigenousLanguage,
                Status = 1,
                Instructed = auxiliarAcademic,
                ScholarPeriod = auxiliarScholarPeriod

            };

            bool result = practitionerDAO.UpdatePractitioner(newPractitioner);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdatePractitionerPassword_PractitionerExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            int idPractitioner = 9;

            bool result = practitionerDAO.UpdatePractitionerPassword(idPractitioner, newPassword);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeletePractitioner_PractitionerExist_ReturnTrue()
        {
            int idPractitioner = 9;

            bool result = practitionerDAO.DeletePractitioner(idPractitioner);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void AssignPractitioner_ProjectAndPractitionerExist_ReturnTrue()
        {
            int idPractitioner = 9;
            int idProject = 9;

            bool result = practitionerDAO.AssignPractitioner(idPractitioner, idProject);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateDocumentGrade_DocumentExist_ReturnTrue()
        {
            int idDocument = 15;
            String grade = "10";

            bool result = documentDAO.UpdateDocumentGrade(idDocument, grade);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteAcademic_AcademicExist_ReturnTrue()
        {
            int idAcademic = 4;

            bool result = academicDAO.DeleteAcademic(idAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAcademic_AcademicExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            AcademicType auxiliarAcademicType = new AcademicType()
            {
                IdAcademicType = 2
            };

            Academic changeAcademic = new Academic()
            {
                IdAcademic = 4,
                Names = "Laura",
                LastName = "Sanchez Morales",
                PersonalNumber = "S12345678",
                Password = newPassword,
                Cubicle = "00",
                Gender = "Femenino",
                Shift = "Pendiente",
                BelongTo = auxiliarAcademicType

            };

            bool result = academicDAO.UpdateAcademic(changeAcademic);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateAcademicPassword_AcademicExist_ReturnTrue()
        {
            String password = "universidadVeracruzana";
            HashManagement hashManager = new HashManagement();
            String newPassword = hashManager.TextToHash(password);

            int idAcademic = 4;

            bool result = academicDAO.UpdateAcademicPassword(idAcademic,newPassword);

            Assert.IsTrue(result);
        }

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
