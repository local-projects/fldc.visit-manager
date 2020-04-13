using System.Collections.Generic;
using Xunit;
using CMSDataLayer.Models;
using FLDCVisitManagerBackend.Models;
using FLDCVisitManagerBackend.BL;
using System.Linq;
using Autofac.Extras.Moq;
using AutoMapper;

namespace FLDCBackend.Test
{
    public class BusinessLogicTests
    {
        [Fact]
        public void ConvertAssetsToIdList_CPAssetsListShouldReturnListOfIds()
        {
            var bl = new BusinessLogic(null, null, null, null, null);
            CollectionPointAssets value = new CollectionPointAssets();
            value.ImageAssets = new List<ImageAssetData>();
            value.QuoteAssets = new List<QuoteAsset>();
            value.ImageAssets.Add(new ImageAssetData()
            {
                Iv = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                Caption = new Caption() { Iv = "Tamar test" },
                Credit = new Caption() { Iv = "Thank you tamar" },
                ImageAsset = new ImageAssetClass() { Iv = new List<string>() { "c8ad4314-fd8b-4200-994c-366bfd87ae13" } },
                Values = new Values()
                {
                    Iv = new List<Iv>() { new Iv() { StringValue = "hope", ValueNumber = "1" },
                new Iv() { StringValue = "Fait", ValueNumber = "2" },
                new Iv() { StringValue = "Unity", ValueNumber = "3" }
                    }
                }
            });
            List<CollectibleItem> expected = new List<CollectibleItem>();
            expected.Add(new CollectibleItem()
            {
                AssetId = "c8ad4314-fd8b-4200-994c-366bfd87ae13",
                CollectabileId = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                CollectabileType = "Image"
            });
            var result = bl.ConvertAssetsToIdList(value);
            Assert.True(result.Count == 1);
            Assert.Equal(result.FirstOrDefault().AssetId , value.ImageAssets.FirstOrDefault().ImageAsset.Iv.FirstOrDefault());
            Assert.Equal(result.FirstOrDefault().CollectabileId , value.ImageAssets.FirstOrDefault().Iv);
/*            Assert.Equal(result.FirstOrDefault().CollectabileType, "Image");*/
        }
    }
}
