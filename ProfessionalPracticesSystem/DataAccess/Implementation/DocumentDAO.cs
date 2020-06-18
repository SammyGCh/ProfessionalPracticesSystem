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

        public DocumentDAO()
        {
            documentList = null;
            document = null;
            connection = new DataBaseConnection();
            mySqlConnection = null;
            query = null;
            reader = null;
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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/DeleteDocument:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }

            return isSaved;
        }

        public List<Document> GetAllDocument()
        {
            addBy = new PractitionerDAO();
            typeOf = new DocumentTypeDAO();
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
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4)),
                        Grade = reader.GetString(5),
                        Observations = reader.GetString(6)
                        
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/GetAllDocument:", ex);
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
            addBy = new PractitionerDAO();
            typeOf = new DocumentTypeDAO();
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
                    document = new Document()
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4)),
                        Grade = reader.GetString(5),
                        Observations = reader.GetString(6)
                    };
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/GetDocument:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return document;
        }

        public List<Document> GetAllDocumentByPractitioner(int idPractitioner)
        {
            addBy = new PractitionerDAO();
            typeOf = new DocumentTypeDAO();
            try
            {
                documentList = new List<Document>();
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "SELECT * FROM Document WHERE Document.idPractitioner = @idPractitioner"
                };
                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 2)
                {
                    Value = idPractitioner
                };

                query.Parameters.Add(idpractitioner);

                reader = query.ExecuteReader();

                while (reader.Read())
                {
                    document = new Document
                    {
                        IdDocument = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Path = reader.GetString(2),
                        TypeOf = typeOf.GetDocumentType(reader.GetInt32(3)),
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4)),
                        Grade = reader.GetString(5),
                        Observations = reader.GetString(6)
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/GetAllDocumentByPractitioner:", ex);
            }
            finally
            {
                reader.Close();
                connection.CloseConnection();
            }

            return documentList;
        }

        public List<Document> GetAllDocumentByType(int idDocumentType)
        {
            addBy = new PractitionerDAO();
            typeOf = new DocumentTypeDAO();
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
                        AddBy = addBy.GetPractitioner(reader.GetInt32(4)),
                        Grade = reader.GetString(5),
                        Observations = reader.GetString(6)
                    };

                    documentList.Add(document);
                }

            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/GetDocumentByType:", ex);
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
                    CommandText = "INSERT INTO Document(name, path, idDocumentType, idPractitioner)" +
                    "VALUES (@name, @path, @idDocumentType, @idPractitioner)"
                };

                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar, 60)
                {
                    Value = document.Name
                };

                MySqlParameter path = new MySqlParameter("@path", MySqlDbType.VarChar, 150)
                {
                    Value = document.Path
                };

                MySqlParameter iddocumentType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 11)
                {
                    Value = document.TypeOf.IdDocumentType
                };

                MySqlParameter idpractitioner = new MySqlParameter("@idPractitioner", MySqlDbType.Int32, 11)
                {
                    Value = document.AddBy.IdPractitioner
                };

                query.Parameters.Add(name);
                query.Parameters.Add(path);
                query.Parameters.Add(iddocumentType);
                query.Parameters.Add(idpractitioner);

                query.ExecuteNonQuery();
                isSaved = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/SaveDocument:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isSaved;
        }

        public bool UpdateDocumentGrade(int idDocument, String grade)
        {
            bool isUpdated = false;
            try
            {
                mySqlConnection = connection.OpenConnection();
                query = new MySqlCommand("", mySqlConnection)
                {
                    CommandText = "UPDATE document SET grade = @grade WHERE document.idDocument = @idDocument"
                };

                MySqlParameter grad = new MySqlParameter("@grade", MySqlDbType.VarChar, 5)
                {
                    Value = grade
                };

                MySqlParameter iddocument = new MySqlParameter("@idDocument", MySqlDbType.Int32, 11)
                {
                    Value = idDocument
                };

                query.Parameters.Add(grad);
                query.Parameters.Add(iddocument);

                query.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (MySqlException ex)
            {
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentDAO/UpdateDocumentGrade:", ex);
            }
            finally
            {
                connection.CloseConnection();
            }
            return isUpdated;
        }
    }
}
