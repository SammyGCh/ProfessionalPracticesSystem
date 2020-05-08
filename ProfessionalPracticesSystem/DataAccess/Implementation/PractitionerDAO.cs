/*
    Date: 07/04/2020
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
    public class PractitionerDAO : IPractitionerDAO
    {
        private List<Practitioner> practitionerList;
        private Practitioner practitioner;
        private DataBaseConnection connection;
        private IndigenousLanguageDAO speaks;
        private AcademicDAO academic;
        private ProjectDAO assigned;
        private ScholarPeriodDAO belogsTo;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private int NO_ACTIVE = 0;

        public PractitionerDAO()
        {
            practitionerList = null;
            practitioner = null;
            connection = new DataBaseConnection();
            speaks = null;
            academic = null;
            assigned = null;
            belogsTo = null;
            mySqlConnection = null;
            query = null;
            reader = null;
        }

        public bool DeletePractitioner(int idPractitioner)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Practitioner SET Status=@status WHERE Practitioner.idPractitioner = @idPractitioner"
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = NO_ACTIVE
                };

                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(status);
                query.Parameters.Add(idpractitioner);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/DeletePractitioner:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<Practitioner> GetAllPractitioner()
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                practitionerList = new List<Practitioner>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practitioner"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (!reader.IsDBNull(8))
                    {
                        practitioner.Assigned = assigned.GetProjectById(reader.GetInt32(8));
                    }

                    practitionerList.Add(practitioner);
                }      
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitioner:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitionerList;
        }
        
        public List<Practitioner> GetAllPractitionerByindigenousLanguage()
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                practitionerList = new List<Practitioner>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practitioner WHERE Practitioner.idIndigenousLanguage != 1"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (!reader.IsDBNull(8))
                    {
                        practitioner.Assigned = assigned.GetProjectById(reader.GetInt32(8));
                    }

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByIndigenousLanguage:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitionerList;
        }

        public Practitioner GetPractitioner(int idPractitioner)
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practitioner WHERE Practitioner.idPractitioner = @idPractitioner"
                };
                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(idpractitioner);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (!reader.IsDBNull(8))
                    {
                        practitioner.Assigned = assigned.GetProjectById(reader.GetInt32(8));
                    }
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetPractitioner:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitioner;
        }

        public bool SavePractitioner(Practitioner practitioner)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Practitioner(matricula, password, grade, gender, names," +
                    "lastName, idIndigenousLanguage, status, idAcademic, idScholarPeriod) VALUES (@matricula, @password, @grade," +
                    " @gender, @names, @lastName, @idIndigenousLanguage, @status, @idAcademic, @idScholarPeriod)"
                };

                MySqlParameter matricula = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = practitioner.Matricula
                };

                MySqlParameter password = new MySqlParameter("@password", MySqlDbType.VarChar, 255)
                {
                    Value = practitioner.Password
                };

                MySqlParameter grade = new MySqlParameter("@grade", MySqlDbType.VarChar, 5)
                {
                    Value = practitioner.Grade
                };

                MySqlParameter gender = new MySqlParameter("@gender", MySqlDbType.VarChar, 10)
                {
                    Value = practitioner.Gender
                };

                MySqlParameter names = new MySqlParameter("@names", MySqlDbType.VarChar, 60)
                {
                    Value = practitioner.Names
                };

                MySqlParameter lastName = new MySqlParameter("@lastName", MySqlDbType.VarChar, 60)
                {
                    Value = practitioner.LastName
                };

                MySqlParameter idIndigenousLanguage = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32, 2)
                {
                    Value = practitioner.Speaks.IdIndigenousLanguage
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 2)
                {
                    Value = practitioner.Status
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 2)
                {
                    Value = practitioner.Instructed.IdAcademic
                };

                MySqlParameter scholarPeriod = new MySqlParameter("@idScholarPeriod", MySqlDbType.Int32, 2)
                {
                    Value = practitioner.ScholarPeriod.IdScholarPeriod
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
                query.Parameters.Add(scholarPeriod);


                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/SavePractitioner:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }

        public bool AssignPractitioner(int idPractitioner, int idProject)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Practitioner SET idProject=@idProject WHERE Practitioner.idPractitioner = @idPractitioner"
                };

                MySqlParameter idproject = new MySqlParameter("@idProject", MySqlDbType.Int32, 2)
                {
                    Value = idProject
                };

                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(idproject);
                query.Parameters.Add(idpractitioner);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/AssignPractitioner:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<Practitioner> GetAllPractitionerByAcademic(int idAcademic)
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                practitionerList = new List<Practitioner>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practitioner WHERE Practitioner.idAcademic = @idAcademic"
                };

                MySqlParameter idacademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = idAcademic
                };

                query.Parameters.Add(idacademic);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };
                    
                    if (!reader.IsDBNull(8))
                    {
                        practitioner.Assigned = assigned.GetProjectById(reader.GetInt32(8));
                    }

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByAcademic:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitionerList;
        }

        public List<Practitioner> GetAllPractitionerByProject(int idProject)
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                practitionerList = new List<Practitioner>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Practitioner WHERE Practitioner.idProject = @idProject"
                };

                MySqlParameter idproject = new MySqlParameter("@idProject", MySqlDbType.Int32, 11)
                {
                    Value = idProject
                };

                query.Parameters.Add(idproject);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Assigned = assigned.GetProjectById(reader.GetInt32(8)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByProject:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitionerList;
        }

        public bool UpdatePractitionerGrade(int idPractitioner)
        {
            bool isUpdated = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE practitioner SET grade = (SELECT AVG(grade) FROM document WHERE idPractitioner = @idPractitioner) WHERE idPractitioner = @idPractitioner;"
                };

                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(idpractitioner);

                query.ExecuteNonQuery();
                isUpdated = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/UpdatePractitionerGrade:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public List<Practitioner> GetAllPractitionerByLinkedOrganization(int idLinkedOrganization)
        {
            belogsTo = new ScholarPeriodDAO();
            speaks = new IndigenousLanguageDAO();
            academic = new AcademicDAO();
            assigned = new ProjectDAO();
            try
            {
                practitionerList = new List<Practitioner>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT DISTINCT practitioner.idPractitioner,practitioner.matricula,practitioner.password,practitioner.grade," +
                    "practitioner.gender,practitioner.names,practitioner.lastName,practitioner.idIndigenousLanguage,practitioner.idProject," +
                    "practitioner.status,practitioner.idAcademic,practitioner.idScholarPeriod FROM practitioner,project,linkedorganization " +
                    "WHERE practitioner.idProject = project.idProject AND project.idLinkedOrganization = @idLinkedOrganization"
                };

                MySqlParameter idOrganization = new MySqlParameter("@idLinkedOrganization", MySqlDbType.Int32, 11)
                {
                    Value = idLinkedOrganization
                };

                query.Parameters.Add(idOrganization);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Grade = reader.GetString(3),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Assigned = assigned.GetProjectById(reader.GetInt32(8)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByLinkedOrganization:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return practitionerList;
        }
    }
}
