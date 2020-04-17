﻿/*
    Date: 09/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class AcademicDAO : IAcademicDAO
    {
        private List<Academic> academicList;
        private Academic academic;
        private DataBaseConnection connection;
        private AcademicTypeDAO belongsto;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AcademicDAO()
        {
            connection = new DataBaseConnection();
        }
        public bool DeleteAcademic(int idAcademic)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Academic SET status='No activo' WHERE Academic.idAcademic = @idAcademic"
                };
                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = idAcademic
                };

                query.Parameters.Add(idacademic);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/AcademicDAO/DeleteAcademic:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public Academic GetAcademic(int idAcademic)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.idAcademic = @idAcademic"
                };
                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = idAcademic
                };
                query.Parameters.Add(idacademic);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    academic = new Academic
                    {
                        IdAcademic = reader.GetInt32(0),
                        PersonalNumber = reader.GetString(1),
                        Names = reader.GetString(2),
                        Cubicle = reader.GetString(3),
                        LastName = reader.GetString(4),
                        Gender = reader.GetString(5),
                        Password = reader.GetString(6),
                        BelongTo = belongsto.GetAcademicTypeById(reader.GetInt32(7)),
                        Shift = reader.GetString(8),
                        Status = reader.GetInt32(9)
                    };
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/AcademicDAO/GetAcademic:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return academic;
        }

        public List<Academic> GetAllAcademic()
        {
            try
            {
                academicList = new List<Academic>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    academic = new Academic
                    {
                        IdAcademic = reader.GetInt32(0),
                        PersonalNumber = reader.GetString(1),
                        Names = reader.GetString(2),
                        Cubicle = reader.GetString(3),
                        LastName = reader.GetString(4),
                        Gender = reader.GetString(5),
                        Password = reader.GetString(6),
                        BelongTo = belongsto.GetAcademicTypeById(reader.GetInt32(7)),
                        Shift = reader.GetString(8),
                        Status = reader.GetInt32(9)
                    };

                    academicList.Add(academic);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/AcademicDAO/GetAllAcademic:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return academicList;
        }

        public bool SaveAcademic(Academic academic)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Academic(personalNumber, names, cubicle, lastName, gender, password, idAcademicType" +
                    "shift, status) VALUES (@personalNumber, @names, @cubicle, @lastName, @gender, @password, @idAcademicType, @shift, @status)"
                };

                MySqlParameter personalNumber = new MySqlParameter("@personalNumber", MySqlDbType.VarChar, 9)
                {
                    Value = academic.PersonalNumber
                };

                MySqlParameter names = new MySqlParameter("@names", MySqlDbType.VarChar, 60)
                {
                    Value = academic.Names
                };

                MySqlParameter cubicle = new MySqlParameter("@cubicle", MySqlDbType.VarChar, 2)
                {
                    Value = academic.Cubicle
                };

                MySqlParameter lastName = new MySqlParameter("@lastName", MySqlDbType.VarChar, 60)
                {
                    Value = academic.LastName
                };

                MySqlParameter gender = new MySqlParameter("@gender", MySqlDbType.VarChar, 10)
                {
                    Value = academic.Gender
                };

                MySqlParameter password = new MySqlParameter("@password", MySqlDbType.VarChar, 255)
                {
                    Value = academic.Password
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 2)
                {
                    Value = academic.BelongTo.IdAcademicType
                };

                MySqlParameter shift = new MySqlParameter("@shift", MySqlDbType.VarChar, 10)
                {
                    Value = academic.Shift
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.VarChar, 10)
                {
                    Value = academic.Status
                };


                query.Parameters.Add(personalNumber);
                query.Parameters.Add(names);
                query.Parameters.Add(cubicle);
                query.Parameters.Add(lastName);
                query.Parameters.Add(gender);
                query.Parameters.Add(password);
                query.Parameters.Add(idAcademic);
                query.Parameters.Add(shift);
                query.Parameters.Add(status);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/AcademicDAO/SaveAcademic:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }
    }
}