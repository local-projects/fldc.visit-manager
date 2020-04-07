using System;
using System.Collections.Generic;
using System.Text;

namespace CMSDataLayer.Models
{
    public class CollectionPointAssets
    {
        public List<ImageAssetData> ImageAssets { get; set; }
        public List<QuoteAsset> QuoteAssets { get; set; }

        public CollectionPointAssets()
        {
            ImageAssets = new List<ImageAssetData>();
            QuoteAssets = new List<QuoteAsset>();
        }
    }
}
