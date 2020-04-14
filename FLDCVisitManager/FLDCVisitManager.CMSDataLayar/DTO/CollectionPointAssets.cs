using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public class CollectionPointAssets
    {
        public List<ImageAssetData> ImageAssets { get; set; }
        public List<QuoteAssetData> QuoteAssets { get; set; }

        public CollectionPointAssets()
        {
            ImageAssets = new List<ImageAssetData>();
            QuoteAssets = new List<QuoteAssetData>();
        }
    }
}
