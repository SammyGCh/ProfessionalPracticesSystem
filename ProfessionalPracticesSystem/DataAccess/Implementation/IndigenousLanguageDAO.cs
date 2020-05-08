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

        public IndigenousLanguageDAO()
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
                indigenousLanguages = new List<IndigenousLanguage>();
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
                        IndigenousLanguageName = reader.GetString(1)
                    };

                    indigenousLanguages.Add(indigenousLanguage);
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/IndigenousLanguageDAO ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return indigenousLanguages;
        }

        public IndigenousLanguage GetIndigenousLanguageById(int idIndigenousLanguage)
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
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/IndigenousLanguageDAO", ex );
            }
            finally
            {
                connection.CloseConnection();
            }

            return indigenousLanguage;
        }

        public bool InsertIndigenousLanguage(IndigenousLanguage indigenousLanguage)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO IndigenousLanguge(name) VALUES (@indigenousLanguageName)"
                };

                query.Parameters.Add("@indigenousLanguageName", MySqlDbType.VarChar, 255).Value = indigenousLanguage.IndigenousLanguageName;
                query.ExecuteNonQuery();

                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/IndigenousLanguageDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool DeleteIndigenousLanguageById(int idIndigenousLanguage)
        {
            bool isDeleted = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "DELETE FROM IndigenousLanguge WHERE IndigenousLanguge.idIndigenousLanguage = @idIndigenousLanguage"
                };
                MySqlParameter id = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32, 2)
                {
                    Value = idIndigenousLanguage
                };
                query.Parameters.Add(id);
                query.ExecuteNonQuery();

                isDeleted = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/IndigenousLanguageDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }
    }
}
