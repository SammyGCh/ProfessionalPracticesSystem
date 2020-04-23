﻿/*
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
    public class ActivityPerformedDAO : IActivityPerformedDAO
    {
        private List<ActivityPerformed> activitiesPerformed;
        private ActivityPerformed activityPerformed;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private PractitionerDAO practitionerHandler;
        private ProfessorActivityDAO professorActivityHandler;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActivityPerformedDAO()
        {
            activitiesPerformed = null;
            activityPerformed = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
            practitionerHandler = null;
            professorActivityHandler = null;
    }

        public List<ActivityPerformed> GetAllActivityPerformed()
        {
            activitiesPerformed = new List<ActivityPerformed>();
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        IdActivityPerformed = reader.GetInt32(0),
                        GeneratedBy = reader.GetInt32(1),
                        DidBy = reader.GetInt32(2),
                        PerformedDate = reader.GetDateTime(3),
                        ActivityReply = reader.GetString(4),
                    };

                activitiesPerformed.Add(activityPerformed);
            }
        }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAcces/Implementation/ActivityPerformed ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return activitiesPerformed;
        }

        public List<ActivityPerformed> GetActivityByPractitioner(int idPractitioner)
        {
            activitiesPerformed = new List<ActivityPerformed>();
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed WHERE idPractitiober = @idPractitioner"
                };

                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = idPractitioner;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        IdActivityPerformed = reader.GetInt32(0),
                        GeneratedBy = reader.GetInt32(1),
                        DidBy = reader.GetInt32(2),
                        PerformedDate = reader.GetDateTime(3),
                        ActivityReply = reader.GetString(4),
                    };

                    activitiesPerformed.Add(activityPerformed);
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAcces/Implementation/ActivityPerformed ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return activitiesPerformed;
        }

        public ActivityPerformed GetActivityPerformed(int idActivityPerformed)
        {
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed WHERE idActivityPerformed = @idActivityPerformed"
                };

                query.Parameters.Add("@idActivityPerformed", MySqlDbType.Int32, 2).Value = idActivityPerformed;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        IdActivityPerformed = reader.GetInt32(0),
                        GeneratedBy = reader.GetInt32(1),
                        DidBy = reader.GetInt32(2),
                        PerformedDate = reader.GetDateTime(3),
                        ActivityReply = reader.GetString(4),
                    };
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAcces/Implementation/ActivityPerformed ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return activityPerformed;
        }

        public bool NewActivityPerformed(MensualReport MensualReport)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO ActivityPerformed (performedDate, activityReply, idProfessorActivity, idPractitioner)" +
                    " VALUES (@performedDate, @activityReply, @idProfessorActivity, @idPractitioner)"
                };

                MySqlParameter performedDate = new MySqlParameter("@performedDate", MySqlDbType.VarChar, 1000)
                {
                    Value = activityPerformed.PerformedDate
                };

                MySqlParameter activityReply = new MySqlParameter("@activityReply", MySqlDbType.VarChar, 60)
                {
                    Value = activityPerformed.ActivityReply
                };

                MySqlParameter idProfessorActivity = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 2)
                {
                    Value = activityPerformed.GeneratedBy.IdProfessorActivity
                };

                MySqlParameter idPractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.DateTime, 10)
                {
                    Value = activityPerformed.DidBy.IdPractitioner
                };

                query.Parameters.Add(performedDate);
                query.Parameters.Add(activityReply);
                query.Parameters.Add(idProfessorActivity);
                query.Parameters.Add(idPractitioner);
                query.ExecuteNonQuery();

                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAcces/Implementation/ActivityPerformed:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool UpdateActivityPerformed(MensualReport MensualReport)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDDATE ActivityPerformed SET performedDate = @performedDate, activityReply = @activityReply)" +
                    "WHERE idActivityPerformed = @idActivityPerformed"
                };

                MySqlParameter performedDate = new MySqlParameter("@performedDate", MySqlDbType.VarChar, 1000)
                {
                    Value = activityPerformed.PerformedDate
                };

                MySqlParameter activityReply = new MySqlParameter("@activityReply", MySqlDbType.VarChar, 60)
                {
                    Value = activityPerformed.ActivityReply
                };


                query.Parameters.Add(performedDate);
                query.Parameters.Add(activityReply);
                query.ExecuteNonQuery();

                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAcces/Implementation/ActivityPerformed:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }
    }
}
