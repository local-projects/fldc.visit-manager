using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public partial class QuoteAsset
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
