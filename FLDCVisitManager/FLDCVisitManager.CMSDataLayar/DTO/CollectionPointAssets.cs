using System;
using System.Collections.Generic;
using System.Text;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public class CollectionPointAssets
    {
        public List<ImageAsset> ImageAssets { get; set; }
        public List<QuoteAsset> QuoteAssets { get; set; }

        public CollectionPointAssets()
        {
            ImageAssets = new List<ImageAsset>();
            QuoteAssets = new List<QuoteAsset>();
        }
    }
}
