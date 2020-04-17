/*
        Date: 10/04/2020                               
            Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using DataAccess.Interfaces;

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
        private ProjectDAO derivedFrom;
        private PractitionerDAO generatedBy;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MensualReportDAO()
        {
            mensualReports = null;
            mensualReport = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
        }

        public bool InsertMensualReport(MensualReport newMensualReport)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO MensualReport(name, description, monthReportedDate,idProject, idPractising)" +
                    "VALUES (@mensualReportName, @description, @monthReportedDate, @idProject, @idPractising)"
                };

                MySqlParameter mensualReportName = new MySqlParameter("@mensualReportName", MySqlDbType.VarChar, 60)
                {
                    Value = mensualReport.Name
                };

                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.LongText)
                {
                    Value = mensualReport.Description
                };

                MySqlParameter monthReportedDate = new MySqlParameter("@monthReportedDate", MySqlDbType.VarChar, 20)
                {
                    Value = mensualReport.MonthReportedDate
                };

                MySqlParameter idproject = new MySqlParameter("@idProject", MySqlDbType.Int32, 20)
                {
                    Value = mensualReport.DerivedFrom.IdProject
                };

                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 10)
                {
                    Value = mensualReport.GeneratedBy.IdPractitioner
                };

                query.Parameters.Add(mensualReportName);
                query.Parameters.Add(description);
                query.Parameters.Add(monthReportedDate);
                query.Parameters.Add(idproject);
                query.Parameters.Add(idpractising);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
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
                log.Error("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }

        public bool UpdateMensualReport(MensualReport mensualReport)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE MensualReport SET name = @mensualReportName, description = @description," +
                    "monthReportedDate = @monthReportedDate WHERE MensualReport.idMensualReport = @idMensualReport"
                };
                MySqlParameter mensualReportName = new MySqlParameter("@mensualReportName", MySqlDbType.VarChar, 60)
                {
                    Value = mensualReport.Name
                };
                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.LongText)
                {
                    Value = mensualReport.Description
                };
                MySqlParameter monthReportedDate = new MySqlParameter("@monthReportedDate", MySqlDbType.VarChar, 20)
                {
                    Value = mensualReport.MonthReportedDate
                };
                MySqlParameter idMensualReport = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = mensualReport.IdMensualReport
                };

                query.Parameters.Add(mensualReportName);
                query.Parameters.Add(description);
                query.Parameters.Add(monthReportedDate);
                query.ExecuteNonQuery();

                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public List<MensualReport> GetAllMensualReports()
        {
            generatedBy = new PractitionerDAO();
            derivedFrom = new ProjectDAO();

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
                        Name = reader.GetString(3),
                        GeneratedBy = generatedBy.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = derivedFrom.GetProjectById(reader.GetInt32(5))
                    };

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
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
						Name = reader.GetString(3),
						GeneratedBy = generatedBy.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = derivedFrom.GetProjectById(reader.GetInt32(3))
                    };
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReport;
		}
		public List<MensualReport> GetReportByPractitioner(int idPractitioner)
		{
            mensualReports = new List<MensualReport>();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idPractising = @idPractising"
                };

				MySqlParameter id = new MySqlParameter("@idPractising", MySqlDbType.Int32, 2)
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
						Name = reader.GetString(3),
						GeneratedBy = generatedBy.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = derivedFrom.GetProjectById(reader.GetInt32(5))
                    };

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
               log.Error("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
		}
        public List<MensualReport> GetReportByProject(int idProject)
		{
            mensualReports = new List<MensualReport>();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idProject = @idProject"
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
						Name = reader.GetString(3),
						GeneratedBy = generatedBy.GetPractitioner(reader.GetInt32(4)),
                        DerivedFrom = derivedFrom.GetProjectById(reader.GetInt32(5))
                    };

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
               log.Error("Someting whent wrong in DataAccess/Implementation/MensualReport", ex);
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