﻿/*
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
    public class DocumentDAO : IDocumentDAO
    {
        private List<Document> documentList;
        private Document document;
        private PractitionerDAO addBy;
        private DocumentTypeDAO typeOf;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;
        private static readonly log4net.Ilog log = log4net.logManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DocumentDAO()
        {
            connection = new DataBaseConnection();
        }
        public bool DeleteDocument(int idDocument)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "DELETE FROM Document WHERE Document.idDocument = @idDocument"
                };
                MySqlParameter _idDocument = new MySqlParameter("@idDocument", MySqlDbType.Int32, 2)
                {
                    Value = idDocument
                };

                query.Parameters.Add(_idDocument);

                query.ExecuteNonQuery();
                isSaved = true;

            }
            catch(MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/DeleteDocument:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<Document> GetAllDocument()
        {
            try
            {
                documentList = new List<Document>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Document"
                };

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    document = new Document
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4))
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/GetAllDocument:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentList;
        }

        public Document GetDocument(int idDocument)
        {
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Document WHERE Document.idDocument = @idDocument"
                };
                MySqlParameter iddocument = new MySqlParameter("@idDocument", MySqlDbType.Int32, 2)
                {
                    Value = idDocument
                };

                query.Parameters.Add(iddocument);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    document = new Document
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4))
                    };
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/GetDocument:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return document;
        }

        public List<Document> GetDocumentByPractising(int idPractising)
        {
            try
            {
                documentList = new List<Document>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Document WHERE Document.idPractising = @idPractising"
                };
                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 2)
                {
                    Value = idPractising
                };

                query.Parameters.Add(idpractising);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    document = new Document
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4))
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/GetDocumentByPractising:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentList;
        }

        public List<Document> GetDocumentByType(int idDocumentType)
        {
            try
            {
                documentList = new List<Document>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Document WHERE Document.idDocumentType = @idDocumentType"
                };
                MySqlParameter idType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 1)
                {
                    Value = idDocumentType
                };

                query.Parameters.Add(idType);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    document = new Document
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4))
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/GetDocumentByType:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentList;
        }

        public bool SaveDocument(Document document)
        {
            bool isSaved = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "INSERT INTO Document(name, path, idDocumentType, idPractising)" +
                    "VALUES (@name, @path, @idDocumentType, @idPractising)"
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 60)
                {
                    Value = document.Name
                };

                MySqlParameter path = new MySqlParameter("@path", MySqlDbType.VarChar, 150)
                {
                    Value = document.Path
                };

                MySqlParameter iddocumentType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 2)
                {
                    Value = document.TypeOf.IdDocumentType
                };

                MySqlParameter idpractising = new MySqlParameter("@idPractising", MySqlDbType.Int32, 1)
                {
                    Value = document.AddBy.IdPractitioner
                };

                query.Parameters.Add(name);
                query.Parameters.Add(path);
                query.Parameters.Add(iddocumentType);
                query.Parameters.Add(idpractising);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                log.Error("Someting whent wrong in  DataAccess/Implementation/DocumentDAO/SaveDocument:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }
    }
}
