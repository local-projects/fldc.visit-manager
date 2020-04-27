using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DBManager
{
    public interface IDBManager
    {
        void SetDBConfiguration(string connectionString);
        SqlConnection OpenConnection();
        ResponseResult UpdateCollectionPointLampInteraction(CPLampData data);
        ResponseResult UpdateCollectionPoint(string iD, string fW);
        ResponseResult ChargerDockerLampRecognized(CDLampData cPLampData);
        FTPDetails GetFirmwareFtpDetails();
        List<string> GetVisitorCollectibleItems(string lampId);
        bool CPLampConnectedValidate();
    }
}
