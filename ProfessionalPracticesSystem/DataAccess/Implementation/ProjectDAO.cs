/*
    Date: 08/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using BusinessDomain;

namespace DataAccess.Implementation
{
    public class ProjectDAO : IProjectDAO
    {
        private List<Project> projects;
        private Project project;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader, activityReader;
        private DevelopmentStageDAO developmentStageHandler;
        private LinkedOrganizationDAO linkedOrganizationHandler;
        private const int ACTIVE = 1;
        private const int NO_ACTIVE = 0;
        private const int NO_ASSIGNED = 0;

        public ProjectDAO()
        {
            projects = null;
            project = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
            developmentStageHandler = null;
            linkedOrganizationHandler = null;
        }

        public bool SaveProject(Project project)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO Project (name, directUsersNumber, indirectUsersNumber, duration, generalGoal, responsabilities, " +
                    "mediateGoals, inmediateGoals, metology, status, neededResources, practitionerNumber, generalDescription, responsableName, " +
                    "responsableCharge, responsableEmail, responsableTelephone, practitionersAssigned, idDevelopmentStage, idLinkedOrganization) VALUES (" +
                    "@name, @directUsersNumber, @indirectUsersNumber, @duration, @generalGoal, @responsabilities, @mediateGoals, " +
                    "@inmediateGoals, @metology, @status, @neededResources, @practitionerNumber, @generalDescription, @responsableName, " +
                    "@responsableCharge, @responsableEmail, @responsableTelephone, @practitionersAssigned, @idDevelopmentStage, @idLinkedOrganization)"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = project.Name;
                query.Parameters.Add("@directUsersNumber", MySqlDbType.VarChar, 3).Value = project.DirectUsersNumber;
                query.Parameters.Add("@indirectUsersNumber", MySqlDbType.VarChar, 3).Value = project.IndirectUsersNumber;
                query.Parameters.Add("@duration", MySqlDbType.VarChar, 5).Value = project.Duration;
                query.Parameters.Add("@generalGoal", MySqlDbType.VarChar, 300).Value = project.GeneralGoal;
                query.Parameters.Add("@responsabilities", MySqlDbType.VarChar, 300).Value = project.Responsabilities;
                query.Parameters.Add("@mediateGoals", MySqlDbType.VarChar, 300).Value = project.MediateGoals;
                query.Parameters.Add("@inmediateGoals", MySqlDbType.VarChar, 300).Value = project.InmediateGoals;
                query.Parameters.Add("@metology", MySqlDbType.VarChar, 100).Value = project.Metology;
                query.Parameters.Add("@status", MySqlDbType.Int32, 2).Value = ACTIVE;
                query.Parameters.Add("@neededResources", MySqlDbType.VarChar, 300).Value = project.NeededResources;
                query.Parameters.Add("@practitionerNumber", MySqlDbType.Int32, 2).Value = project.PractitionerNumber;
                query.Parameters.Add("@generalDescription", MySqlDbType.LongText).Value = project.GeneralDescription;
                query.Parameters.Add("@responsableName", MySqlDbType.VarChar, 60).Value = project.ResponsableName;
                query.Parameters.Add("@responsableCharge", MySqlDbType.VarChar, 60).Value = project.ResponsableCharge;
                query.Parameters.Add("@responsableEmail", MySqlDbType.VarChar, 60).Value = project.ResponsableEmail;
                query.Parameters.Add("@responsableTelephone", MySqlDbType.VarChar, 10).Value = project.ResponsableTelephone;
                query.Parameters.Add("@practitionersAssigned", MySqlDbType.Int32, 2).Value = NO_ASSIGNED;
                query.Parameters.Add("@idDevelopmentStage", MySqlDbType.Int32, 2).Value = project.BelongsTo.IdDevelopmentStage;
                query.Parameters.Add("@idLinkedOrganization", MySqlDbType.Int32, 2).Value = project.ProposedBy.IdLinkedOrganization;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool SaveProjectActivity(ProjectActivity projectActivity, String projectName)
        {
            bool isSaved = false;

            try
            {
                int idProject;
                idProject = GetIdProjectByName(projectName);

                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO ProjectActivity (name, month, idProject) VALUES (@name, @month, @idProject)"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = projectActivity.Name;
                query.Parameters.Add("@month", MySqlDbType.VarChar, 15).Value = projectActivity.Month;
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = idProject;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<Project> GetAllProjects()
        {
            projects = new List<Project>();
            developmentStageHandler = new DevelopmentStageDAO();
            linkedOrganizationHandler = new LinkedOrganizationDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM Project"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    project = new Project
                    {
                        IdProject = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        DirectUsersNumber = reader.GetString(2),
                        IndirectUsersNumber = reader.GetString(3),
                        Duration = reader.GetString(4),
                        GeneralGoal = reader.GetString(5),
                        Responsabilities = reader.GetString(6),
                        MediateGoals = reader.GetString(7),
                        InmediateGoals = reader.GetString(8),
                        Metology = reader.GetString(9),
                        Status = reader.GetInt32(10),
                        NeededResources = reader.GetString(11),
                        PractitionerNumber = reader.GetInt32(12),
                        GeneralDescription = reader.GetString(13),
                        ResponsableName = reader.GetString(14),
                        ResponsableCharge = reader.GetString(15),
                        ResponsableEmail = reader.GetString(16),
                        ResponsableTelephone = reader.GetString(17),
                        PractitionersAssigned = reader.GetInt32(18),
                        BelongsTo = developmentStageHandler.GetDevelopmentStageById(reader.GetInt32(19)),
                        ProposedBy = linkedOrganizationHandler.GetLinkedOrganizationById(reader.GetInt32(20)),
                        ProjectActivities = GetAllProjectActivities(reader.GetInt32(0))
                    };

                    projects.Add(project);
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                if(reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return projects;
        }

        public Project GetProjectById(int idProject)
        {
            developmentStageHandler = new DevelopmentStageDAO();
            linkedOrganizationHandler = new LinkedOrganizationDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM Project WHERE idProject = @idProject"
                };

                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = idProject;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    project = new Project
                    {
                        IdProject = idProject,
                        Name = reader.GetString(1),
                        DirectUsersNumber = reader.GetString(2),
                        IndirectUsersNumber = reader.GetString(3),
                        Duration = reader.GetString(4),
                        GeneralGoal = reader.GetString(5),
                        Responsabilities = reader.GetString(6),
                        MediateGoals = reader.GetString(7),
                        InmediateGoals = reader.GetString(8),
                        Metology = reader.GetString(9),
                        Status = reader.GetInt32(10),
                        NeededResources = reader.GetString(11),
                        PractitionerNumber = reader.GetInt32(12),
                        GeneralDescription = reader.GetString(13),
                        ResponsableName = reader.GetString(14),
                        ResponsableCharge = reader.GetString(15),
                        ResponsableEmail = reader.GetString(16),
                        ResponsableTelephone = reader.GetString(17),
                        PractitionersAssigned = reader.GetInt32(18),
                        BelongsTo = developmentStageHandler.GetDevelopmentStageById(reader.GetInt32(19)),
                        ProposedBy = linkedOrganizationHandler.GetLinkedOrganizationById(reader.GetInt32(20))
                        
                    };

                    project.ProjectActivities = GetAllProjectActivities(idProject);
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return project;
        }

        public Project GetProjectByName(String name)
        {
            developmentStageHandler = new DevelopmentStageDAO();
            linkedOrganizationHandler = new LinkedOrganizationDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM Project WHERE name = @name"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = name;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    project = new Project
                    {
                        IdProject = reader.GetInt32(0),
                        Name = name,
                        DirectUsersNumber = reader.GetString(2),
                        IndirectUsersNumber = reader.GetString(3),
                        Duration = reader.GetString(4),
                        GeneralGoal = reader.GetString(5),
                        Responsabilities = reader.GetString(6),
                        MediateGoals = reader.GetString(7),
                        InmediateGoals = reader.GetString(8),
                        Metology = reader.GetString(9),
                        Status = reader.GetInt32(10),
                        NeededResources = reader.GetString(11),
                        PractitionerNumber = reader.GetInt32(12),
                        GeneralDescription = reader.GetString(13),
                        ResponsableName = reader.GetString(14),
                        ResponsableCharge = reader.GetString(15),
                        ResponsableEmail = reader.GetString(16),
                        ResponsableTelephone = reader.GetString(17),
                        PractitionersAssigned = reader.GetInt32(18),
                        BelongsTo = developmentStageHandler.GetDevelopmentStageById(reader.GetInt32(19)),
                        ProposedBy = linkedOrganizationHandler.GetLinkedOrganizationById(reader.GetInt32(20)),
                        ProjectActivities = GetAllProjectActivities(reader.GetInt32(0))
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return project;
        }

        public int GetIdProjectByName(String name)
        {
            int idProject = 0;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT idProject FROM Project WHERE name = @name"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = name;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    idProject = reader.GetInt32(0);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return idProject;
        }

        public List<ProjectActivity> GetAllProjectActivities(int idProject)
        {
            List<ProjectActivity> projectActivities = new List<ProjectActivity>();
            ProjectActivity projectActivity;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM ProjectActivity WHERE idProject = @idProject"
                };

                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = idProject;

                activityReader = query.ExecuteReader();

                while (activityReader.Read())
                {
                    projectActivity = new ProjectActivity
                    {
                        IdProjectActivity = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Month = reader.GetString(2)
                    };

                    projectActivities.Add(projectActivity);
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                activityReader.Close();
                connection.CloseConnection();
            }

            return projectActivities;
        }

        public bool UpdateProject(Project projectUpdated)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE Project SET name = @name, directUsersNumber = @directUsersNumber, " +
                    "indirectUsersNumber = @IndirectUsersNumber, duration = @duration, generalGoal = @generalGoal, " +
                    "responsabilities = @responsabilities, mediateGoals = @mediateGoals, inmediateGoals = @inmediateGoals, metology = @metology, " +
                    "status = @status, neededResources = @neededResources, practitionerNumber = @practitionerNumber, " +
                    "generalDescription = @generalDescription, responsableName = @responsableName, responsableCharge = @responsableCharge, " +
                    "responsableEmail = @responsableEmail, responsableTelephone = @responsableTelephone, " +
                    "practitionersAssigned = @practitionersAssigned, idDevelopmentStage = @idDevelopmentStage WHERE idProject = @idProject"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = projectUpdated.Name;
                query.Parameters.Add("@directUsersNumber", MySqlDbType.VarChar, 3).Value = project.DirectUsersNumber;
                query.Parameters.Add("@indirectUsersNumber", MySqlDbType.VarChar, 3).Value = project.IndirectUsersNumber;
                query.Parameters.Add("@duration", MySqlDbType.VarChar, 5).Value = project.Duration;
                query.Parameters.Add("@generalGoal", MySqlDbType.VarChar, 300).Value = projectUpdated.GeneralGoal;
                query.Parameters.Add("@responsabilities", MySqlDbType.VarChar, 300).Value = projectUpdated.Responsabilities;
                query.Parameters.Add("@mediateGoals", MySqlDbType.VarChar, 300).Value = projectUpdated.MediateGoals;
                query.Parameters.Add("@inmediateGoals", MySqlDbType.VarChar, 300).Value = projectUpdated.InmediateGoals;
                query.Parameters.Add("@metology", MySqlDbType.VarChar, 100).Value = projectUpdated.Metology;
                query.Parameters.Add("@status", MySqlDbType.VarChar, 100).Value = projectUpdated.Status;
                query.Parameters.Add("@neededResources", MySqlDbType.VarChar, 300).Value = projectUpdated.NeededResources;
                query.Parameters.Add("@practitionerNumber", MySqlDbType.Int32, 2).Value = projectUpdated.PractitionerNumber;
                query.Parameters.Add("@generalDescription", MySqlDbType.LongText).Value = projectUpdated.GeneralDescription;
                query.Parameters.Add("@responsableName", MySqlDbType.VarChar, 60).Value = projectUpdated.ResponsableName;
                query.Parameters.Add("@responsableCharge", MySqlDbType.VarChar, 60).Value = projectUpdated.ResponsableCharge;
                query.Parameters.Add("@responsableEmail", MySqlDbType.VarChar, 60).Value = projectUpdated.ResponsableEmail;
                query.Parameters.Add("@responsableTelephone", MySqlDbType.VarChar, 10).Value = projectUpdated.ResponsableTelephone;
                query.Parameters.Add("@practitionersAssigned", MySqlDbType.Int32, 2).Value = projectUpdated.PractitionersAssigned;
                query.Parameters.Add("@idDevelopmentStage", MySqlDbType.Int32, 2).Value = projectUpdated.BelongsTo.IdDevelopmentStage;
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = projectUpdated.IdProject;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public bool UpdateProjectActivity(ProjectActivity projectActivityUpdated, int idProject)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE ProjectActivity SET name = @name, month = @month WHERE idProjectActivity = @idProjectActivity AND " +
                    "idProject = @idProject"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 255).Value = projectActivityUpdated.Name;
                query.Parameters.Add("@month", MySqlDbType.VarChar, 15).Value = projectActivityUpdated.Month;
                query.Parameters.Add("@idProjectActivity", MySqlDbType.Int32, 2).Value = projectActivityUpdated.IdProjectActivity;
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = idProject;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public bool DeleteProject(int idProject)
        {
            bool isDeleted = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE Project SET status = @status WHERE idProject = @idProject"
                };

                query.Parameters.Add("@status", MySqlDbType.Int32, 2).Value = NO_ACTIVE;
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = idProject;

                query.ExecuteNonQuery();
                isDeleted = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }
        public List<Project> GetProjectsByOrganization(int idOrganization)
        {
            projects = new List<Project>();
            developmentStageHandler = new DevelopmentStageDAO();
            linkedOrganizationHandler = new LinkedOrganizationDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM Project WHERE idLinkedOrganization = @idOrganization"
                };

                MySqlParameter idOrg = new MySqlParameter("@idOrganization", MySqlDbType.Int32, 32)
                {
                    Value = idOrganization
                };

                query.Parameters.Add(idOrg);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    project = new Project
                    {
                        IdProject = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        DirectUsersNumber = reader.GetString(2),
                        IndirectUsersNumber = reader.GetString(3),
                        Duration = reader.GetString(4),
                        GeneralGoal = reader.GetString(5),
                        Responsabilities = reader.GetString(6),
                        MediateGoals = reader.GetString(7),
                        InmediateGoals = reader.GetString(8),
                        Metology = reader.GetString(9),
                        Status = reader.GetInt32(10),
                        NeededResources = reader.GetString(11),
                        PractitionerNumber = reader.GetInt32(12),
                        GeneralDescription = reader.GetString(13),
                        ResponsableName = reader.GetString(14),
                        ResponsableCharge = reader.GetString(15),
                        ResponsableEmail = reader.GetString(16),
                        ResponsableTelephone = reader.GetString(17),
                        PractitionersAssigned = reader.GetInt32(18),
                        BelongsTo = developmentStageHandler.GetDevelopmentStageById(reader.GetInt32(19)),
                        ProposedBy = linkedOrganizationHandler.GetLinkedOrganizationById(reader.GetInt32(20)),
                        ProjectActivities = GetAllProjectActivities(reader.GetInt32(0))
                    };

                    projects.Add(project);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return projects;
        }

    }
}
