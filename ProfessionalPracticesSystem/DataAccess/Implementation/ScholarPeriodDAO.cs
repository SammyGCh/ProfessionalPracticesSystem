/*
    Date: 16/04/2020
    Author(s) : Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using DataAccess.Interfaces;
using DataAccess.DataBase;
using BusinessDomain;
using MySql.Data.MySqlClient;

namespace DataAccess.Implementation
{
    public class ScholarPeriodDAO : IScholarPeriodDAO
    {
        private List<ScholarPeriod> scholarPeriods;
        private ScholarPeriod scholarPeriod;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScholarPeriodDAO()
        {
            scholarPeriods = null;
            scholarPeriod = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
        }

        public bool SaveScholarPeriod(ScholarPeriod scholarPeriod)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO ScholarPeriod (name) VALUES (@name)"
                };

                query.Parameters.Add("@name", MySqlDbType.VarChar, 45).Value = scholarPeriod.Name;

                query.ExecuteReader();
                isSaved = true;
            }
            catch(MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/ScholarPeriodDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<ScholarPeriod> GetAllScholarPeriods()
        {
            scholarPeriods = new List<ScholarPeriod>();

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ScholarPeriod"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    scholarPeriod = new ScholarPeriod
                    {
                        IdScholarPeriod = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };

                    scholarPeriods.Add(scholarPeriod);
                }
            }
            catch(MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/ScholarPeriodDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return scholarPeriods;
        }

        public ScholarPeriod GetScholarPeriodById(int idScholarPeriod)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ScholarPeriod WHERE idScholarPeriod = @idScholarPeriod"
                };

                query.Parameters.Add("@idScholarPeriod", MySqlDbType.Int32, 2).Value = idScholarPeriod;

                reader = query.ExecuteReader();

                while(reader.Read())
                {
                    scholarPeriod = new ScholarPeriod
                    {
                        IdScholarPeriod = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            catch(MySqlException ex)
            {
                log.Error("Something went wrong in DataAccess/Implementation/ScholarPeriodDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return scholarPeriod;
        }
    }
}