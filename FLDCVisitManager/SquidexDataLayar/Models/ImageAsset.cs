using Newtonsoft.Json;
using Squidex.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMSDataLayer.Models
{
    public class ImageAsset : SquidexEntityBase<ImageAssetData>
    {

    }
    public partial class ImageAssetData
    {
        [JsonProperty("ImageAsset")]
        public ImageAssetClass ImageAsset { get; set; }

        [JsonProperty("Caption")]
        public Caption Caption { get; set; }

        [JsonProperty("Credit")]
        public Caption Credit { get; set; }

        [JsonProperty("Values")]
        public Values Values { get; set; }
    }

    public partial class Caption
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }

    public partial class ImageAssetClass
    {
        [JsonProperty("iv")]
        public List<string> Iv { get; set; }
    }

    public partial class Values
    {
        [JsonProperty("iv")]
        public List<Iv> Iv { get; set; }
    }

    public partial class Iv
    {
        [JsonProperty("StringValue")]
        public string StringValue { get; set; }

        [JsonProperty("ValueNumber")]
        public string ValueNumber { get; set; }
    }
}

