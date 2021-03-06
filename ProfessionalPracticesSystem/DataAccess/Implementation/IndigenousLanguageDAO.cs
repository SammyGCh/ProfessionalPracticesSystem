/*
        Date: 08/04/2020                               
        Author: Cesar Sergio Martinez Palacios
 */

using System;
using BusinessDomain;
using DataAccess.DataBase;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class IndigenousLanguageDAO : IIndigenousLanguageDAO
    {
        private List<IndigenousLanguage> indigenousLanguages;
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
            indigenousLanguages = new List<IndigenousLanguage>();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM IndigenousLanguage"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    indigenousLanguage = new IndigenousLanguage
                    {
                        IdIndigenousLanguage = reader.GetInt32(0),
                        IndigenousLanguageName = reader.GetString(1),
                        Status = reader.GetInt32(2)
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
                    CommandText = "SELECT * FROM IndigenousLanguage WHERE idIndigenousLanguage = @idIndigenousLanguage"
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
                        IndigenousLanguageName = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/IndigenousLanguageDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return indigenousLanguage;
        }
    }
}
