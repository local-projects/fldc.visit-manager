using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class CPHeartBeatIncomingRequestParams
    {
        public string ID { get; set; }
        public string FW { get; set;  }
        public string IPv6 { get; set; }
        public string IPv4 { get; set; }
        public double Uptime { get; set; }
    }
}
