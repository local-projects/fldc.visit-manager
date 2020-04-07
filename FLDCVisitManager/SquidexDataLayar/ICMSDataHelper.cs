using CMSDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMSDataLayer
{
    public interface ICMSDataHelper
    {
        Task<LedColorsSeq> GetLedColors(string seqId);
        Task<CollectionPoint> GetCollectionPointById(string cpId);
        void ConnectToCMS(AppOptions appOptions);
    }
}
