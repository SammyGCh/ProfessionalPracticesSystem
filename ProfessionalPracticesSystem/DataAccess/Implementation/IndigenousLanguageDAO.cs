/*
        Date: 08/04/2020                               
            Author:Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using DataAccess.DataBase;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class IndigenousLanguageDAO : IIndigenousLanguageDAO
    {
        private List <IndigenousLanguage> indigenousLanguages;
        private IndigenousLanguage indigenousLanguage;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IndigenousLanguageDAOImp()
        {
            this.indigenousLanguages = null;
            this.indigenousLanguage = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<IndigenousLanguage> GetAllIndigenousLanguages()
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText ="SELECT * FROM IndigenousLanguge"
                };
                
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    indigenousLanguage = new IndigenousLanguage
                    {
                        IdIndigenousLanguage = reader.GetInt32(0),
                        indigenousLanguageName = reader.GetString(1)
                    };

                    indigenousLanguages.Add(indigenousLanguage);
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return indigenousLanguages;
        }

        public IndigenousLanguage GetLanguageById(int idIndigenousLanguage)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM IndigenousLanguge WHERE idIndigenousLanguage = @idIndigenousLanguage"
                };

                MySqlParameter idIndiLanguage = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32, 32)
                {
                    Value = idIndigenousLanguage
                };

                query.Parameters.Add(idIndiLanguage);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    indigenousLanguage = new IndigenousLanguage
                    {
                        IdIndigenousLanguage = reader.GetInt32(0),
                        IndigenousLanguageName = reader.GetString(1)
                    };
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex );
            }
            finally
            {
                connection.CloseConnection();
            }

            return indigenousLanguage;
        }
    }
}
