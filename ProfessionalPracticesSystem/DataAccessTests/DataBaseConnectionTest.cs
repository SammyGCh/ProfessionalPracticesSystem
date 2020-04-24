/*
    Date: 14/04/20
    Author(s): Sammy Guadarrama Chávez
 */

using System;
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
        public void SuccessConnectionTest()
        {
            connection = new DataBaseConnection();
            MySqlConnection newConnection = connection.OpenConnection();
            Assert.IsNotNull(newConnection);
        }

        [TestMethod]
        public void SuccessDisconnectionTest()
        {
            connection = new DataBaseConnection();
            MySqlConnection newConnection = connection.OpenConnection();
            connection.CloseConnection();
            Assert.IsTrue(newConnection.State == ConnectionState.Closed);
        }
    }
}
