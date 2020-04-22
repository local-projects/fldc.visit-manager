using FLDCVisitManager.CMSDataLayar.DTO;
using FLDCVisitManagerBackend.Models;
using System.Collections;
using System.Collections.Generic;

namespace FLDCBackend.Test
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetCPToIdListData()
        {
            yield return new object[]
            {
                GetCPAssets(),
                GetExpectedAssetIdList()
            };

            yield return new object[]
            {
                null,
                new List<CollectibleItemReference>()
            };

            yield return new object[]
            {
                new CollectionPointAssets(),
                new List<CollectibleItemReference>(),
            };

            yield return new object[]
            {
                new CollectionPointAssets() {
                    ImageAssets = new List<ImageAsset>() { new ImageAsset() },
                    QuoteAssets = new List<QuoteAsset>() {new QuoteAsset() }
                },
                new List<CollectibleItemReference>() { new CollectibleItemReference() { CollectabileType = "Image" }, new CollectibleItemReference() { CollectabileType = "Quote" } },
            };
        }

        public static IEnumerable<object[]> GetCPToAssetListData()
        {
            yield return new object[]
            {
                GetCPAssets(),
                GetExpectedCollectabileItems(),
            };

            yield return new object[]
            {
                null,
                new List<CollectibleItem>(),
            };

            yield return new object[]
            {
                new CollectionPointAssets(),
                new List<CollectibleItem>()
            };

            yield return new object[]
            {
                new CollectionPointAssets() {
                    ImageAssets = new List<ImageAsset>() { new ImageAsset() },
                    QuoteAssets = new List<QuoteAsset>() {new QuoteAsset() }
                },
                new List<CollectibleItem>() {new CollectibleItem(), new CollectibleItem()}
            };
        }

        private static List<CollectibleItem> GetExpectedCollectabileItems()
        {
            List<CollectibleItem> expected = new List<CollectibleItem>();
            var item = new CollectibleItem()
            {
                Id = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                Credit = "Thank you tamar",
                Caption = "Tamar test",
                IconUrl = null,
                ValuePairs = new Dictionary<string, int>() { { "hope", 1 }, { "Fait", 2 }, { "Unity", 3 } },
                ImageUrl = "https://cloud.squidex.io/api/assets/fldc-prod/c8ad4314-fd8b-4200-994c-366bfd87ae13"
            };
            expected.Add(item);
            return expected;
        }

        private static List<CollectibleItemReference> GetExpectedAssetIdList()
        {
            List<CollectibleItemReference> expected = new List<CollectibleItemReference>();
            expected.Add(new CollectibleItemReference()
            {
                AssetId = "c8ad4314-fd8b-4200-994c-366bfd87ae13",
                CollectabileId = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                CollectabileType = "Image"
            });
            return expected;
        }

        private static CollectionPointAssets GetCPAssets()
        {
            CollectionPointAssets value = new CollectionPointAssets();
            value.ImageAssets = new List<ImageAsset>();
            value.QuoteAssets = new List<QuoteAsset>();
            value.ImageAssets.Add(new ImageAsset()
            {
                Id = new System.Guid("c8ad4314-fd8b-4200-994c-366bfd87ae12"),
                Data =
                {
                    Caption = new Caption() { Iv = "Tamar test" },
                    Credit = new Caption() { Iv = "Thank you tamar" },
                    ImageAsset = new ImageAssetClass() { Iv = new List<string>() { "c8ad4314-fd8b-4200-994c-366bfd87ae13" } },
                    Values = new Values()
                    {
                        Iv = new List<Iv>() {   new Iv() { StringValue = "hope", ValueNumber = "1" },
                                                new Iv() { StringValue = "Fait", ValueNumber = "2" },
                                                new Iv() { StringValue = "Unity", ValueNumber = "3" }
                    }
                    }
                }

            });
            return value;
        }
    }
}
