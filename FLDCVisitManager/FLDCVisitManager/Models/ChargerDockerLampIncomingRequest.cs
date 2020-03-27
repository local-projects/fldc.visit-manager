using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class ChargerDockerLampIncomingRequest
    {
        public string LampId { get; set; }
        public int Port { get; set; }
        public string Status { get; set; }
        public int Battery { get; set; }
        public string FwVersion { get; set; }
        public string SeqVersion { get; set; }
    }
}
