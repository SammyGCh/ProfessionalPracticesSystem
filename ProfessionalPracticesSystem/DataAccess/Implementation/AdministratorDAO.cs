/*
        Date: 05/06/2020                              
        Author: Cesar Sergio Martinez Palacios
 */

using System.Collections.Generic;
using BusinessDomain;
using DataAccess.DataBase;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class AdministratorDAO : IAdmninistratorDAO
    {
        private Administrator administrator;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public AdministratorDAO()
        {
            administrator = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;

        }

        public Administrator GetAdministratorByUser(string adminUsername)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM Administrator WHERE username = @adminUsername"
                };

                MySqlParameter adUsername = new MySqlParameter("@adminUsername", MySqlDbType.VarChar, 45)
                {
                    Value = adminUsername
                };

                query.Parameters.Add(adUsername);
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    administrator = new Administrator
                    {
                        IdAdministrator = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2)
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/AdministratorDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }
            return administrator;
        }

    }
}
