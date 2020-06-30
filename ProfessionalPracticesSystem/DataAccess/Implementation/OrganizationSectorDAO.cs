/*
    Date: 06/04/2020
    Author(s): Sammy Guadarrama Chavez
 */

using System;
using System.Collections.Generic;
using BusinessDomain;
using DataAccess.DataBase;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class OrganizationSectorDAO : IOrganizationSectorDAO
    {
        private List<OrganizationSector> organizationSectors;
        private OrganizationSector organizationSector;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public OrganizationSectorDAO()
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
            organizationSectors = new List<OrganizationSector>();

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
                        Name = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };

                    organizationSectors.Add(organizationSector);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/OrganizationSectorDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return organizationSectors;
        }

        public OrganizationSector GetOrganizationSectorById(int idOrganizationSector)
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
                        Name = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/OrganizationSectorDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return organizationSector;
        }

        public OrganizationSector GetOrganizationSectorByName(String sectorName)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM OrganizationSector WHERE name = @sectorName"
                };

                MySqlParameter sectorsName = new MySqlParameter("@sectorName", MySqlDbType.VarChar, 25)
                {
                    Value = sectorName
                };

                query.Parameters.Add(sectorsName);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    organizationSector = new OrganizationSector
                    {
                        IdOrganizationSector = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Status = reader.GetInt32(2)
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/OrganizationSectorDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return organizationSector;
        }
    }
}
