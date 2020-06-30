/*
    Date: 07/04/2020
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
    public class LinkedOrganizationDAO : ILinkedOrganizationDAO
    {
        private List<LinkedOrganization> linkedOrganizations;
        private LinkedOrganization linkedOrganization;
        private OrganizationSectorDAO organizationSectorHandle;
        private DataBaseConnection connection;
        private MySqlConnection mysqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public LinkedOrganizationDAO()
        {
            linkedOrganizations = null;
            linkedOrganization = null;
            organizationSectorHandle = new OrganizationSectorDAO();
            connection = new DataBaseConnection();
            mysqlConnection = null;
            query = null;
            reader = null;
        }

        public bool SaveLinkedOrganization(LinkedOrganization linkedOrganization)
        {
            bool isSaved = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "INSERT INTO LinkedOrganization (city, email, state, name, telephoneNumber, address, idOrganizationSector)" +
                    "VALUES (@city, @email, @state, @name, @telephoneNumber, @address, @idOrganizationSector)"
                };

                MySqlParameter city = new MySqlParameter("@city", MySqlDbType.VarChar, 45)
                {
                    Value = linkedOrganization.City
                };
                MySqlParameter email = new MySqlParameter("@email", MySqlDbType.VarChar, 60)
                {
                    Value = linkedOrganization.Email
                };
                MySqlParameter state = new MySqlParameter("@state", MySqlDbType.VarChar, 45)
                {
                    Value = linkedOrganization.State
                };
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 100)
                {
                    Value = linkedOrganization.Name
                };
                MySqlParameter telephoneNumber = new MySqlParameter("@telephoneNumber", MySqlDbType.VarChar, 10)
                {
                    Value = linkedOrganization.TelephoneNumber
                };
                MySqlParameter address = new MySqlParameter("@address", MySqlDbType.VarChar, 150)
                {
                    Value = linkedOrganization.Address
                };
                MySqlParameter idOrganizationSector = new MySqlParameter("@idOrganizationSector", MySqlDbType.Int32, 32)
                {
                    Value = linkedOrganization.BelongsTo.IdOrganizationSector
                };

                query.Parameters.Add(city);
                query.Parameters.Add(email);
                query.Parameters.Add(state);
                query.Parameters.Add(name);
                query.Parameters.Add(telephoneNumber);
                query.Parameters.Add(address);
                query.Parameters.Add(idOrganizationSector);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<LinkedOrganization> GetAllLinkedOrganizations()
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM LinkedOrganization"
                };

                reader = query.ExecuteReader();
                linkedOrganizations = new List<LinkedOrganization>();

                while (reader.Read())
                {
                    linkedOrganization = new LinkedOrganization
                    {
                        IdLinkedOrganization = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Email = reader.GetString(2),
                        State = reader.GetString(3),
                        Name = reader.GetString(4),
                        TelephoneNumber = reader.GetString(5),
                        Address = reader.GetString(6),
                        BelongsTo = organizationSectorHandle.GetOrganizationSectorById(reader.GetInt32(7))
                    };

                    linkedOrganizations.Add(linkedOrganization);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                
                connection.CloseConnection();
            }

            return linkedOrganizations;
        }

        public LinkedOrganization GetLinkedOrganizationById(int idLinkedOrganization)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM LinkedOrganization WHERE idLinkedOrganization = @idLinkedOrganization"
                };

                MySqlParameter idLinkedOrg = new MySqlParameter("@idLinkedOrganization", MySqlDbType.Int32, 32)
                {
                    Value = idLinkedOrganization
                };

                query.Parameters.Add(idLinkedOrg);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    linkedOrganization = new LinkedOrganization
                    {
                        IdLinkedOrganization = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Email = reader.GetString(2),
                        State = reader.GetString(3),
                        Name = reader.GetString(4),
                        TelephoneNumber = reader.GetString(5),
                        Address = reader.GetString(6),
                        BelongsTo = organizationSectorHandle.GetOrganizationSectorById(reader.GetInt32(7))
                    };
                }
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                
                connection.CloseConnection();
            }

            return linkedOrganization;
        }

        public LinkedOrganization GetLinkedOrganizationByName(String name)
        {
            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM LinkedOrganization WHERE name = @name"
                };

                MySqlParameter orgName = new MySqlParameter("@name", MySqlDbType.VarChar, 100)
                {
                    Value = name
                };

                query.Parameters.Add(orgName);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    linkedOrganization = new LinkedOrganization
                    {
                        IdLinkedOrganization = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Email = reader.GetString(2),
                        State = reader.GetString(3),
                        Name = reader.GetString(4),
                        TelephoneNumber = reader.GetString(5),
                        Address = reader.GetString(6),
                        BelongsTo = organizationSectorHandle.GetOrganizationSectorById(reader.GetInt32(7))
                    };
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return linkedOrganization;
        }

        public List<LinkedOrganization> GetLinkedOrganizationBySector(OrganizationSector organizationSector)
        {
            linkedOrganizations = new List<LinkedOrganization>();

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "SELECT * FROM LinkedOrganization WHERE idOrganizationSector = @idOrganizationSector"
                };

                MySqlParameter idOrganizationSector = new MySqlParameter("@idOrganizationSector", MySqlDbType.Int32, 32)
                {
                    Value = organizationSector.IdOrganizationSector
                };

                query.Parameters.Add(idOrganizationSector);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    linkedOrganization = new LinkedOrganization
                    {
                        IdLinkedOrganization = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Email = reader.GetString(2),
                        State = reader.GetString(3),
                        Name = reader.GetString(4),
                        TelephoneNumber = reader.GetString(5),
                        Address = reader.GetString(6),
                        BelongsTo = organizationSectorHandle.GetOrganizationSectorById(reader.GetInt32(7))
                    };

                    linkedOrganizations.Add(linkedOrganization);
                }
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return linkedOrganizations;
        }

        public bool UpdateLinkedOrganization(LinkedOrganization linkedOrganizationUpdated)
        {
            bool isUpdated = false;

            try
            {
                mysqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mysqlConnection)
                {
                    CommandText = "UPDATE LinkedOrganization SET " +
                    "city = @city, email = @email, state = @state, name = @name, telephoneNumber = @telephoneNumber, address = @address, " +
                    "idOrganizationSector = @idOrganizationSector " +
                    "WHERE idLinkedOrganization = @idLinkedOrganization"
                };

                MySqlParameter city = new MySqlParameter("@city", MySqlDbType.VarChar, 45)
                {
                    Value = linkedOrganizationUpdated.City
                };
                MySqlParameter email = new MySqlParameter("@email", MySqlDbType.VarChar, 60)
                {
                    Value = linkedOrganizationUpdated.Email
                };
                MySqlParameter state = new MySqlParameter("@state", MySqlDbType.VarChar, 45)
                {
                    Value = linkedOrganizationUpdated.State
                };
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 100)
                {
                    Value = linkedOrganizationUpdated.Name
                };
                MySqlParameter telephoneNumber = new MySqlParameter("@telephoneNumber", MySqlDbType.VarChar, 10)
                {
                    Value = linkedOrganizationUpdated.TelephoneNumber
                };
                MySqlParameter address = new MySqlParameter("@address", MySqlDbType.VarChar, 150)
                {
                    Value = linkedOrganizationUpdated.Address
                };
                MySqlParameter idOrganizationSector = new MySqlParameter("@idOrganizationSector", MySqlDbType.Int32, 32)
                {
                    Value = linkedOrganizationUpdated.BelongsTo.IdOrganizationSector
                };
                MySqlParameter idLinkedOrganization = new MySqlParameter("@idLinkedOrganization", MySqlDbType.Int32, 32)
                {
                    Value = linkedOrganizationUpdated.IdLinkedOrganization
                };

                query.Parameters.Add(city);
                query.Parameters.Add(email);
                query.Parameters.Add(state);
                query.Parameters.Add(name);
                query.Parameters.Add(telephoneNumber);
                query.Parameters.Add(address);
                query.Parameters.Add(idOrganizationSector);
                query.Parameters.Add(idLinkedOrganization);

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch(MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in DataAccess/Implementation/LinkedOrganizationDAO: ", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isUpdated;
        }
    }
}
