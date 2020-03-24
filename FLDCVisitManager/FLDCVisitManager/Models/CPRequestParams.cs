using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class CPRequestParams
    {
        public string PointName { get; set; }
        public string CpIp { get; set; }
        public LEDRequestParams SleepAnimation { get; set; }
        public LEDRequestParams TriggerAnimation { get; set; }
        public LEDRequestParams ReturnAnimation { get; set; }
    }
}
