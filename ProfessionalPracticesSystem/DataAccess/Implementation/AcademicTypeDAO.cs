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

        public AcademicTypeDAOImp()
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

                    AcademicTypes.Add(academicType);
                }

                reader.Close();

            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return AcademicTypes;
        }

        public AcademicType GetAcademicType(int idAcademicType)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM AcademicType WHERE idAcademicType = @idAcademicType"
                };

                MySqlParameter idAcadType = new MySqlParameter("@idAcademicType", MySqlDbType.Int32, 32)
                {
                    Value = idAcademicType
                };

                query.Parameters.Add(idAcadType);

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
                log.Error("Ocurrio un error al intertar conectar a la base de datos: ", ex );
            }
            finally
            {
                connection.CloseConnection();
            }

            return academicType;
        }
   
    }   
}