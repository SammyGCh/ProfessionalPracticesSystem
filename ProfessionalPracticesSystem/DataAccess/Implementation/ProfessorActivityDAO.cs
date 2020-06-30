/*
    Date: 09/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Implementation
{
    public class ProfessorActivityDAO : IProfessorActivityDAO
    {
        private List<ProfessorActivity> professorActivityList;
        private ProfessorActivity professorActivity;
        private DataBaseConnection connection;
        private AcademicDAO belongsto;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private int NO_ACTIVE = 0;

        public ProfessorActivityDAO()
        {
            professorActivityList = null;
            professorActivity = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
        }
        public bool DeleteProfessorActivity(int idProfessorActivity)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE professoractivity SET status = @status WHERE professoractivity.idProfessorActivity = @idProfessorActivity"
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = NO_ACTIVE
                };

                MySqlParameter idActivity = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 11)
                {
                    Value = idProfessorActivity
                };

                query.Parameters.Add(status);
                query.Parameters.Add(idActivity);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/DeleteProfessorActivity:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<ProfessorActivity> GetAllProfessorActivityByAcademic(int idAcademic)
        {
            belongsto = new AcademicDAO();
            try
            {
                professorActivityList = new List<ProfessorActivity>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ProfessorActivity WHERE ProfessorActivity.idAcademic = @idAcademic"
                };

                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = idAcademic
                };

                query.Parameters.Add(idacademic);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    professorActivity = new ProfessorActivity
                    {
                        IdProfessorActivity = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Name = reader.GetString(2),
                        PerformanceDate = reader.GetString(3),
                        GeneratedBy = belongsto.GetAcademic(reader.GetInt32(4)),
                        Observations = reader.GetString(5),
                        Status = reader.GetInt32(6)
                    };

                    professorActivityList.Add(professorActivity);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/GetAllProfessorActivityByAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return professorActivityList;
        }

        public ProfessorActivity GetProfessorActivity(int idProfessorActivity)
        {
            belongsto = new AcademicDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ProfessorActivity WHERE ProfessorActivity.idProfessorActivity = @idProfessorActivity"
                };

                MySqlParameter idprofesoractivity = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 2)
                {
                    Value = idProfessorActivity
                };

                query.Parameters.Add(idprofesoractivity);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    professorActivity = new ProfessorActivity
                    {
                        IdProfessorActivity = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Name = reader.GetString(2),
                        PerformanceDate = reader.GetString(3),
                        GeneratedBy = belongsto.GetAcademic(reader.GetInt32(4)),
                        Observations = reader.GetString(5),
                        Status = reader.GetInt32(6)
                    };
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/GetProfessorActivity:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return professorActivity;
        }

        public bool SaveProfessorActivity(ProfessorActivity professorActivity)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO professorActivity(description, name, performanceDate, idAcademic, observations, status)" +
                    " VALUES (@description, @name, @performanceDate, @idAcademic, @observations, @status)"
                };

                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.VarChar, 1000)
                {
                    Value = professorActivity.Description
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 60)
                {
                    Value = professorActivity.Name
                };

                MySqlParameter performanceDate = new MySqlParameter("@performanceDate", MySqlDbType.DateTime, 50)
                {
                    Value = DateTime.Parse(professorActivity.PerformanceDate)
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = professorActivity.GeneratedBy.IdAcademic
                };

                MySqlParameter observations = new MySqlParameter("@observations", MySqlDbType.VarChar, 200)
                {
                    Value = professorActivity.Observations
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = professorActivity.Status
                };


                query.Parameters.Add(description);
                query.Parameters.Add(name);
                query.Parameters.Add(performanceDate);
                query.Parameters.Add(idAcademic);
                query.Parameters.Add(observations);
                query.Parameters.Add(status);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/SaveProfessorActivity:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<ProfessorActivity> GetAllProfessorActivity()
        {
            belongsto = new AcademicDAO();
            try
            {
                professorActivityList = new List<ProfessorActivity>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ProfessorActivity"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    professorActivity = new ProfessorActivity()
                    {
                        IdProfessorActivity = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        Name = reader.GetString(2),
                        PerformanceDate = reader.GetString(3),
                        GeneratedBy = belongsto.GetAcademic(reader.GetInt32(4)),
                        Observations = reader.GetString(5),
                        Status = reader.GetInt32(6)
                    };

                    professorActivityList.Add(professorActivity);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/GetAllProfessorActivity:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return professorActivityList;
        }

        public bool UpdateProfessorActivity(ProfessorActivity professorActivity)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE professoractivity SET description = @description, name = @name, observations = @observations," +
                    " performanceDate = @performanceDate WHERE idProfessorActivity = @idProfessorActivity"
                };

                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.String, 1000)
                {
                    Value = professorActivity.Description
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 60)
                {
                    Value = professorActivity.Name
                };

                MySqlParameter observations = new MySqlParameter("@observations", MySqlDbType.VarChar, 200)
                {
                    Value = professorActivity.Observations
                };

                MySqlParameter performanceDate = new MySqlParameter("@performanceDate", MySqlDbType.DateTime, 50)
                {
                    Value = DateTime.Parse(professorActivity.PerformanceDate)
                };

                MySqlParameter idProfessorActivity = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 11)
                {
                    Value = professorActivity.IdProfessorActivity
                };


                query.Parameters.Add(description);
                query.Parameters.Add(name);
                query.Parameters.Add(observations);
                query.Parameters.Add(performanceDate);
                query.Parameters.Add(idProfessorActivity);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/ProfessorActivityDAO/UpdateProfessorActivity:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }
    }
}