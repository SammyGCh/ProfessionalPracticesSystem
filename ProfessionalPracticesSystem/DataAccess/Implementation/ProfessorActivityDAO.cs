/*
    Date: 09/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;

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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ProfessorActivityDAO()
        {
            connection = new DataBaseConnection();
        }
        public bool DeleteProfessorActivity(int idProfessorActivity)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM ProfessorActivity WHERE ProfessorActivity.idProfessorActivity = @idProfessorActivity"
                };
                MySqlParameter idActivity = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 2)
                {
                    Value = idProfessorActivity
                };

                query.Parameters.Add(idActivity);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/ProfessorActivityDAO/DeleteProfessorActivity:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<ProfessorActivity> GetAllActivity(int idAcademic)
        {
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
                        ValueActivity = reader.GetInt32(3),
                        PerformanceDate = reader.GetString(4),
                        GeneratedBy = belongsto.GetAcademic(reader.GetInt32(5))
                    };

                    professorActivityList.Add(professorActivity);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/ProfessorActivityDAO/GetAllActivity:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return professorActivityList;
        }

        public bool SaveProfessorActivity(ProfessorActivity professorActivity)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Academic(description, name, value, performanceDate, idAcademic)" +
                    " VALUES (@description, @name, @value, @performanceDate, @idAcademic)"
                };

                MySqlParameter description = new MySqlParameter("@description", MySqlDbType.VarChar, 1000)
                {
                    Value = professorActivity.Description
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 60)
                {
                    Value = professorActivity.Name
                };

                MySqlParameter value = new MySqlParameter("@value", MySqlDbType.Int32, 2)
                {
                    Value = professorActivity.ValueActivity
                };

                MySqlParameter performanceDate = new MySqlParameter("@performanceDate", MySqlDbType.DateTime, 10)
                {
                    Value = professorActivity.PerformanceDate
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = professorActivity.GeneratedBy.IdAcademic
                };


                query.Parameters.Add(description);
                query.Parameters.Add(name);
                query.Parameters.Add(value);
                query.Parameters.Add(performanceDate);
                query.Parameters.Add(idAcademic);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/ProfessorActivityDAO/SaveProfessorActivity:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }
    }
}