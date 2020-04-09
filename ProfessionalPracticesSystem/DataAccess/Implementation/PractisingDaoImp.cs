/*
    Date: 07/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
 */
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class PractisingDaoImp : IPractisingDao
    {
        private List<Practising> practisingList;
        private Practising practising;
        private DataBaseConnection connection;
        private IndigenousLanguageDaoImp speaks;
        private AcademicDaoImp academic;
        private ProjectDaoImp assigned;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        //private static readonly log4net.Ilog log = log4net.logManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PractisingDaoImp()
        {
            practisingList = null;
            practising = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
            speaks = null;
            academic = null;
            assigned = null;
        }

        public bool DeletePractising(int idPractising)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Practising SET Status='No activo' WHERE Practising.idPractising = @idPractising"
                };
                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 2)
                {
                    Value = idPractising
                };

                query.Parameters.Add(idpractising);

                query.ExecuteNonQuery();
                return true;

            }
            catch (MySqlException ex)
            {
                //log.Error("Ocurrio un error:", ex);
                return false;
            }
            finally
            {
                connection.CloseConnection();
            }
        }

        public List<Practising> GetAllPractising()
        {
            try
            {
                practisingList = null;
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practising"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practising = new Practising
                    {
                        IdPractising = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetFloat(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.getIndigenousLanguage(reader.GetInt32(7)),
                        Assigned = assigned.getProject(reader.GetInt32(8)),
                        Status = reader.GetString(9),
                        Instructed = academic.getAcademic(reader.GetInt32(10))


                    };

                    practisingList.Add(practising);
                }      
            }
            catch (MySqlException ex)
            {
                //log.Error("Ocurrio un error:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practisingList;
        }
        
        public List<Practising> GetAllPractisingByindigenousLanguage()
        {
            try
            {
                practisingList = null;
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practising WHERE Practising.idIndigenousLanguage != 1"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practising = new Practising
                    {
                        IdPractising = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetFloat(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.getIndigenousLanguage(reader.GetInt32(7)),
                        Assigned = assigned.getProject(reader.GetInt32(8)),
                        Status = reader.GetString(9),
                        Instructed = academic.getAcademic(reader.GetInt32(10))


                    };

                    practisingList.Add(practising);
                }
            }
            catch (MySqlException ex)
            {
                //log.Error("Ocurrio un error:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practisingList;
        }

        public Practising GetPractising(int idPractising)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practising WHERE Practising.idPractising = @idPractising"
                };
                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 2)
                {
                    Value = idPractising
                };
                query.Parameters.Add(idpractising);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practising = new Practising
                    {
                        IdPractising = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetFloat(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.getIndigenousLanguage(reader.GetInt32(7)),
                        Assigned = assigned.getProject(reader.GetInt32(8)),
                        Status = reader.GetString(9),
                        Instructed = academic.getAcademic(reader.GetInt32(10))
                    };
                }
            }
            catch (MySqlException ex)
            {
                //log.Error("Ocurrio un error:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practising;
        }

        public bool SavePractising(Practising practising)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Practising(matricula, password, grade, gender, names," +
                    "lastName, idIndigenousLanguage, status, idAcademic) VALUES (@matricula, @password, @grade," +
                    " @gender, @names, @lastName, @idIndigenousLanguage, @status, @idAcademic)"
                };

                MySqlParameter matricula = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = practising.Matricula
                };

                MySqlParameter password = new MySqlParameter("@password", MySqlDbType.VarChar, 255)
                {
                    Value = practising.Password
                };

                MySqlParameter grade = new MySqlParameter("@grade", MySqlDbType.Float, 2)
                {
                    Value = practising.Grade
                };

                MySqlParameter gender = new MySqlParameter("@gender", MySqlDbType.VarChar, 10)
                {
                    Value = practising.Gender
                };

                MySqlParameter names = new MySqlParameter("@names", MySqlDbType.VarChar, 60)
                {
                    Value = practising.Names
                };

                MySqlParameter lastName = new MySqlParameter("@lastName", MySqlDbType.VarChar, 60)
                {
                    Value = practising.LastName
                };

                MySqlParameter idIndigenousLanguage = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32, 2)
                {
                    Value = practising.Speaks.IdIndigenousLanguage
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.VarChar, 10)
                {
                    Value = practising.Status
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = practising.Instructed.IdAcademic
                };


                query.Parameters.Add(matricula);
                query.Parameters.Add(password);
                query.Parameters.Add(grade);
                query.Parameters.Add(gender);
                query.Parameters.Add(names);
                query.Parameters.Add(lastName);
                query.Parameters.Add(idIndigenousLanguage);
                query.Parameters.Add(status);
                query.Parameters.Add(idAcademic);


                query.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                //log.Error("Ocurrio un error:", ex);
                return false;
            }
            finally
            {
                connection.CloseConnection();
            }
        }
    }
}
