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
	public class MensualReportDAOImp : MensualReportDAO
	{
   	 	private List<MensualReport> mensualReports;
		private MensualReport MensualReport;
	  	private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
		private ProjectDaoImp derivedFrom;
		private PractisingDaoImp generatedBy;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    	public MensualReportDAOImp()
    	{
        	MensualReports = null ;
	    	MensualReport = null;
			connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
    	}

    	public bool InsertMensualReport(MensualReport newMensualReport)
    	{
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
                    Value = mensualReport.mensualReportName
                };

                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.LONGTEXT)
                {
                    Value = mensualReport.Description
                };

				MySqlParameter monthReportedDate = new MySqlParameter("@monthReportedDate", MySqlDbType.VarChar, 20)
                {
                    Value = mensualReport.monthReportedDate
                };

                MySqlParameter idproject = new MySqlParameter("@idProject", MySqlDbType.Int32, 20)
                {
                    Value = mensualReport.DerivedFrom.Idproject
                };

                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 10)
                {
                    Value = MensualReport.generatedBy.IdPractising
                };

                query.Parameters.Add(mensualReportName);
                query.Parameters.Add(description);
				query.Parameters.Add(monthReportedDate);
                query.Parameters.Add(idproject);
                query.Parameters.Add(idpractising);

                query.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
            finally
            {
                connection.CloseConnection();
            }
		}

    	public bool DeleteMensualReport(MensualReport MensualReport)
    	{
			try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
                MySqlParameter idMensualReport = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

                query.Parameters.Add(_idMensualReport);

                query.ExecuteNonQuery();
                return true;

            }
            catch(MySqlException ex)
            {
                return false;
            }
            finally
            {
                connection.CloseConnection();
            }
		}

		public bool UpdateMensualReport(MensualReport MensualReport)
    	{
			try
			{
				mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE MensualReport SET name = @mensualReportName, description = @description,"+
					"monthReportedDate = @monthReportedDate WHERE MensualReport.idMensualReport = @idMensualReport"
                };
				MySqlParameter mensualReportName = new MySqlParameter("@mensualReportName", MySqlDbType.VarChar, 60)
                {
                    Value = mensualReport.mensualReportName
                };
                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.LONGTEXT)
                {
                    Value = mensualReport.Description
                };
				MySqlParameter monthReportedDate = new MySqlParameter("@monthReportedDate", MySqlDbType.VarChar, 20)
                {
                    Value = mensualReport.monthReportedDate
                };
				MySqlParameter idMensualReport = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

				query.Parameters.Add(mensualReportName);
                query.Parameters.Add(description);
				query.Parameters.Add(monthReportedDate);
				query.ExecuteNonQuery();

                return true;
			}
			catch (MySqlException ex)
            {
                return false;
            }
            finally
            {
                connection.CloseConnection();
            }
		}

		public List<MensualReport> GetAllMensualReports()
    	{
			try
            {
                reportsList = null;
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
                        idMensualReport = reader.GetInt32(0),
						description = reader.GetString(1),
                        monthReportedDate = reader.GetString(2),
						mensualReportName = reader.GetString(3),
						generatedBy = generatedBy.GetPractising(reader.GetInt32(4))
                        derivedFrom = derivedFrom.GetProject(reader.GetInt32(5)),
                    };

                    reportsList.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return reportsList;
		}

		public MensualReport GetMensualReport(int idMensualReport)
    	{
			try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
				MySqlParameter idMensualReport = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

				query.Parameters.Add(idMensualReport);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new mensualReport
                    {
                        idMensualReport = reader.GetInt32(0),
						description = reader.GetString(1),
                        monthReportedDate = reader.GetString(2),
						mensualReportName = reader.GetString(3),
						generatedBy = generatedBy.GetPractising(reader.GetInt32(4))
                        derivedFrom = derivedFrom.GetProject(reader.GetInt32(3)),
                    };
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return  document;
		}
		public List<MensualReport> GetReportByPractising(int idPractising)
		{
			try
            {
                reportsList = null;
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idPractising = @idPractising"
                };

				MySqlParameter idPractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 2)
                {
                    Value = idPractising
                };

				query.Parameters.Add(idPractising);
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        idMensualReport = reader.GetInt32(0),
						description = reader.GetString(1),
                        monthReportedDate = reader.GetString(2),
						mensualReportName = reader.GetString(3),
						generatedBy = generatedBy.GetPractising(reader.GetInt32(4))
                        derivedFrom = derivedFrom.GetProject(reader.GetInt32(5)),
                    };

                    reportsList.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
               log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return reportsList;
		}
        List<Document> GetReportByProject(int idProject)
		{
			try
            {
                reportsList = null;
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idProject = @idProject"
                };

				MySqlParameter idProject = new MySqlParameter("@idProject", MySqlDbType.Int32, 2)
                {
                    Value = idProject
                };

				query.Parameters.Add(idProject);
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        idMensualReport = reader.GetInt32(0),
						description = reader.GetString(1),
                        monthReportedDate = reader.GetString(2),
						mensualReportName = reader.GetString(3),
						generatedBy = generatedBy.GetPractising(reader.GetInt32(4))
                        derivedFrom = derivedFrom.GetProject(reader.GetInt32(5)),
                    };

                    reportsList.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
               log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return reportsList;
		}
	}
}