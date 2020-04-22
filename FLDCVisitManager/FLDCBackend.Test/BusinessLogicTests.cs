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
using Xunit.Extensions;

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

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCPToIdListData), MemberType = typeof(TestDataGenerator))]
        public void ConvertAssetsToIdList_CPAssetsListShouldReturnListOfIds(CollectionPointAssets value, List<CollectibleItemReference> expected)
        {
            /*            var value = GetCPAssets();
                        var expected = GetExpectedAssetIdList();*/
            var result = bl.ConvertAssetsToIdList(value);
            Assert.True(result.Count == expected.Count);
            Assert.Equal(expected.FirstOrDefault()?.AssetId, result.FirstOrDefault()?.AssetId);
            Assert.Equal(expected.FirstOrDefault()?.CollectabileId, result.FirstOrDefault()?.CollectabileId);
            Assert.Equal(expected.FirstOrDefault()?.CollectabileType, result.FirstOrDefault()?.CollectabileType);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCPToAssetListData), MemberType = typeof(TestDataGenerator))]
        public void ConvertAssetsToCollectibleItems_CPAssetsListShouldReturnListOfCollectibleItems(CollectionPointAssets value, List<CollectibleItem> expected)
        {
            /*            var value = GetCPAssets();
                        List<CollectibleItem> expected = new List<CollectibleItem>();
                        GetExpectedCollectabileItems(expected);*/
            var result = bl.ConvertAssetsToCollectibleItems(value);
            Assert.True(result.Count == expected.Count);
            Assert.Equal(expected.FirstOrDefault()?.Id, result.FirstOrDefault()?.Id);
            Assert.Equal(expected.FirstOrDefault()?.Caption, result.FirstOrDefault()?.Caption);
            Assert.Equal(expected.FirstOrDefault()?.Credit, result.FirstOrDefault()?.Credit);
            Assert.Equal(expected.FirstOrDefault()?.IconUrl, result.FirstOrDefault()?.IconUrl);
            Assert.Equal(expected.FirstOrDefault()?.ImageUrl, result.FirstOrDefault()?.ImageUrl);
            if (result.Count > 0)
            {
                foreach (var item in result.Select((x, i) => new { Value = x, Index = i }) )
                {
                    if(item.Value?.ValuePairs != null)
                    {
                        foreach (var pair in item.Value?.ValuePairs)
                        {
                            Assert.Equal(expected[item.Index]?.ValuePairs[pair.Key], pair.Value);
                        }

                    }
                }
            }
        }

        [Fact]
        public void SerializeObjectToJson_SimpleObjectShouldReturnObjectString()
        {

        }
    }
}
