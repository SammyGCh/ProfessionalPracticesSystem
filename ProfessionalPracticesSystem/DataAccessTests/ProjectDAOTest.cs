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
        public void SaveProjectSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            DevelopmentStageDAO developmentStageDao = new DevelopmentStageDAO();
            LinkedOrganizationDAO linkedOrganizationDao = new LinkedOrganizationDAO();

            int idDevelopmentStage = 1;
            string linkedOrganizationName = "PROMOR";

            Project project = new Project
            {
                Name = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES",
                DirectUsersNumber = 10,
                IndirectUsersNumber = 2,
                Duration = 200,
                GeneralGoal = "COMPROBAR QUE EL SISTEMA FUNCIONE DE MANERA CORRECTA",
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
        public void SaveProjectActivitySuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

            ProjectActivity projectActivity = new ProjectActivity
            {
                Name = "REALIZAR LA PRUEBA UNITARIA PARA EL MODULO DE BAJAS",
                Month = "AGOSTO"
            };

            bool isSaved = projectDao.SaveProjectActivity(projectActivity, projectName);

            Assert.IsTrue(isSaved);
        }

        [TestMethod]
        public void GetAllProjectsSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            List<Project> projects = projectDao.GetAllProjects();
            
            Assert.IsTrue(projects.Count > 0);
        }

        [TestMethod]
        public void GetProjectByIdSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);

            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void NotGetProjectByUnknownId()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 0;

            Project project = projectDao.GetProjectById(idProject);

            Assert.IsNull(project);
        }

        [TestMethod]
        public void GetProjectByNameSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

            Project project = projectDao.GetProjectByName(projectName);

            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void NotGetProjectByUnknownName()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS";

            Project project = projectDao.GetProjectByName(projectName);

            Assert.IsNull(project);
        }

        [TestMethod]
        public void GetProjectByNameWithoutActivitiesSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            string projectName = "PRUEBAS PARA EL SISTEMA DE ADMINISTRACIÓN DE REFACCIONES";

            Project project = projectDao.GetProjectByNameWithoutActivities(projectName);

            Assert.IsNotNull(project);
        }

        [TestMethod]
        public void GetAllProjectActivitiesSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            List<ProjectActivity> projectActivities = projectDao.GetAllProjectActivities(idProject);

            Assert.IsTrue(projectActivities.Count > 0);
        }

        [TestMethod]
        public void NotGetAllProjectActivitiesFromUnkownProject()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 0;

            List<ProjectActivity> projectActivities = projectDao.GetAllProjectActivities(idProject);

            Assert.IsTrue(projectActivities.Count == 0);
        }

        [TestMethod]
        public void UpdateProjectSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.Duration = 600;

            bool isUpdated = projectDao.UpdateProject(project);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void UpdateProjectActivitySuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;

            Project project = projectDao.GetProjectById(idProject);
            project.ProjectActivities[0].Month = "SEPTIEMBRE";

            bool isUpdated = projectDao.UpdateProjectActivity(project.ProjectActivities[0], idProject);

            Assert.IsTrue(isUpdated);
        }

        [TestMethod]
        public void DeleteProjectSuccess()
        {
            ProjectDAO projectDao = new ProjectDAO();
            int idProject = 1;
            
            bool isDeleted = projectDao.DeleteProject(idProject);

            Assert.IsTrue(isDeleted);
        }
    }
}
