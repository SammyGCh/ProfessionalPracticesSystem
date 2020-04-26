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
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private const int ACTIVE = 1;

        public ProjectsRequestDAO()
        {
            projectsRequests = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<ProjectsRequest> GetAllProjectsRequest()
        {
            projectsRequests = new List<ProjectsRequest>();
            ProjectDAO projectsHandler = new ProjectDAO();
            PractitionerDAO practisingHandler = new PractitionerDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection) 
                {
                    CommandText = "SELECT * FROM ProjectsRequest"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    projectsRequest = new ProjectsRequest
                    {
                        IdProjectsRequest = reader.GetInt32(0),
                        Status = reader.GetString(1),
                        Date = reader.GetString(2)
                    };

                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProjectById(reader.GetInt32(3)));
                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProjectById(reader.GetInt32(4)));
                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProjectById(reader.GetInt32(5)));
                    projectsRequest.RequestedBy = practisingHandler.GetPractitioner(reader.GetInt32(6));

                    projectsRequests.Add(projectsRequest);
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/ProjectsRequestDAO: ", ex);
            }
            finally
            {
                reader.Close();
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
                    CommandText = "INSERT INTO ProjectsRequest (status, idProjectRequested1, idProjectRequested2, idProjectRequest3, " +
                    "idPractising) VALUES (@status, @idProjectRequested1, @idProjectRequested2, @idProjectRequested3, @idPractising)"
                };

                query.Parameters.Add("@status", MySqlDbType.VarChar, 10).Value = ACTIVE;
                query.Parameters.Add("@idProjectRequested1", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[0].IdProject;
                query.Parameters.Add("@idProjectRequested2", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[1].IdProject;
                query.Parameters.Add("@idProjectRequested3", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[2].IdProject;
                query.Parameters.Add("@idPractising", MySqlDbType.Int32, 2).Value = projectsRequest.RequestedBy.IdPractitioner;

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

        public bool UpdateProjectsRequest(int idProjectsRequest)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE ProjectsRequest SET status = @status WHERE idProjectsRequest = @idProjectsRequest"
                };

                query.Parameters.Add("@status", MySqlDbType.VarChar, 10).Value = "NO ACTIVO";
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
    }
}
