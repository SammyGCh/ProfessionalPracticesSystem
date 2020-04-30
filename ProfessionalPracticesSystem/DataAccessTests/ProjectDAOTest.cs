/*
    Date: 20/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Implementation;
using BusinessDomain;
using System.Collections.Generic;

namespace DataAccessTests
{
    [TestClass]
    public class ProjectDAOTest
    {
        [TestMethod]
        public void SaveProject_NewProject_SuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idDevelopmentStage = 1;
            string linkedOrganizationName = "CASA AHUED";

            Project project = new Project
            {
                Name = "MANTENIMIENTO AL SISTEMA DE CAJA DE COBRO",
                DirectUsersNumber = "10",
                IndirectUsersNumber = "2",
                Duration = "200",
                GeneralGoal = "BRINDAR UN MANTENIMIENTO EFICIENTE PARA AGILIZAR EL COBRO DE LAS COMPRAS",
                Responsabilities = "EL PRACTICANTE DEBE CUMPLIR CON EL HORARIO ACORDADO DE LAS ACTIVIDADES",
                MediateGoals = "GENERAR UNA DOCUMENTACIÓN SOLIDA DE LAS PRUEBAS REALIZADAS",
                InmediateGoals = "GENERAR PRUEBAS UNITARIAS",
                Metology = "PRUEBAS UNITARIAS",
                NeededResources = "COMPUTADORA DE ALTO RENDIMIENTO",
                PractitionerNumber = 3,
                GeneralDescription = "EL SISTEMA QUE SE COMENTA NO HA SIDO PROBADO",
                ResponsableName = "LUIS ALEJANDRO MOJICA",
                ResponsableCharge = "JEFE DEL DEPARTAMENTO DE SISTEMAS",
                ResponsableEmail = "luisale@gmail.com",
                ResponsableTelephone = "2281001213",
                BelongsTo = developmentStageDao.GetDevelopmentStageById(idDevelopmentStage),
                ProposedBy = linkedOrganizationDao.GetLinkedOrganizationByName(linkedOrganizationName)
            };

            bool isSaved = projectDao.SaveProject(project);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void SaveProjectActivity_NewProjectActivity_SuccessInserting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

            ProjectActivity projectActivity = new ProjectActivity
            {
                Name = "REALIZAR LA PRUEBA UNITARIA PARA EL MODULO DE CONSULTAS DE REFACCIONES",
                Month = "AGOSTO"
            };

            bool isSaved = projectDao.SaveProjectActivity(projectActivity, projectName);

            Assert.IsTrue(isSaved);
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
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

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
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

            int idProjectExpected = 1;
            int idProjectObtained = projectDao.GetIdProjectByName(projectName);

            Assert.AreEqual(idProjectExpected, idProjectObtained);
        }

        [TestMethod]
        public void GetAllProjectActivities_AvailableProjectActivities_ListWithElements()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

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
        public void UpdateProject_ProjectUpdated_SuccessUpdating()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.Duration = "500";

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
        public void DeleteProject_KnownProject_SuccessDeleting()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;
            
            bool isDeleted = projectDao.DeleteProject(idProject);

            Assert.IsTrue(isDeleted);
        }
    }
}
