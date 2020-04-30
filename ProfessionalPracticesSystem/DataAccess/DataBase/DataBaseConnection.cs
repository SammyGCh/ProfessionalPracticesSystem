/*
    Date: 04/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess.DataBase
{
    public class DataBaseConnection
    {
        private string infoConnection;
        private MySqlConnection connection;

        private void Connect()
        {
            try
            {
                infoConnection = ConfigurationManager.ConnectionStrings["connectionSetting"].ConnectionString;
                connection = new MySqlConnection(infoConnection);
                connection.Open();
            }
            catch (MySqlException ex)
            {
                throw;
            }
        }

        public MySqlConnection OpenConnection()
        {
            try
            {
                Connect();
            }
            catch (MySqlException ex)
            {
                throw;
            }

            return connection;
        }

        public void CloseConnection()
        {
            if (connection != null)
            {
                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
                catch (MySqlException ex)
                {
                    throw;
                }
            }
        }
    }
}
