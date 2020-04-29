/*
        Date: 08/04/2020                               
            Author:Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using DataAccess.DataBase;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Implementation
{
    public class ActivityPerformedDAO : IActivityPerformedDAO
    {
        private List<ActivityPerformed> activitiesPerformed;
        private ActivityPerformed activityPerformed;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
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
            mySqlConnection = null;
            query = null;
            reader = null;
            practitionerHandler = null;
            professorActivityHandler = null;
    }

        public List<ActivityPerformed> GetAllActivityPerformedByProfessorActivity(int idProfessorActivity)
        {
            activitiesPerformed = new List<ActivityPerformed>();
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed WHERE idProfessorActivity = @idProfessorActivity"
                };

                query.Parameters.Add("@idProfessorActivity", MySqlDbType.Int32, 2).Value = idProfessorActivity;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        GeneratedBy = professorActivityHandler.GetProfessorActivity(reader.GetInt32(0)),
                        PerformedBy = practitionerHandler.GetPractitioner(reader.GetInt32(1)),
                        PerformedDate = reader.GetString(2),
                        ActivityReply = reader.GetString(3),
                        Observations = reader.GetString(4)
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

        public List<ActivityPerformed> GetAllActivitiesyPerformedByPractitioner(int idPractitioner)
        {
            activitiesPerformed = new List<ActivityPerformed>();
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed WHERE idPractitioner = @idPractitioner"
                };

                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = idPractitioner;

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        GeneratedBy = professorActivityHandler.GetProfessorActivity(reader.GetInt32(0)),
                        PerformedBy = practitionerHandler.GetPractitioner(reader.GetInt32(1)),
                        PerformedDate = reader.GetString(2),
                        ActivityReply = reader.GetString(3),
                        Observations = reader.GetString(4)
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

        public ActivityPerformed GetActivityPerformed(int idProfessorActivity, int idPractitioner)
        {
            practitionerHandler = new PractitionerDAO();
            professorActivityHandler = new ProfessorActivityDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM ActivityPerformed WHERE idProfessorActivity = @idProfessorActivity " +
                    "AND idPractitioner = @idPractitioner"
                };

                MySqlParameter idProfesorA = new MySqlParameter("@idProfessorActivity", MySqlDbType.Int32, 2)
                {
                    Value = idProfessorActivity
                };
                MySqlParameter idPractitionerA = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(idProfesorA);
                query.Parameters.Add(idPractitionerA);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    activityPerformed = new ActivityPerformed
                    {
                        GeneratedBy = professorActivityHandler.GetProfessorActivity(reader.GetInt32(0)),
                        PerformedBy = practitionerHandler.GetPractitioner(reader.GetInt32(1)),
                        PerformedDate = reader.GetString(2),
                        ActivityReply = reader.GetString(3),
                        Observations = reader.GetString(4)
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

        public bool NewActivityPerformed(ActivityPerformed activityPerformed)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = " INSERT INTO ActivityPerformed (idProfessorActivity, idPractitioner,performedDate, activityReply)"+
                            "VALUES(@idProfessorActivity, @idPractitioner,@performedDate, @activityReply)"
                };

                query.Parameters.Add("@idProfessorActivity", MySqlDbType.Int32, 2).Value = activityPerformed.GeneratedBy.IdProfessorActivity;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = activityPerformed.PerformedBy.IdPractitioner;
                query.Parameters.Add("@performedDate", MySqlDbType.DateTime, 20).Value = DateTime.Parse(activityPerformed.PerformedDate);
                query.Parameters.Add("@activityReply", MySqlDbType.VarChar, 255).Value = activityPerformed.ActivityReply;


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

        public bool UpdateActivityPerformed(ActivityPerformed activityPerformed)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE ActivityPerformed SET performedDate = @performedDate, activityReply = @activityReply" +
                    " WHERE idProfessorActivity = @idProfessorActivity AND idPractitioner = @idPractitioner"
                };

                query.Parameters.Add("@idProfessorActivity", MySqlDbType.Int32, 2).Value = activityPerformed.GeneratedBy.IdProfessorActivity;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = activityPerformed.PerformedBy.IdPractitioner;
                query.Parameters.Add("@performedDate", MySqlDbType.DateTime, 20).Value = DateTime.Parse(activityPerformed.PerformedDate);
                query.Parameters.Add("@activityReply", MySqlDbType.VarChar, 255).Value = activityPerformed.ActivityReply;

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

        public bool AddObservationsActivityPerformed(ActivityPerformed activityPerformed)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE ActivityPerformed SET observations = @observations" +
                    " WHERE idProfessorActivity = @idProfessorActivity AND idPractitioner = @idPractitioner"
                };

                query.Parameters.Add("@idProfessorActivity", MySqlDbType.Int32, 2).Value = activityPerformed.GeneratedBy.IdProfessorActivity;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = activityPerformed.PerformedBy.IdPractitioner;
                query.Parameters.Add("@observations", MySqlDbType.VarChar, 255).Value = activityPerformed.Observations;

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
