using Dapper;
using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;

namespace DBManager
{
    public class SQLHelper : IDBManager
    {
        private static string _connectionString { get; set; }
        public void SetDBConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection OpenConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public ResponseResult UpdateCollectionPointLampInteraction(CPLampData data)
        {
            var insertedRows = 0;
            try
            {
                using (var conn = OpenConnection())
                {
                    insertedRows = conn.Query<int>("stp_VisitorToCollectionPoint_Insert", new { data.CPId, data.LampId, data.AssetId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                return new ResponseResult() { Status = 400, Message = $"Error inserting to DB, error message {ex.Message}" };
                //throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }
            if (insertedRows >= 0)
                return new ResponseResult();
            else
                return new ResponseResult() { Status = 206, Message = "Nothing was inserted to the DB" };
        }
        public ResponseResult UpdateCollectionPoint(string cpId, string cpFw)
        {
            var updatedRows = 0;
            try
            {
                using (var conn = OpenConnection())
                {
                    updatedRows = conn.Query<int>("stp_CollectionPoint_Update", new { cpId, cpFw }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Status = 400, Message = $"Error inserting to DB, error message {ex.Message}" };
                //throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }
            if (updatedRows >= 0)
                return new ResponseResult();
            else
                return new ResponseResult() { Status = 206, Message = "Nothing was updating the DB" };
        }
        public List<string> GetVisitorCollectibleItems(string lampId)
        {
            List<string> result = new List<string>();
            try
            {
                using (var conn = OpenConnection())
                {
                    return result = conn.Query<string>("stp_VisitorCollections_Get", new { lampId }, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
                //throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }
        }
        public ResponseResult ChargerDockerLampRecognized(CDLampData cdLampData)
        {
            var updatedRows = 0;
            try
            {
                using (var conn = OpenConnection())
                {
                    updatedRows = conn.Query<int>("stp_ChargerDockerLamp_Update", new { cdLampData.LampId, cdLampData.Port ,cdLampData.Status, cdLampData.FwVersion, cdLampData.SeqVersion }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return new ResponseResult() { Status = 400, Message = $"Error updating the DB, error message {ex.Message}" };
                //throw (new Exception($"Error connecting to the DB, error message {ex.Message}"));
            }
            if (updatedRows >= 0)
                return new ResponseResult();
            else
                return new ResponseResult() { Status = 206, Message = "Nothing was updated in the DB" };
        }

        public FTPDetails GetFirmwareFtpDetails()
        {
            FTPDetails result = null;
            try
            {
                using (var conn = OpenConnection())
                {
                    result = conn.Query<FTPDetails>("stp_FirmwareFtpDetails_Get", new {  }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        
        public string CPLampConnectedValidate()
        {
            string sql = @"SELECT TOP 1 
                           Lamps.Id
                            from[VisitorToCollection]
                            inner join Visitors on Visitors.Id = [VisitorId]
                            inner join Lamps on Lamps.id = LampId
                            where[VisitorToCollection].DateCreated > dateadd(MINUTE, -1, getdate())";
            try
            {
                using (var conn = OpenConnection())
                {
                    var result = conn.Query<string>(sql).FirstOrDefault();
                    if (!string.IsNullOrEmpty(result))
                        return result;
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
