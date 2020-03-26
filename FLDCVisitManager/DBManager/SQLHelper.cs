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
        private static string connectionString { get; set; }
        public SqlConnection OpenConnection(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void UpdateCollectionPointLampInteraction(CPLampData data)
        {
            using (var conn = OpenConnection(connectionString))
            {
                var items = conn.Query("stp_VisitorToCollectionPoint_Insert", new { data.CPId, data.LampId }, commandType: CommandType.StoredProcedure);
            }

        }
    }
}
