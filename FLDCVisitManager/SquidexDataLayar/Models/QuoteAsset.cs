using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSDataLayer.Models
{
    public partial class QuoteAsset
    {
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
