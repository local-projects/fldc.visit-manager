using Newtonsoft.Json;
using Squidex.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSDataLayer.Models
{
    public class CollectionPoint : SquidexEntityBase<CollectionPointData>
    {

    }
    public partial class CollectionPointData
    {
        [JsonProperty("pointID")]
        public PointId PointId { get; set; }

        [JsonProperty("pointName")]
        public CpIp PointName { get; set; }

        [JsonProperty("pointAsset")]
        public ReturnAnimation PointAsset { get; set; }

        [JsonProperty("CPIp")]
        public CpIp CpIp { get; set; }

        [JsonProperty("SleepAnimation")]
        public ReturnAnimation SleepAnimation { get; set; }

        [JsonProperty("TriggerAnimation")]
        public ReturnAnimation TriggerAnimation { get; set; }

        [JsonProperty("ReturnAnimation")]
        public ReturnAnimation ReturnAnimation { get; set; }
        public LedColorsSeq LedColorsSeq { get; set; }
    }

    public partial class CpIp
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }

    public partial class ReturnAnimation
    {
        [JsonProperty("iv")]
        public List<string> Iv { get; set; }
    }

    public partial class PointId
    {
        [JsonProperty("iv")]
        public int Iv { get; set; }
    }
}
