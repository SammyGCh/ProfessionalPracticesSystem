/*
    Date: 14/04/20
    Author(s): Sammy Guadarrama Chávez
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.DataBase;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccessTests
{
    [TestClass]
    public class DataBaseConnectionTest
    {
        private DataBaseConnection connection;

        [TestMethod]
        public void OpenConnection_ServerAvailable_SuccessConnection()
        {
            connection = new DataBaseConnection();
            MySqlConnection newConnection = connection.OpenConnection();
            Assert.IsNotNull(newConnection);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void OpenConnection_ServerUnavailable_MySqlException()
        {
            connection = new DataBaseConnection();
            connection.OpenConnection();
        }

        [TestMethod]
        public void CloseConnection_ConnectionOpened_SuccessDisconnection()
        {
            connection = new DataBaseConnection();
            MySqlConnection newConnection = connection.OpenConnection();
            connection.CloseConnection();
            Assert.IsTrue(newConnection.State == ConnectionState.Closed);
        }
    }
}
