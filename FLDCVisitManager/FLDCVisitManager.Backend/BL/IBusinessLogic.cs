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
        Task<List<CollectibleItem>> GetVisitorCollectabileItems(string lampId);
        void CollectionPointLamp(string cpId, string lampId);
        ResponseResult ChargerDockerLamp(ChargerDockerLampIncomingRequest cdLampReq);
        ResponseResult UpdateCollectionPointHeartBeat(CPHeartBeatIncomingRequestParams req);
        FTPDetails GetFirmwareFtpDetails();
        void GetAllCollectabileItems(DateTime? dateLastTaken);
    }
}
