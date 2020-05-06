/*
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
    public class AcademicTypeDAO : IAcademicTypeDAO
    {
        private List <AcademicType> academicTypes;
        private AcademicType academicType;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AcademicTypeDAO()
        {
            this.academicTypes = null;
            this.academicType = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<AcademicType> GetAllAcademicTypes()
        {
             try
            {
                academicTypes = new List<AcademicType>();
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText ="SELECT * FROM AcademicType"
                };
                
                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    academicType = new AcademicType
                    {
                        IdAcademicType = reader.GetInt32(0),
                        AcademicTypeName = reader.GetString(1)
                    };

                    academicTypes.Add(academicType);
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/AcademicTypeDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return academicTypes;
        }

        public AcademicType GetAcademicTypeById(int idAcademicType)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM AcademicType WHERE idAcademicType = @idAcademicType"
                };

                MySqlParameter idType = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 32)
                {
                    Value = idAcademicType
                };

                query.Parameters.Add(idType);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    academicType = new AcademicType
                    {
                        IdAcademicType = reader.GetInt32(0),
                        AcademicTypeName = reader.GetString(1)
                    };
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Someting whent wrong in DataAccess/Implementation/AcademicTypeDAO", ex );
            }
            finally
            {
                connection.CloseConnection();
            }

            return academicType;
        }
        public bool InsertAcademicType(AcademicType academicType)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO AcademicType(name) VALUES (@academicTypeName)"
                };

                query.Parameters.Add("@academicTypeName", MySqlDbType.VarChar, 255).Value = academicType.AcademicTypeName;
                query.ExecuteNonQuery();

                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/AcademicTypeDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }
        public bool DeleteAcademicTypeById(int idAcademicType)
        {
            bool isDeleted = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "DELETE FROM AcademicType WHERE AcademicType.idAcademicType = @idAcademicType"
                };
                MySqlParameter id = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 2)
                {
                    Value = idAcademicType
                };
                query.Parameters.Add(id);
                query.ExecuteNonQuery();

                isDeleted = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/AcademicTypeDAO", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isDeleted;
        }

    }   
}