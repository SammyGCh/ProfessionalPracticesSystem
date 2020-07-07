/*
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
    public class DocumentTypeDAO : IDocumentTypeDAO
    {
        private List<DocumentType> documentTypeList;
        private DocumentType documentType;
        private DataBaseConnection connection;
        private MySqlConnection mySqlConnection;
        private MySqlCommand query;
        private MySqlDataReader reader;

        public DocumentTypeDAO()
        {
            connection = new DataBaseConnection();
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

                MySqlParameter iddocumentType = new MySqlParameter("@idDocumentType", MySqlDbType.Int32, 11)
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
                LogManager.WriteLog("Something went wrong in  DataAccess/Implementation/DocumentTypeDAO/GetDocumenntType:", ex);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                connection.CloseConnection();
            }

            return documentType;
        }
    }
}