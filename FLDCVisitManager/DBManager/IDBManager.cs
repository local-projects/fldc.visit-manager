using DBManager.Models;
using System;
using System.Data.SqlClient;

namespace DBManager
{
    public interface IDBManager
    {
        void SetDBConfiguration(string connectionString);
        SqlConnection OpenConnection();
        void UpdateCollectionPointLampInteraction(CPLampData data);
        void UpdateCollectionPoint(string iD, string fW);
    }
}
