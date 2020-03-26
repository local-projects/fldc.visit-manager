using DBManager.Models;
using System;
using System.Data.SqlClient;

namespace DBManager
{
    public interface IDBManager
    {
        SqlConnection OpenConnection(string connectionString);
        void UpdateCollectionPointLampInteraction(CPLampData data);
    }
}
