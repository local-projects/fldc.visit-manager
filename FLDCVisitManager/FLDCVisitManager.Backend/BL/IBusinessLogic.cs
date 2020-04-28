using DBManager.Models;
using FLDCVisitManagerBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.BL
{
    public interface IBusinessLogic
    {
        Task<List<CollectibleItemReference>> GetVisitorCollectibleItems(string lampId);
        void CollectionPointLamp(string cpId, string lampId);
        ResponseResult ChargerDockerLamp(ChargerDockerLampIncomingRequest cdLampReq);
        ResponseResult UpdateCollectionPointHeartBeat(CPHeartBeatIncomingRequestParams req);
        FTPDetails GetFirmwareFtpDetails();
        Task<List<CollectibleItem>> GetAllCollectibleItems(bool shopify, DateTime? dateLastTaken);
        string CPLampConnectedValidate();
    }
}
