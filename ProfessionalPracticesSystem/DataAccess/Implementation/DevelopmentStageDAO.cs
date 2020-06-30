/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using BusinessDomain;
using DataAccess.DataBase;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class DevelopmentStageDAO : IDevelopmentStageDAO
    {
        private List<DevelopmentStage> developmentStages;
        private DevelopmentStage developmentStage;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public DevelopmentStageDAO()
        {
            developmentStages = null;
            developmentStage = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<DevelopmentStage> GetAllDevelopmentStages()
        {
            developmentStages = new List<DevelopmentStage>();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM DevelopmentStage"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    developmentStage = new DevelopmentStage
                    {
                        IdDevelopmentStage = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };

                    developmentStages.Add(developmentStage);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/DevelopmentStageDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                
                connection.CloseConnection();
            }

            return developmentStages;
        }

        public DevelopmentStage GetDevelopmentStageById(int idDevelopmentStage)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM DevelopmentStage WHERE idDevelopmentStage = @idDevelopmentStage"
                };
                MySqlParameter idDevStage = new MySqlParameter("@idDevelopmentStage", MySqlDbType.Int32, 32)
                {
                    Value = idDevelopmentStage
                };

                query.Parameters.Add(idDevStage);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    developmentStage = new DevelopmentStage
                    {
                        IdDevelopmentStage = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/DevelopmentStageDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                connection.CloseConnection();
            }

            return developmentStage;
        }
    }
}
