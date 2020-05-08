/*
        Date: 10/04/2020                               
            Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Implementation
{
    public class MensualReportDAO : IMensualReportDAO
    {
        private List<MensualReport> mensualReports;
        private MensualReport mensualReport;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private ProjectDAO projectHandler;
        private PractitionerDAO practitionerHandler;

        public MensualReportDAO()
        {
            mensualReports = null;
            mensualReport = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
            projectHandler = new ProjectDAO();
            practitionerHandler = new PractitionerDAO();
    }

        public bool InsertMensualReport(MensualReport mensualReport)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO MensualReport(description, monthReportedDate,name,idProject,idPractitioner)" +
                    "VALUES (@description, @monthReportedDate,@mensualReportName, @idProject, @idPractitioner)"
                };

                query.Parameters.Add("@description", MySqlDbType.LongText, 2000).Value = mensualReport.Description;
                query.Parameters.Add("@mensualReportName", MySqlDbType.VarChar, 60).Value = mensualReport.MensualReportName;
                query.Parameters.Add("@monthReportedDate", MySqlDbType.VarChar, 20).Value = DateTime.Parse(mensualReport.MonthReportedDate);
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = mensualReport.DerivedFrom.IdProject;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = mensualReport.GeneratedBy.IdPractitioner;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }
        public bool DeleteMensualReport(int idMensualReport)
        {
            bool isDeleted = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
                MySqlParameter id = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

                query.Parameters.Add(id);

                query.ExecuteNonQuery();
                isDeleted = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }
        public bool UpdateMensualReport(MensualReport mensualReportUpdate)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE MensualReport SET description = @description, monthReportedDate = @monthReportedDate, " +  
                    "name = @mensualReportName, idProject = @idProject, idPractitioner = @idPractitioner WHERE idMensualReport = @idMensualReport"
                };
                
                query.Parameters.Add("@description", MySqlDbType.LongText, 2000).Value = mensualReportUpdate.Description;
                query.Parameters.Add("@mensualReportName", MySqlDbType.VarChar, 60).Value = mensualReportUpdate.MensualReportName;
                query.Parameters.Add("@monthReportedDate", MySqlDbType.VarChar, 20).Value = DateTime.Parse(mensualReportUpdate.MonthReportedDate);
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = mensualReportUpdate.DerivedFrom.IdProject;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = mensualReportUpdate.GeneratedBy.IdPractitioner;
                query.Parameters.Add("@idMensualReport", MySqlDbType.Int32, 2).Value = mensualReportUpdate.IdMensualReport;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public List<MensualReport> GetAllMensualReports()
        {
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(5))
                    };

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
        }
        public MensualReport GetMensualReportById(int idMensualReport)
    	{
			try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
				MySqlParameter id = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

				query.Parameters.Add(id);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
						Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5))
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReport;
		}
		public List<MensualReport> GetAllReportsByPractitioner(int idPractitioner)
		{
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE idPractitioner = @idPractitioner"
                };

				MySqlParameter id = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

				query.Parameters.Add(id);
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
						Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(5))
                    };

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
		}
        public List<MensualReport> GetAllReportsByProject(int idProject)
		{
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE idProject = @idProject"
                };

				MySqlParameter id = new MySqlParameter("@idProject", MySqlDbType.Int32, 2)
                {
                    Value = idProject
                };

				query.Parameters.Add(id);
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(5))
                    };
                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
		}
	}
}