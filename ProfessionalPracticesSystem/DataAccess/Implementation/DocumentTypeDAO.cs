/*
    Date: 07/04/2020
    Author(s) : Angel de Jesus Juarez Garcia
*/
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BusinessDomain;
using DataAccess.DataBase;
using DataAccess.Interfaces;

namespace DataAccess.Implementation
{
    public class DocumentTypeDAO : IDocumentTypeDAO
    {
        private List<DocumentType> documentTypeList;
        private DocumentType documentType;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.Ilog log = log4net.logManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DocumentTypeDAO()
        {
            connection = new DataBaseConnection();
        }
        public bool DeleteDocumentType(int idDocumentType)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM DocumentType WHERE DocumentType.idDocumentType = @idDocumentType"
                };
                MySqlParameter iddocumentType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 2)
                {
                    Value = idDocumentType
                };

                query.Parameters.Add(iddocumentType);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentTypeDAO/DeleteDocumentType:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<DocumentType> GetAllDocumentType()
        {
            try
            {
                documentTypeList = new List<DocumentType>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM DocumentType"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    documentType = new DocumentType
                    {
                        IdDocumentType = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };

                    documentTypeList.Add(documentType);
                }
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentTypeDAO/GetAllDocumentType:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentTypeList;
        }

        public DocumentType GetDocumentType(int idDocumentType)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM DocumentType WHERE DocumentType.idDocumentType = @idDocumentType"
                };

                MySqlParameter iddocumentType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 2)
                {
                    Value = idDocumentType
                };

                query.Parameters.Add(iddocumentType);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    documentType = new DocumentType
                    {
                        IdDocumentType = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentTypeDAO/GetDocumenntType:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentType;
        }

        public bool SaveDocumentType(DocumentType documentType)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO DocumentType(name) VALUES (@name)"
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 100)
                {
                    Value = documentType.Name
                };

                query.Parameters.Add(name);
      
                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentTypeDAO/SaveDocumentType:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }
    }
}
