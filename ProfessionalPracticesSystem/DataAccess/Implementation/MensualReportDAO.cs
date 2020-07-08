/*
        Date: 10/04/2020                               
        Author: Cesar Sergio Martinez Palacios
 */

using BusinessDomain;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DataAccess.DataBase;
using DataAccess.Interfaces;
using System;

namespace DataAccess.Implementation
{
    public class MensualReportDAO : IMensualReportDAO
    {
        private List<MensualReport> mensualReports;
        private MensualReport mensualReport;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private ProjectDAO projectHandler;
        private PractitionerDAO practitionerHandler;
        private const String GRADE_NOT_ASSIGNED_MESSAGE = "Calificacion no asignada";
        private const int STATUS_ACTIVE = 1;

        public MensualReportDAO()
        {
            mensualReports = null;
            mensualReport = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
            projectHandler = new ProjectDAO();
            practitionerHandler = new PractitionerDAO();
        }

        public bool InsertMensualReport(MensualReport mensualReport)
        {
            bool isSaved = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO MensualReport(description, monthReportedDate,name,idProject,idPractitioner)" +
                    "VALUES (@description, @monthReportedDate,@mensualReportName, @idProject, @idPractitioner)"
                };

                query.Parameters.Add("@description", MySqlDbType.LongText, 2000).Value = mensualReport.Description;
                query.Parameters.Add("@monthReportedDate", MySqlDbType.DateTime, 50).Value = DateTime.Parse(mensualReport.MonthReportedDate);
                query.Parameters.Add("@mensualReportName", MySqlDbType.VarChar, 60).Value = mensualReport.MensualReportName;
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = mensualReport.DerivedFrom.IdProject;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = mensualReport.GeneratedBy.IdPractitioner;

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public bool DeleteMensualReport(int idMensualReport)
        {
            bool isDeleted = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
                MySqlParameter id = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

                query.Parameters.Add(id);

                query.ExecuteNonQuery();
                isDeleted = true;

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }

        public bool UpdateMensualReport(MensualReport mensualReportUpdate)
        {
            bool isUpdated = false;

            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE MensualReport SET description = @description, monthReportedDate = @monthReportedDate, " +
                    "name = @mensualReportName, idProject = @idProject, idPractitioner = @idPractitioner, grade = @grade WHERE idMensualReport = @idMensualReport"
                };

                query.Parameters.Add("@description", MySqlDbType.LongText, 2000).Value = mensualReportUpdate.Description;
                query.Parameters.Add("@mensualReportName", MySqlDbType.VarChar, 60).Value = mensualReportUpdate.MensualReportName;
                query.Parameters.Add("@monthReportedDate", MySqlDbType.VarChar, 20).Value = DateTime.Parse(mensualReportUpdate.MonthReportedDate);
                query.Parameters.Add("@idProject", MySqlDbType.Int32, 2).Value = mensualReportUpdate.DerivedFrom.IdProject;
                query.Parameters.Add("@idPractitioner", MySqlDbType.Int32, 2).Value = mensualReportUpdate.GeneratedBy.IdPractitioner;
                query.Parameters.Add("@grade", MySqlDbType.VarChar, 5).Value = mensualReportUpdate.Grade;
                query.Parameters.Add("@idMensualReport", MySqlDbType.Int32, 2).Value = mensualReportUpdate.IdMensualReport;

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }

        public List<MensualReport> GetAllMensualReports()
        {
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5)),
                    };

                    if (reader.IsDBNull(6))
                    {
                        mensualReport.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        mensualReport.Grade = reader.GetString(6);
                    }

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/MensualReportDAO", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
        }

        public MensualReport GetMensualReportById(int idMensualReport)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE MensualReport.idMensualReport = @idMensualReport"
                };
                MySqlParameter id = new MySqlParameter("@idMensualReport", MySqlDbType.Int32, 2)
                {
                    Value = idMensualReport
                };

                query.Parameters.Add(id);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5)),
                    };

                    if (reader.IsDBNull(6))
                    {
                        mensualReport.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        mensualReport.Grade = reader.GetString(6);
                    }
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReport;
        }

        public List<MensualReport> GetAllReportsByPractitioner(String matricula)
        {
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT " +
                    "MensualReport.idMensualReport, " +
                    "MensualReport.description, " +
                    "MensualReport.monthReportedDate, " +
                    "MensualReport.name, " +
                    "MensualReport.idProject, " +
                    "MensualReport.idPractitioner, " +
                    "MensualReport.grade " +
                    "FROM MensualReport,Practitioner " +
                    "WHERE Practitioner.matricula = @matricula " +
                    "AND MensualReport.idPractitioner = Practitioner.idPractitioner"
                };

                MySqlParameter matriculaPractitioner = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = matricula
                };

                MySqlParameter state = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                query.Parameters.Add(matriculaPractitioner);
                query.Parameters.Add(state);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5)),
                    };

                    if (reader.IsDBNull(6))
                    {
                        mensualReport.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        mensualReport.Grade = reader.GetString(6);
                    }

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
        }

        public List<MensualReport> GetAllReportsByProject(int idProject)
        {
            mensualReports = new List<MensualReport>();
            practitionerHandler = new PractitionerDAO();
            projectHandler = new ProjectDAO();
            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM MensualReport WHERE idProject = @idProject"
                };

                MySqlParameter id = new MySqlParameter("@idProject", MySqlDbType.Int32, 2)
                {
                    Value = idProject
                };

                query.Parameters.Add(id);
                reader = query.ExecuteReader();
                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5)),
                    };

                    if (reader.IsDBNull(6))
                    {
                        mensualReport.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        mensualReport.Grade = reader.GetString(6);
                    }

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/MensualReport", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
        }

        public List<MensualReport> GetAllReportsByAcademic(String personalNumberAcademic)
        {
            List<MensualReport> mensualReports = new List<MensualReport>();
            PractitionerDAO practitionerHandler = new PractitionerDAO();
            ProjectDAO projectHandler = new ProjectDAO();

            try
            {
                mySqlConnection = connection.OpenConnection();

                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT " +
                    "MensualReport.idMensualReport, " +
                    "MensualReport.description, " +
                    "MensualReport.monthReportedDate, " +
                    "MensualReport.name, " +
                    "MensualReport.idProject, " +
                    "MensualReport.idPractitioner, " +
                    "MensualReport.grade " +
                    "FROM MensualReport,Practitioner,Academic " +
                    "WHERE MensualReport.idPractitioner = Practitioner.idPractitioner " +
                    "AND Practitioner.status = @status " +
                    "AND Academic.personalNumber = @personalNumber"
                };

                MySqlParameter status = new MySqlParameter("@status", MySqlDbType.Int32, 11)
                {
                    Value = STATUS_ACTIVE
                };

                MySqlParameter academic = new MySqlParameter("@personalNumber", MySqlDbType.VarChar, 9)
                {
                    Value = personalNumberAcademic
                };

                query.Parameters.Add(status);
                query.Parameters.Add(academic);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    mensualReport = new MensualReport
                    {
                        IdMensualReport = reader.GetInt32(0),
                        Description = reader.GetString(1),
                        MonthReportedDate = reader.GetString(2),
                        MensualReportName = reader.GetString(3),
                        DerivedFrom = projectHandler.GetProjectById(reader.GetInt32(4)),
                        GeneratedBy = practitionerHandler.GetPractitioner(reader.GetInt32(5)),
                    };

                    if (reader.IsDBNull(6))
                    {
                        mensualReport.Grade = GRADE_NOT_ASSIGNED_MESSAGE;
                    }
                    else
                    {
                        mensualReport.Grade = reader.GetString(6);
                    }

                    mensualReports.Add(mensualReport);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/GetAllReportsByAcademic", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return mensualReports;
        }

        public int GetNumberOfAllMensualReportsByPractitioner(String matricula)
        {
            int totalReports = 0;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT COUNT(idMensualReport) " +
                    "FROM MensualReport,Practitioner " +
                    "WHERE MensualReport.idPractitioner = Practitioner.idPractitioner " +
                    "AND Practitioner.matricula = @matricula;"
                };

                MySqlParameter matriculaPractitioner = new MySqlParameter("@matricula", MySqlDbType.VarChar, 9)
                {
                    Value = matricula
                };

                query.Parameters.Add(matriculaPractitioner);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    totalReports = reader.GetInt32(0);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/GetNumberOfAllMensualReportsByPractitioner:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return totalReports;
        }
    }
}