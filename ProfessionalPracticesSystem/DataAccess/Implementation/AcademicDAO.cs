﻿/*
    Date: 09/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using System;

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
        private int STATUS_NO_ACTIVE = 0;
        private int STATUS_ACTIVE = 1;
        private const int COORDINATOR_TYPE_ID = 1;
        private const int PROFESSOR_TYPE__ID = 2;
        private const int ACTIVE_COORDINATOR_LIMIT = 1;
        private const int ACTIVE_PROFESSOR_LIMIT = 2;

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
                    CommandText = "UPDATE Academic SET status=@status WHERE Academic.idAcademic = @idAcademic"
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = STATUS_NO_ACTIVE
                };

                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = idAcademic
                };

                query.Parameters.Add(status);
                query.Parameters.Add(idacademic);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/DeleteAcademic:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public Academic GetAcademic(int idAcademic)
        {
            belongsto = new AcademicTypeDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.idAcademic = @idAcademic"
                };
                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academic;
        }

        public Academic GetAcademicByPersonalNumber(String personalNumber)
        {
            belongsto = new AcademicTypeDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.personalNumber = @personalN"
                };
                MySqlParameter personalN = new MySqlParameter("@personalN", MySqlDbType.VarChar, 9)
                {
                    Value = personalNumber
                };
                query.Parameters.Add(personalN);

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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academic;
        }

        public List<Academic> GetAllAcademic()
        {
            belongsto = new AcademicTypeDAO();
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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAllAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academicList;
        }

        public List<Academic> GetAllActiveAcademic()
        {
            belongsto = new AcademicTypeDAO();
            try
            {
                academicList = new List<Academic>();
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.status = @status"
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(status);

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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAllAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academicList;
        }

        public List<Academic> GetAllActiveProfessors()
        {
            belongsto = new AcademicTypeDAO();

            try
            {
                academicList = new List<Academic>();
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.idAcademicType = @idAcademicType AND Academic.status = @status "
                };

                MySqlParameter academicType = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 2)
                {
                    Value = PROFESSOR_TYPE__ID
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(academicType);
                query.Parameters.Add(status);
                
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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAllAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academicList;
        }

        public Academic GetCoordinator()
        {
            belongsto = new AcademicTypeDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Academic WHERE Academic.idAcademicType = @idAcademicType AND Academic.status = @status"
                };

                MySqlParameter idAcademicType = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 11)
                {
                    Value = COORDINATOR_TYPE_ID
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };


                query.Parameters.Add(idAcademicType);
                query.Parameters.Add(status);

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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return academic;
        }

        public bool ActiveAcademicCountFull(int academicTypeID)
        {
            bool isFull = false;
            belongsto = new AcademicTypeDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT COUNT(Academic.idAcademic) FROM Academic WHERE Academic.idAcademicType = @idAcademicType AND Academic.status =@status  "
                };

                MySqlParameter academicType = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 2)
                {
                    Value = academicTypeID
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(academicType);
                query.Parameters.Add(status);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    int result = reader.GetInt32(0);

                    switch (academicTypeID)
                    {
                        case COORDINATOR_TYPE_ID:
                            if (result >= ACTIVE_COORDINATOR_LIMIT)
                            {
                                isFull = true;
                            }
                            break;
                        case PROFESSOR_TYPE__ID:
                            if (result >= ACTIVE_PROFESSOR_LIMIT)
                            {
                                isFull = true;
                            }
                            break;
                    }
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/GetAllAcademic:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return isFull;
        }

        public bool SaveAcademic(Academic academic)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Academic(personalNumber, names, cubicle, lastName, gender, password, idAcademicType," +
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

                MySqlParameter idAcademic = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 11)
                {
                    Value = academic.BelongTo.IdAcademicType
                };

                MySqlParameter shift = new MySqlParameter("@shift", MySqlDbType.VarChar, 10)
                {
                    Value = academic.Shift
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = academic.Status
                };

                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = academic.IdAcademic
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
                query.Parameters.Add(idacademic);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/AcademicDAO/SaveAcademic:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }

        public bool UpdateAcademic(Academic updatedAcademic)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Academic SET personalNumber = @personalNumber, names = @names, " + 
                    "cubicle = @cubicle, lastName = @lastName, gender = @gender, shift = @shift WHERE idAcademic = @idAcademic"
                };

                query.Parameters.Add("@personalNumber", MySqlDbType.VarChar, 9).Value = updatedAcademic.PersonalNumber;
                query.Parameters.Add("@names", MySqlDbType.VarChar, 60).Value = updatedAcademic.Names;
                query.Parameters.Add("@cubicle", MySqlDbType.VarChar, 2).Value = updatedAcademic.Cubicle;
                query.Parameters.Add("@lastName", MySqlDbType.VarChar, 60).Value = updatedAcademic.LastName;
                query.Parameters.Add("@gender", MySqlDbType.VarChar, 10).Value = updatedAcademic.Gender;
                query.Parameters.Add("@shift", MySqlDbType.VarChar, 10).Value = updatedAcademic.Shift;
                query.Parameters.Add("@idAcademic", MySqlDbType.Int32, 2).Value = updatedAcademic.IdAcademic;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/AcademicDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public bool UpdateAcademicPassword(int idAcademic, String password)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Academic SET Academic.password = @newPassword WHERE idAcademic = @idAcademic;"
                };

                MySqlParameter newPassword = new MySqlParameter("@newPassword", MySqlDbType.VarChar, 255)
                {
                    Value = password
                };

                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = idAcademic
                };

                query.Parameters.Add(newPassword);
                query.Parameters.Add(idacademic);

                query.ExecuteNonQuery();
                isUpdated = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/UpdateAcademicPassword:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }


    }
}