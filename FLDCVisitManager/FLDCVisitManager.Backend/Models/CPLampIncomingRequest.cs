using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class CPLampIncomingRequest
    {
        public string Id { get; set; }
        public string LampId { get; set; }
    }

    [MessagePackObject]
    public class BeaconsCPLampIncomingRequest
    {
        string BeaconID { get; set; }
        string LampID { get; set; }
        string AssetsID { get; set; }
    }
}
