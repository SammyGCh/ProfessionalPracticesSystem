/*
        Date: 02/07/2020                              
        Author:Ricardo Moguel Sanchez
 */
using BusinessDomain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Implementation
{
    public class NoticeDAO : INoticeDAO
    {
        private List<Notice> notices;
        private Notice notice;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader noticeReader;
        private AcademicDAO academicHandler;

        public NoticeDAO()
        {
            notices = null;
            notice = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            noticeReader = null;
            academicHandler = new AcademicDAO();
        }

        public bool SaveNotice(Notice newNotice)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Notice(title,body,date,idAcademic)" +
                    "VALUES (@title, @body,@date, @idAcademic)"
                };

                query.Parameters.Add("@title", MySqlDbType.VarChar, 45).Value = newNotice.Title;
                query.Parameters.Add("@body", MySqlDbType.VarChar, 255).Value = newNotice.Body;
                query.Parameters.Add("@date", MySqlDbType.DateTime, 20).Value = DateTime.Parse(newNotice.CreationDate);
                query.Parameters.Add("@idAcademic", MySqlDbType.Int32, 2).Value = newNotice.CreatedBy.IdAcademic;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException mySQLException)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/NoticeDAO", mySQLException);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool UpdateNotice(Notice updatedNotice)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Notice SET title = @title, body = @body, " +
                    "date = @date, idAcademic = @idAcademic WHERE Notice.idNotice = @idNotice"
                };

                query.Parameters.Add("@title", MySqlDbType.VarChar, 45).Value = updatedNotice.Title;
                query.Parameters.Add("@body", MySqlDbType.VarChar, 255).Value = updatedNotice.Body;
                query.Parameters.Add("@date", MySqlDbType.DateTime, 20).Value = DateTime.Parse(updatedNotice.CreationDate);
                query.Parameters.Add("@idAcademic", MySqlDbType.Int32, 2).Value = updatedNotice.CreatedBy.IdAcademic;
                query.Parameters.Add("@idNotice", MySqlDbType.Int32, 2).Value = updatedNotice.IdNotice;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException mySQLException)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/NoticeDAO", mySQLException);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public List<Notice> GetAllNotices()
        {
            notices = new List<Notice>();
            academicHandler = new AcademicDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Notice"
                };

                noticeReader = query.ExecuteReader();

                while (noticeReader.Read())
                {
                    notice = new Notice
                    {
                        IdNotice = noticeReader.GetInt32(0),
                        Title = noticeReader.GetString(1),
                        Body = noticeReader.GetString(2),
                        CreationDate = noticeReader.GetString(3),
                        CreatedBy = academicHandler.GetAcademic(noticeReader.GetInt32(4)),
                    };

                    notices.Add(notice);
                }

            }
            catch (MySqlException mySQLException)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/NoticeDAO", mySQLException);
            }
            finally
            {
                if (noticeReader != null)
                {
                    noticeReader.Close();
                }

                connection.CloseConnection();
            }

            return notices;
        }
    }
}
