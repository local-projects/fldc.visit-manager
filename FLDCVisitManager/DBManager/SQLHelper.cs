using Dapper;
using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DBManager
{
    public class SQLHelper : IDBManager
    {
        private static string _connectionString { get; set; }
        public SqlConnection OpenConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void UpdateCollectionPointLampInteraction(CPLampData data)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    var items = conn.Query("stp_VisitorToCollectionPoint_Insert", new { data.CPId, data.LampId }, commandType: CommandType.StoredProcedure);
                }
            }
            catch(Exception ex)
            {
                throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }

        }
        public void UpdateCollectionPoint(string cpId, string cpFw)
        {
            try
            {
                using (var conn = OpenConnection())
                {
                    var items = conn.Query("stp_CollectionPoint_Update", new { cpId, cpFw }, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }
        }

        public void SetDBConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ChargerDockerLampRecognized(CPLampData cPLampData)
        {
            throw new NotImplementedException();
        }
    }
}
