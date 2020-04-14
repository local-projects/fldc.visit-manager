using FLDCVisitManager.CMSDataLayar.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FLDCVisitManager.CMSDataLayar
{
    public interface ICMSDataHelper
    {
        Task<LedColorsSeq> GetLedColors(string seqId);
        Task<CollectionPoint> GetCollectionPointDataById(string cpId);
        void ConnectToCMS(AppOptions appOptions);
        Task<CollectionPointAssets> GetCollectionAssets(List<string> iv);
    }
}
