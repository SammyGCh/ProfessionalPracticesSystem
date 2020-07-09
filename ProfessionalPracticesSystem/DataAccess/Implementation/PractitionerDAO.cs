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
        private readonly DataBaseConnection connection;
        private IndigenousLanguageDAO speaks;
        private AcademicDAO academic;
        private ProjectDAO assigned;
        private ScholarPeriodDAO belogsTo;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private const int STATUS_NO_ACTIVE = 0;
        private const int STATUS_ACTIVE = 1;
        private const String GRADE_NOT_ASSIGNED_MESSAGE = "Calificaci�n no asignada";
        private const String GRADE_NOT_AVAILABLE_MESSAGE = "Calificaci�n no disponible";


        public PractitionerDAO()
        {
            connection = new DataBaseConnection();
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
                    Value = STATUS_NO_ACTIVE
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
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                if (reader != null)
                {
                    reader.Close();
                }
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
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                if (reader != null)
                {
                    reader.Close();
                }
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
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return practitioner;
        }

        public Practitioner GetPractitionerPersonalInfo(int idPractitioner)
        {
            practitioner = null;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT idPractitioner, names, lastName, matricula FROM Practitioner WHERE Practitioner.idPractitioner = @idPractitioner"
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
                        Names = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Matricula = reader.GetString(3),
                        Password = reader.GetString(2),
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetPractitioner:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                connection.CloseConnection();
            }

            return practitioner;
        }

        public Practitioner GetPractitionerByMatricula(String matriculaP)
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
                    CommandText = "SELECT * FROM Practitioner WHERE Practitioner.matricula = @matriculaP AND Practitioner.status = @status"
                };

                MySqlParameter matricula = new MySqlParameter("@matriculaP", MySqlDbType.VarChar, 9)
                {
                    Value = matriculaP
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(matricula);
                query.Parameters.Add(status);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                if (reader != null)
                {
                    reader.Close();
                }
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

                MySqlParameter idIndigenousLanguage = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32, 11)
                {
                    Value = practitioner.Speaks.IdIndigenousLanguage
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                MySqlParameter idAcademic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = practitioner.Instructed.IdAcademic
                };

                MySqlParameter scholarPeriod = new MySqlParameter("@idScholarPeriod", MySqlDbType.Int32, 11)
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

        public List<Practitioner> GetAllPractitionerByAcademic(String PersonalNumber)
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
                    CommandText = 
                    "SELECT " +
                    "Practitioner.idPractitioner, " +
                    "Practitioner.matricula, " +
                    "Practitioner.password, " +
                    "Practitioner.grade, " +
                    "Practitioner.gender, " +
                    "Practitioner.names, " +
                    "Practitioner.lastName, " +
                    "Practitioner.idIndigenousLanguage, " +
                    "Practitioner.idProject, " +
                    "Practitioner.status, " +
                    "Practitioner.idAcademic, " +
                    "Practitioner.idScholarPeriod " +
                    "FROM " +
                    "Academic,Practitioner " +
                    "WHERE " +
                    "Practitioner.idAcademic = Academic.idAcademic " +
                    "AND Academic.personalNumber = @personalNumber " +
                    "AND Practitioner.status = @status;"
                };

                MySqlParameter idacademic = new MySqlParameter("@personalNumber", MySqlDbType.VarChar, 9)
                {
                    Value = PersonalNumber
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(idacademic);
                query.Parameters.Add(status);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return practitionerList;
        }

        public List<Practitioner> GetAllPractitionerByMatricula()
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
                    CommandText = "SELECT * FROM Practitioner ORDER BY MATRICULA ASC"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    practitioner = new Practitioner
                    {
                        IdPractitioner = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Password = reader.GetString(2),
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

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
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Assigned = assigned.GetProjectById(reader.GetInt32(8)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByProject:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
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
                    CommandText = "UPDATE Practitioner SET Practitioner.grade = " +
                    "(SELECT ((Select AVG(Document.grade) from Document where Document.idPractitioner = @idPractitioner) + " +
                    "(select AVG(MensualReport.grade) from MensualReport where MensualReport.idPractitioner = @idPractitioner))" +
                    " / 2) WHERE idPractitioner = @idPractitioner"
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
                    CommandText = "SELECT DISTINCT Practitioner.idPractitioner,Practitioner.matricula,Practitioner.password,Practitioner.grade,Practitioner.gender," +
                    "Practitioner.names,Practitioner.lastName,Practitioner.idIndigenousLanguage,Practitioner.idProject,Practitioner.status,Practitioner.idAcademic," +
                    "Practitioner.idScholarPeriod FROM Practitioner,Project,LinkedOrganization WHERE Practitioner.idProject = Project.idProject AND Project.idLinkedOrganization = @idLinkedOrganization"
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
                        Gender = reader.GetString(4),
                        Names = reader.GetString(5),
                        LastName = reader.GetString(6),
                        Speaks = speaks.GetIndigenousLanguageById(reader.GetInt32(7)),
                        Assigned = assigned.GetProjectById(reader.GetInt32(8)),
                        Status = reader.GetInt32(9),
                        Instructed = academic.GetAcademic(reader.GetInt32(10)),
                        ScholarPeriod = belogsTo.GetScholarPeriodById(reader.GetInt32(11))
                    };

                    if (reader.IsDBNull(3))
                    {
                        practitioner.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        if (reader.GetString(3).Equals("0"))
                        {
                            practitioner.Grade = GRADE_NOT_AVAILABLE_MESSAGE;
                        }
                        else
                        {
                            practitioner.Grade = reader.GetString(3);
                        }
                    }

                    practitionerList.Add(practitioner);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/GetAllPractitionerByLinkedOrganization:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return practitionerList;
        }

        public bool UpdatePractitioner(Practitioner updatePractitioner)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Practitioner SET " +
                    "matricula = @matricula, " +
                    "gender = @gender, " +
                    "names = @names, " +
                    "lastName = @lastName, " +
                    "idIndigenousLanguage = @idIndigenousLanguage, " +
                    "idAcademic = @idAcademic, " +
                    "idScholarPeriod = @idScholarPeriod " +
                    "WHERE idPractitioner = @idPractitioner"
                };

                MySqlParameter matricula = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = updatePractitioner.Matricula
                };

                MySqlParameter gender = new MySqlParameter("@gender", MySqlDbType.VarChar, 10)
                {
                    Value = updatePractitioner.Gender
                };

                MySqlParameter names = new MySqlParameter("@names", MySqlDbType.VarChar, 60)
                {
                    Value = updatePractitioner.Names
                };

                MySqlParameter lastName = new MySqlParameter("@lastName", MySqlDbType.VarChar, 60)
                {
                    Value = updatePractitioner.LastName
                };

                MySqlParameter indigenousLanguage = new MySqlParameter("@idIndigenousLanguage", MySqlDbType.Int32,11)
                {
                    Value = updatePractitioner.Speaks.IdIndigenousLanguage
                };

                MySqlParameter academic = new MySqlParameter("@idAcademic", MySqlDbType.Int32, 11)
                {
                    Value = updatePractitioner.Instructed.IdAcademic
                };

                MySqlParameter scholarPeriod = new MySqlParameter("@idScholarPeriod", MySqlDbType.Int32, 11)
                {
                    Value = updatePractitioner.ScholarPeriod.IdScholarPeriod
                };

                MySqlParameter idPractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 11)
                {
                    Value = updatePractitioner.IdPractitioner
                };

                query.Parameters.Add(matricula);
                query.Parameters.Add(gender);
                query.Parameters.Add(names);
                query.Parameters.Add(lastName);
                query.Parameters.Add(indigenousLanguage);
                query.Parameters.Add(academic);
                query.Parameters.Add(scholarPeriod);
                query.Parameters.Add(idPractitioner);

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/PractitionerDAO/UpdatePractitioner: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public bool UpdatePractitionerPassword(int idPractitioner, String password)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE Practitioner SET Practitioner.password = @newPassword WHERE idPractitioner = @idPractitioner;"
                };

                MySqlParameter newPassword = new MySqlParameter("@newPassword", MySqlDbType.VarChar, 255)
                {
                    Value = password
                };

                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 11)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(newPassword);
                query.Parameters.Add(idpractitioner);

                query.ExecuteNonQuery();
                isUpdated = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/UpdatePractitionerPassword:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public bool PractitionerHasProject(String matricula)
        {
            bool hasProject = false;
            const int PROJECT_ASSIGNED = 1;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT COUNT(Practitioner.idProject) FROM Practitioner WHERE Practitioner.matricula = @matricula AND Practitioner.status = @status;"
                };


                MySqlParameter matriculaPractitioner = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = matricula
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(matriculaPractitioner);
                query.Parameters.Add(status);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    int result = reader.GetInt32(0);

                    if (result == PROJECT_ASSIGNED)
                    {
                        hasProject = true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/PractitionerDAO/PractitionerHasProject:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return hasProject;
        }
    }
}