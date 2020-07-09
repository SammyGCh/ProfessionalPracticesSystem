using System;
using System.Collections.Generic;
using BusinessDomain;
using DataAccess.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccessTests
{
    [TestClass]
    public class InsertDataTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void SaveProject_NewProject_SuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idDevelopmentStage = 2;
            string linkedOrganizationName = "Instituto de Pensiones del Estado";

            Project project = new Project
            {
                Name = "Desarrollo del sistema de atención al cliente para adultos mayores",
                DirectUsersNumber = "10",
                IndirectUsersNumber = "2000",
                Duration = "200",
                GeneralGoal = "Iniciar el desarrollo del sistema, entender y encontras las necesidades de" +
                " la empresa.",
                Responsabilities = "El practicante debe asistir de forma puntual, entregar los productos" +
                " solicicitados por el responsable del proyecto. Así como ser respetuoso.",
                MediateGoals = "Establecer una base solida para el desarrollo del sistema de manera exitosa",
                InmediateGoals = "Elicitar sobre el problema",
                Metology = "Entrevistas, storyboards",
                Status = 1,
                NeededResources = "Computadora y software necesario",
                PractitionerNumber = 3,
                GeneralDescription = "Se han presentado problemas al atender a personas mayores, por lo tanto " +
                "se busca brindar un mejor servicio implementando este nuevo sistema",
                ResponsableName = "Carlos David Lopez Dominguez",
                ResponsableCharge = "Director de sistemas",
                ResponsableEmail = "calordavid@gmail.com",
                ResponsableTelephone = "2281001312",
                PractitionersAssigned = 0,
                BelongsTo = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage),
                ProposedBy = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName)
            };

            bool isSaved = projectDao.SaveProject(project);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void SaveProject_UnavailableConnection_UnsuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();

            DevelopmentStage developmentStage = new DevelopmentStage
            {
                IdDevelopmentStage = 2
            };

            LinkedOrganization linkedOrganization = new LinkedOrganization
            {
                IdLinkedOrganization = 5
            };

            Project project = new Project
            {
                Name = "Desarrollo de un sistema web para la las quejas de los clientes",
                DirectUsersNumber = "10",
                IndirectUsersNumber = "2000",
                Duration = "200",
                GeneralGoal = "Iniciar el desarrollo del sistema, entender y encontras las necesidades de" +
                " la empresa.",
                Responsabilities = "El practicante debe asistir de forma puntual, entregar los productos" +
                " solicicitados por el responsable del proyecto. Así como ser respetuoso.",
                MediateGoals = "Establecer una base solida para el desarrollo del sistema de manera exitosa",
                InmediateGoals = "Elicitar sobre el problema",
                Metology = "Entrevistas, storyboards",
                Status = 1,
                NeededResources = "Computadora y software necesario",
                PractitionerNumber = 3,
                GeneralDescription = "Se han presentado problemas al atender a personas mayores, por lo tanto " +
                "se busca brindar un mejor servicio implementando este nuevo sistema",
                ResponsableName = "Carlos David Lopez Dominguez",
                ResponsableCharge = "Director de sistemas",
                ResponsableEmail = "calordavid@gmail.com",
                ResponsableTelephone = "2281001312",
                PractitionersAssigned = 0,
                BelongsTo = developmentStage,
                ProposedBy = linkedOrganization
            };

            bool isSaved = projectDao.SaveProject(project);

            Assert.IsFalse(isSaved);
        }

        [TestMethod]
        public void SaveProjectActivity_NewProjectActivity_SuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "Desarrollo del sistema de atención al cliente para adultos mayores";

            ProjectActivity projectActivity = new ProjectActivity
            {
                Name = "Realizar entrevista a los directores encargados del modulo de adultos mayores",
                Month = "Octubre"
            };

            bool isSaved = projectDao.SaveProjectActivity(projectActivity, projectName);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void SaveProjectActivity_UnkownProjectName_UnsuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "Desarrollo";

            ProjectActivity projectActivity = new ProjectActivity
            {
                Name = "Realizar entrevista a los directores encargados del modulo de adultos mayores",
                Month = "AGOSTO"
            };

            bool isSaved = projectDao.SaveProjectActivity(projectActivity, projectName);

            Assert.IsFalse(isSaved);
        }

        [TestMethod]
        public void SaveLinkedOrganization_NewLinkedOrganization_SuccessInsert()
        {
            OrganizationSectorDAO organizationSectorDao = new OrganizationSectorDAO();
            int idOrganizationSector = 2;

            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            LinkedOrganization linkedOrganization = new LinkedOrganization
            {
                City = "XALAPA",
                Email = "promor@empresa.org.mx",
                State = "VERACRUZ",
                Name = "PROMOR",
                TelephoneNumber = "2288151233",
                Address = "FRANCISCO JAVIER CLAVIJERO 22",
                BelongsTo = organizationSectorDao.GetOrganizationSectorById(idOrganizationSector)
            };

            bool isSaved = linkedOrganizationDao.SaveLinkedOrganization(linkedOrganization);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void SaveProjectsRequest_NewProjectsRequest_SuccessInserting()
        {
            ProjectsRequestDAO projectsRequestDao = new ProjectsRequestDAO();
            PractitionerDAO practitionerDao = new PractitionerDAO();
            List<Project> projects = new List<Project>();
            ProjectsRequest projectsRequest = new ProjectsRequest();
            int idPractitioner = 6;

            projects.Add(new Project
            {
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
        public void SaveScholarPeriod_NewScholarPeriod_SuccessInsert()
        {
            ScholarPeriodDAO scholarPeriodDao = new ScholarPeriodDAO();
            ScholarPeriod scholarPeriod = new ScholarPeriod
            {
                Name = "AGOST0 2021 - ENERO 2022"
            };

            bool isSaved = scholarPeriodDao.SaveScholarPeriod(scholarPeriod);

            Assert.IsTrue(isSaved);
        }
    }
}
