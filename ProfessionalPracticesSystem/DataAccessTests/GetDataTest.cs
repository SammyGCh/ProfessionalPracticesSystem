using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class GetDataTest
    {

        [TestMethod]
        public void GetAministrator_Administrator()
        {
            AdministratorDAO administratorDao = new AdministratorDAO();
            string administratorUser = "S42069420";
            Administrator administrator = administratorDao.GetAdministratorByUser(administratorUser);
            Assert.IsNotNull(administrator);
        }

        [TestMethod]
        public void GetMensualReports_PractitionerReports_ListReports()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            string practitionerMatricula = "S19012186";
            List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByPractitioner(practitionerMatricula);

            int expectedResult = 3;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetMensualReports_ProfessorPractitionersReports_ListReports()
        {
            MensualReportDAO mensualReportDao = new MensualReportDAO();
            string professorPersonalNumber = "S99999999";
            List<MensualReport> mensualReports = mensualReportDao.GetAllReportsByAcademic(professorPersonalNumber);
            int expectedResult = 4;
            int obtainedResult = mensualReports.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetIndigenousLanguages_AllIndigenousLanguages_ListIndigenousLaunguage()
        {
            IndigenousLanguageDAO indigenousLanguageDao = new IndigenousLanguageDAO();
            List<IndigenousLanguage> indigenousLanguages = indigenousLanguageDao.GetAllIndigenousLanguages();
            int expectedResult = 3;
            int obtainedResult = indigenousLanguages.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetIndigenousLanguage_IndigenousLanguage_ByIdIndigenousLanguage()
        {
            int idIndigenousLanguage = 1;
            IndigenousLanguageDAO indigenousLanguageDao = new IndigenousLanguageDAO();
            IndigenousLanguage indigenousLanguage = indigenousLanguageDao.GetIndigenousLanguageById(idIndigenousLanguage);
            Assert.IsNotNull(indigenousLanguage);
        }

        [TestMethod]
        public void GetAcademicTypes_AllAcademicTypes_ListAcademicTypes()
        {
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            List<AcademicType> academicTypes = academicTypeDao.GetAllAcademicTypes();
            int expectedResult = 2;
            int obtainedResult = academicTypes.Count;
            Assert.AreEqual(expectedResult, obtainedResult);
        }

        [TestMethod]
        public void GetAcademicTyoe_AcademicType_ByIdAcademicType()
        {
            int academicTypeId = 1;
            AcademicTypeDAO academicTypeDao = new AcademicTypeDAO();
            AcademicType academicType = academicTypeDao.GetAcademicTypeById(academicTypeId);
            Assert.IsNotNull(academicType);
        }

        [TestMethod]
        public void GetAllProjects_AvailableProjects_ListWithElements()
        {
            ProjectDAO projectDao = new ProjectDAO();
            List<Project> projects = projectDao.GetAllProjects();

            Assert.IsTrue(projects.Count > 0);
        }

        [TestMethod]
        public void GetProjectById_KnownIdProject_ProjectWithSameId()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);

            Assert.AreEqual(idProject, project.IdProject);
        }

        [TestMethod]
        public void GetProjectById_UnkownIdProject_NullObject()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 0;

            Project project = projectDao.GetProjectById(idProject);

            Assert.IsNull(project);
        }

        [TestMethod]
        public void GetProjectByName_KnownProjectName_ProjectWithSameName()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "MANTENIMIENTO AL SISTEMA DE CAJA DE COBRO";

            Project project = projectDao.GetProjectByName(projectName);

            Assert.AreEqual(projectName, project.Name);
        }

        [TestMethod]
        public void GetProjectByName_UnknownProjectName_NullObject()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS";

            Project project = projectDao.GetProjectByName(projectName);

            Assert.IsNull(project);
        }

        [TestMethod]
        public void GetIdProjectByName_KnownIdProject_ProjectWithSameName()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "MANTENIMIENTO AL SISTEMA DE CAJA DE COBRO";

            int idProjectExpected = 1;
            int idProjectObtained = projectDao.GetIdProjectByName(projectName);

            Assert.AreEqual(idProjectExpected, idProjectObtained);
        }

        [TestMethod]
        public void GetAllProjectActivities_AvailableProjectActivities_ListWithElements()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 6;

            List<ProjectActivity> projectActivities = projectDao.GetAllProjectActivities(idProject);

            Assert.IsTrue(projectActivities.Count > 0);
        }

        [TestMethod]
        public void GetAllProjectActivities_UnkownProject_EmptyList()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 0;

            List<ProjectActivity> projectActivities = projectDao.GetAllProjectActivities(idProject);

            Assert.IsFalse(projectActivities.Count > 0);
        }

        [TestMethod]
        public void GetActiveProjects_AvailableProjects_ListWithActiveProjects()
        {
            ProjectDAO projectDao = new ProjectDAO();

            List<Project> projectActivities = projectDao.GetActiveProjects();

            int activeStatus = 1;

            projectActivities.ForEach(project =>
                Assert.IsTrue(project.Status == activeStatus)
            );
        }

        [TestMethod]
        public void GetProjectRequestInfo_KnownProject_ProjectWithSameInfo()
        {
            ProjectDAO projectDao = new ProjectDAO();

            int idProject = 1;

            Project expectedProject = new Project()
            {
                GeneralDescription = "EL SISTEMA QUE SE COMENTA NO HA SIDO PROBADO"
            };

            Project projectObtained = projectDao.GetProjectRequestInfo(idProject);

            Assert.AreEqual(expectedProject.GeneralDescription, projectObtained.GeneralDescription);
        }

        [TestMethod]
        public void GetProjectAvailabilityInfo_KnownProject_ProjectWithSameAvailabilityInfo()
        {
            ProjectDAO projectDao = new ProjectDAO();

            int idProject = 1;

            Project expectedProject = new Project()
            {
                PractitionerNumber = 3
            };

            Project projectObtained = projectDao.GetProjectAvailabilityInfo(idProject);

            Assert.AreEqual(expectedProject.PractitionerNumber, projectObtained.PractitionerNumber);
        }

        [TestMethod]
        public void GetProjectByOrganization_KnownOrganization_ProjectWithSameOrganization()
        {
            ProjectDAO projectDao = new ProjectDAO();

            int idLinkedOrganization = 5;

            List<Project> projects = projectDao.GetProjectsByOrganization(idLinkedOrganization);

            projects.ForEach(project =>
                Assert.AreEqual(project.ProposedBy.IdLinkedOrganization, idLinkedOrganization)
            );
        }

        [TestMethod]
        public void GetAllDevelopmentStages_AvailableDevelopmentStages_ListWithElements()
        {
            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            List<DevelopmentStage> developmentStages = developmentStageDao.GetAllDevelopmentStages();

            Assert.IsTrue(developmentStages.Count > 0);
        }

        [TestMethod]
        public void GetDevelopmentStageById_KnownDevelopmentStage_DevelopmentStageWithSameId()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 1;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.AreEqual(idDevelopmentStage, developmentStage.IdDevelopmentStage);
        }

        [TestMethod]
        public void GetDevelopmentStageById_UnkownDevelopmentStage_NullObject()
        {
            DevelopmentStageDAO developmentStageDao;
            developmentStageDao = new DevelopmentStageDAO();

            int idDevelopmentStage = 0;
            DevelopmentStage developmentStage = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage);

            Assert.IsNull(developmentStage);
        }

        [TestMethod]
        public void GetAllLinkedOrganizations_AvailableLinkedOrganizations_ListWithElements()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetAllLinkedOrganizations();

            Assert.IsTrue(linkedOrganizations.Count > 0);
        }

        [TestMethod]
        public void GetLinkedOrganizationById_KownLinkedOrganizationId_LinkedOrganizationWithSameId()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 1;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.AreEqual(idLinkedOrganization, linkedOrganization.IdLinkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationById_UnknownLinkedOrganizationId_NullObject()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idLinkedOrganization = 0;
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationById(idLinkedOrganization);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationByName_KnownLinkedOrganizationName_LinkedOrganizationWithSameName()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.AreEqual(linkedOrganizationName, linkedOrganization.Name);
        }

        [TestMethod]
        public void GetLinkedOrganizationByName_UnkownLinkedOrganizationName_NullObject()
        {
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            string linkedOrganizationName = "PROMOR12344";
            LinkedOrganization linkedOrganization = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName);

            Assert.IsNull(linkedOrganization);
        }

        [TestMethod]
        public void GetLinkedOrganizationBySector_KnownOrganizationSector_LinkedOrganizationsWithSameSector()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            linkedOrganizations.ForEach(linkedOrganization =>
                Assert.AreEqual(organizationSector.Name, linkedOrganization.BelongsTo.Name)
            );
        }

        [TestMethod]
        public void GetLinkedOrganizationBySector_UnkownOrganizationSector_ListWithoutElements()
        {

            OrganizationSector organizationSector = new OrganizationSector
            {
                IdOrganizationSector = 0
            };

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();
            List<LinkedOrganization> linkedOrganizations = linkedOrganizationDao.GetLinkedOrganizationBySector(organizationSector);

            Assert.IsFalse(linkedOrganizations.Count > 0);
        }

        [TestMethod]
        public void GetAllOrganizationSectors_AvailableOrganizationSectors_ListWithElements()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();
            List<OrganizationSector> organizationSectors = organizationSectorDao.GetAllOrganizationSectors();

            Assert.IsTrue(organizationSectors.Count > 0);
        }

        [TestMethod]
        public void GetOrganizationSectorById_KnownOrganizationSector_OrganizationSectorWithSameId()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 1;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.AreEqual(idOrganizationSector, organizationSector.IdOrganizationSector);
        }

        [TestMethod]
        public void GetOrganizationSectorById_UnknownOrganizationSector_NullObject()
        {
            OrganizationSectorDAO organizationSectorDao;
            organizationSectorDao = new OrganizationSectorDAO();

            int idOrganizationSector = 0;
            OrganizationSector organizationSector = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector);

            Assert.IsNull(organizationSector);
        }

        [TestMethod]
        public void GetAllProjectsRequest_AvailableProjectsRequest_ListWithActiveRequest()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();

            List<ProjectsRequest> projectsRequests = projectsRequestDao.GetAllProjectsRequestActive();
            int activeStatus = 1;

            projectsRequests.ForEach(projectsRequest =>
                Assert.AreEqual(projectsRequest.Status, activeStatus)
            );
        }

        [TestMethod]
        public void ExistsProjectsRequestFromPractitioner_PractitionerWithProjectsRequested_Exists()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            string practitionerMatricula = "S18000000";

            int expectedResult = 1;
            int actualResult = projectsRequestDao.ExistsProjectsRequestFromPractitioner(practitionerMatricula);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetAllScholarPeriod_AvailableScholarPeriods_ListWithElements()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            List<ScholarPeriod> scholarPeriods = scholarPeriodDao.GetAllScholarPeriods();

            Assert.IsTrue(scholarPeriods.Count > 0);
        }

        [TestMethod]
        public void GetScholarPeriodById_KnownScholarPeriod_ScholarPeriodWithSameId()
        {
            int idScholarPeriod = 1;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.AreEqual(idScholarPeriod, scholarPeriod.IdScholarPeriod);
        }

        [TestMethod]
        public void GetScholarPeriodId_UnknownScholarPeriodId_NullObject()
        {
            int idScholarPeriod = 0;
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();

            ScholarPeriod scholarPeriod = scholarPeriodDao.GetScholarPeriodById(idScholarPeriod);

            Assert.IsNull(scholarPeriod);
        }
    }
}
