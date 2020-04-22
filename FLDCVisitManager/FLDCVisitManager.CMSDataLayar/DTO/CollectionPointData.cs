using Newtonsoft.Json;
using Squidex.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public class CollectionPoint : Content<CollectionPointData>
    {

    }
    public partial class CollectionPointData
    {
        [JsonProperty("pointID")]
        public PointId PointId { get; set; }

        [JsonProperty("pointName")]
        public CpIp PointName { get; set; }

        [JsonProperty("pointAsset")]
        public References PointAsset { get; set; }

        [JsonProperty("CPIp")]
        public CpIp CpIp { get; set; }

        [JsonProperty("SleepAnimation")]
        public References SleepAnimation { get; set; }

        [JsonProperty("TriggerAnimation")]
        public References TriggerAnimation { get; set; }

        [JsonProperty("ReturnAnimation")]
        public References ReturnAnimation { get; set; }
        [JsonProperty("CollectionAssets")]
        public References CollectionAssets { get; set; }
        public LedColorsSeq TriggerLedColorsSeq { get; set; }
        public LedColorsSeq SleepLedColorsSeq { get; set; }
    }

    public partial class CpIp
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }

    public partial class References
    {
        [JsonProperty("iv")]
        public List<string> Iv { get; set; }
    }

    public partial class PointId
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }
}
