/*
    Date: 04/04/2020
    Author(s) : Sammy Guadarrama Chávez
 */

using System;
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
                //La línea de abajo no funciona, no pasa las pruebas unitarias
                infoConnection = ConfigurationManager.ConnectionStrings["connectionSetting"].ConnectionString;

                //La línea de abajo SÍ funciona
                //infoConnection = "server=localhost; database=professionalpracticesdb; user = adminPPS; password = proyectoConstruccion2020";
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
