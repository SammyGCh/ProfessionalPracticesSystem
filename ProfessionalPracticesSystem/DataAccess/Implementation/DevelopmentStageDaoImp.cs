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
    public class DevelopmentStageDaoImp : IDevelopmentStageDao
    {
        private List<DevelopmentStage> developmentStages;
        private DevelopmentStage developmentStage;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DevelopmentStageDaoImp()
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
                        Name = reader.GetString(1)
                    };

                    developmentStages.Add(developmentStage);
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return developmentStages;
        }

        public DevelopmentStage GetDevelopmentStage(int idDevelopmentStage)
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
                        Name = reader.GetString(1)
                    };
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return developmentStage;
        }
    }
}
