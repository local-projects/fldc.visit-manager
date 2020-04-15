using Newtonsoft.Json;
using Squidex.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public class QuoteAsset : Content<QuoteAssetData>
    {

    }
    public partial class QuoteAssetData
    {
        public string Iv { get; set; }

        [JsonProperty("Quote")]
        public Quote Quote { get; set; }

        [JsonProperty("Values")]
        public Values Values { get; set; }
    }

    public partial class Quote
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }
}
