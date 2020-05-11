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
        [Key("beaconID")]
        string beaconID { get; set; }
        [Key("lampID")]
        string lampID { get; set; }
        [Key("assetsID")]
        string assetsID { get; set; }
    }
}
