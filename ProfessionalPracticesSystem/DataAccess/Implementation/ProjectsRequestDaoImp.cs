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
    public class ProjectsRequestDaoImp : IProjectsRequestDao
    {
        private List<ProjectsRequest> projectsRequests;
        private ProjectsRequest projectsRequest;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProjectsRequestDaoImp()
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
            ProjectDaoImp projectsHandler = new ProjectDaoImp();
            PractisingDaoImp practisingHandler = new PractisingDaoImp();

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

                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProject(reader.GetInt32(3)));
                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProject(reader.GetInt32(4)));
                    projectsRequest.ProjectsRequested.Add(projectsHandler.GetProject(reader.GetInt32(5)));
                    projectsRequest.RequestedBy = practisingHandler.GetPractising(reader.GetInt32(6));

                    projectsRequests.Add(projectsRequest);
                }
            }
            catch(MySqlException ex)
            {
                log.Error("Something went wrong!: ", ex);
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

                query.Parameters.Add("@status", MySqlDbType.VarChar, 10).Value = projectsRequest.Status;
                query.Parameters.Add("@idProjectRequested1", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[0].IdProject;
                query.Parameters.Add("@idProjectRequested2", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[1].IdProject;
                query.Parameters.Add("@idProjectRequested3", MySqlDbType.Int32, 2).Value = projectsRequest.ProjectsRequested[2].IdProject;
                query.Parameters.Add("@idPractising", MySqlDbType.Int32, 2).Value = projectsRequest.RequestedBy.IdPractising;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Something went wrong!: ", ex);
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
                log.Error("Something went wrong!: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }
    }
}
