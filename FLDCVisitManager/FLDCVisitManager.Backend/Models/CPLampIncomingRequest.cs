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
        [Key("beaconId")]
        public string BeaconId {get; set;}
        [Key("lampId")]
        public string LampId { get; set; }
        [Key("assetsId")]
        public string AssetsId { get; set; }
    }
}
