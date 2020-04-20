using System.Collections.Generic;
using Xunit;
using FLDCVisitManager.CMSDataLayar.DTO;
using FLDCVisitManagerBackend.Models;
using FLDCVisitManagerBackend.BL;
using System.Linq;
using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using FLDCVisitManager.CMSDataLayar;
using Microsoft.Extensions.Options;
using DBManager;
using Microsoft.Extensions.Configuration;
using System;
using FLDCVisitManagerBackend.Helpers;

namespace FLDCBackend.Test
{
    public class BusinessLogicTests
    {
        private static BusinessLogic bl;
        private static IConfigurationRoot _config;
        private static IMapper mapper;
        public BusinessLogicTests()
        {

            _config = new ConfigurationBuilder()
             .AddJsonFile("test-app.configuration.json")
             .Build();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappingModels());
            });
            mapper = mockMapper.CreateMapper();
            Mapper.Reset();
            Mapper.Initialize(x =>
                x.AddProfile<AutoMappingModels>()
            );
            mapper.ConfigurationProvider.AssertConfigurationIsValid(); //AutoMapping test
            var cmsMock = new Mock<ICMSDataHelper>();
            var cmsOptions = new AppOptionsConfiguration()
            {
                AppName = _config["CMSOptions:AppName"].ToString(),
                Url = _config["CMSOptions:Url"].ToString()
            };
            cmsMock.Setup(x => x.ConnectToCMS(mapper.Map<AppOptions>(cmsOptions)));
            var dbMock = new Mock<IDBManager>();
            var dbOptionsMock = new Mock<DatabaseOptions>();
            dbMock.Setup(x => x.SetDBConfiguration(dbOptionsMock.Object.DefaultConnection));

            bl = new BusinessLogic(mapper, cmsMock.Object, cmsOptions, dbMock.Object, dbOptionsMock.Object);
        }

        [Theory]
        [InlineData("1234", "https://cloud.squidex.io/api/assets/fldc-prod/1234")]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void GenerateImageUrl_SimpleStringShouldReturnUrlPath(string value, string expected)
        {
            var result = bl.GenerateImageUrl(value);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertAssetsToIdList_CPAssetsListShouldReturnListOfIds()
        {
            CollectionPointAssets value = new CollectionPointAssets();
            GetCPAssets(value);
            List<CollectibleItemReference> expected = new List<CollectibleItemReference>();
            GetExpectedAssetIdList(expected);
            var result = bl.ConvertAssetsToIdList(value);
            Assert.True(result.Count == 1);
            Assert.Equal(expected.FirstOrDefault().AssetId, result.FirstOrDefault().AssetId);
            Assert.Equal(expected.FirstOrDefault().CollectabileId, result.FirstOrDefault().CollectabileId);
            Assert.Equal(expected.FirstOrDefault().CollectabileType, result.FirstOrDefault().CollectabileType);
        }

        [Fact]
        public void ConvertAssetsToCollectibleItems_CPAssetsListShouldReturnListOfCollectibleItems()
        {
            CollectionPointAssets value = new CollectionPointAssets();
            GetCPAssets(value);
            List<CollectibleItem> expected = new List<CollectibleItem>();
            GetExpectedCollectabileItems(expected);
            var result = bl.ConvertAssetsToCollectibleItems(value);
            Assert.True(result.Count == 1);
            Assert.Equal(expected.FirstOrDefault().Id, result.FirstOrDefault().Id);
            Assert.Equal(expected.FirstOrDefault().Caption, result.FirstOrDefault().Caption);
            Assert.Equal(expected.FirstOrDefault().Credit, result.FirstOrDefault().Credit);
            Assert.Equal(expected.FirstOrDefault().IconUrl, result.FirstOrDefault().IconUrl);
            Assert.Equal(expected.FirstOrDefault().ImageUrl, result.FirstOrDefault().ImageUrl);
            foreach(var item in result.FirstOrDefault().ValuePairs)
            {
                Assert.Equal(expected.FirstOrDefault().ValuePairs[item.Key], item.Value);
            }
        }

        private void GetExpectedCollectabileItems(List<CollectibleItem> expected)
        {
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
        }

        private void GetExpectedAssetIdList(List<CollectibleItemReference> expected)
        {
            expected.Add(new CollectibleItemReference()
            {
                AssetId = "c8ad4314-fd8b-4200-994c-366bfd87ae13",
                CollectabileId = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                CollectabileType = "Image"
            });
        }

        private void GetCPAssets(CollectionPointAssets value)
        {
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
        }

        [Fact]
        public void SerializeObjectToJson_SimpleObjectShouldReturnObjectString()
        {

        }
    }
}
