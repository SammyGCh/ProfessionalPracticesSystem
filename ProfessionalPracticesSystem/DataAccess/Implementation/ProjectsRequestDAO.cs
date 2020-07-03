/*
    Date: 09/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using BusinessDomain;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class ProjectsRequestDAO : IProjectsRequestDAO
    {
        private List<ProjectsRequest> projectsRequests;
        private ProjectsRequest projectsRequest;
        private readonly DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private const int ACTIVE = 1;
        private const int NO_ACTIVE = 0;
        private const int DOESNT_EXIST = 0;

        public ProjectsRequestDAO()
        {
            projectsRequests = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<ProjectsRequest> GetAllProjectsRequestActive()
        {
            projectsRequests = new List<ProjectsRequest>();
            ProjectDAO projectsHandler = new ProjectDAO();
            PractitionerDAO practisingHandler = new PractitionerDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection) 
                {
                    CommandText = "SELECT * FROM ProjectsRequest WHERE status = @status"
                };

                query.Parameters.Add("@status", MySqlDbType.VarChar, 10).Value = ACTIVE;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    projectsRequest = new ProjectsRequest
                    {
                        IdProjectsRequest = reader.GetInt32(0),
                        Status = reader.GetInt32(1),
                        Date = reader.GetString(2)
                    };

                    projectsRequest.ProjectsRequested = new List<Project>
                    {
                        projectsHandler.GetProjectRequestInfo(reader.GetInt32(3)),
                        projectsHandler.GetProjectRequestInfo(reader.GetInt32(4)),
                        projectsHandler.GetProjectRequestInfo(reader.GetInt32(5))
                    };
                    projectsRequest.RequestedBy = practisingHandler.GetPractitionerPersonalInfo(reader.GetInt32(6));

                    projectsRequests.Add(projectsRequest);
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectsRequestDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                connection.CloseConnection();
            }

            return projectsRequests;
        }

        public bool SaveProjectsRequest(ProjectsRequest projectsRequest)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO ProjectsRequest (status, ProjectRequested1, ProjectRequested2, ProjectRequest3, " +
                    "idPractitioner) VALUES (@status, @idProjectRequested1, @idProjectRequested2, @idProjectRequested3, @idPractitioner)"
                };

                query.Parameters.Add("@status", MySqlDbType.VarChar, 10).Value = ACTIVE;
                query.Parameters.Add("@idProjectRequested1", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[0].IdProject;
                query.Parameters.Add("@idProjectRequested2", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[1].IdProject;
                query.Parameters.Add("@idProjectRequested3", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[2].IdProject;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = projectsRequest.RequestedBy.IdPractitioner;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implemation/ProjectsRequestDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool UpdateProjectsRequestStatus(int idProjectsRequest)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE ProjectsRequest SET status = @status WHERE idProjectsRequest = @idProjectsRequest"
                };

                query.Parameters.Add("@status", MySqlDbType.Int32, 2).Value = NO_ACTIVE;
                query.Parameters.Add("@idProjectsRequest", MySqlDbType.Int32, 2).Value = idProjectsRequest;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectsRequestDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public int ExistsProjectsRequestFromPractitioner(string practitionerMatricula)
        {
            int exists = DOESNT_EXIST;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT EXISTS (SELECT ProjectsRequest.idPractitioner, " +
                    "Practitioner.status, Practitioner.matricula, Practitioner.idPractitioner " +
                    "FROM Projectsrequest, Practitioner " +
                    "WHERE ProjectsRequest.idPractitioner = Practitioner.idPractitioner AND " +
                    "Practitioner.status = 1 AND Practitioner.matricula = @matricula)"
                };

                query.Parameters.Add("@matricula", MySqlDbType.VarChar, 9).Value = practitionerMatricula;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    exists = reader.GetInt32(0);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectsRequestDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                connection.CloseConnection();
            }

            return exists;
        }
    }
}
