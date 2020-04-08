/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System.Collections.Generic;
using BusinessDomain;
using DataAccess.DataBase;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class OrganizationSectorDaoImp : IOrganizationSectorDao
    {
        private List<OrganizationSector> organizationSectors;
        private OrganizationSector organizationSector;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OrganizationSectorDaoImp()
        {
            organizationSectors = null;
            organizationSector = null;
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public List<OrganizationSector> GetAllOrganizationSectors()
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection) 
                {
                    CommandText = "SELECT * FROM OrganizationSector"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    organizationSector = new OrganizationSector
                    {
                        IdOrganizationSector = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };

                    organizationSectors.Add(organizationSector);
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Ocurrio un error: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }


            return organizationSectors;
        }

        public OrganizationSector GetOrganizationSector(int idOrganizationSector)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection) 
                {
                    CommandText = "SELECT * FROM OrganizationSector WHERE idOrganizationSector = @idOrganizationSector"
                };
                
                MySqlParameter idOrgSector = new MySqlParameter("@idOrganizationSector", MySqlDbType.Int32, 32)
                {
                    Value = idOrganizationSector
                };

                query.Parameters.Add(idOrgSector);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    organizationSector = new OrganizationSector
                    {
                        IdOrganizationSector = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
            }
            catch(MySqlException ex)
            {
                log.Error("Ocurrio un error: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return organizationSector;
        }
    }
}
