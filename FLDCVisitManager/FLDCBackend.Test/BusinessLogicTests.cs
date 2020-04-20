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

namespace FLDCBackend.Test
{
    public class BusinessLogicTests
    {
        private static BusinessLogic bl;
        private static IConfigurationRoot _config;

        public BusinessLogicTests()
        {

            _config = new ConfigurationBuilder()
             .AddJsonFile("test-app.configuration.json")
             .Build();

            var mapperMock = new Mock<IMapper>();
            var cmsMock = new Mock<ICMSDataHelper>();
            var cmsOptions = new AppOptionsConfiguration()
            {
                AppName = _config["CMSOptions:AppName"].ToString(),
                Url = _config["CMSOptions:Url"].ToString()
            };
            cmsMock.Setup(x => x.ConnectToCMS(mapperMock.Object.Map<AppOptions>(cmsOptions)));
            var dbMock = new Mock<IDBManager>();
            var dbOptionsMock = new Mock<DatabaseOptions>();
            dbMock.Setup(x => x.SetDBConfiguration(dbOptionsMock.Object.DefaultConnection));

            bl = new BusinessLogic(mapperMock.Object, cmsMock.Object, cmsOptions, dbMock.Object, dbOptionsMock.Object);
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
            List<CollectibleItemReference> expected = new List<CollectibleItemReference>();
            expected.Add(new CollectibleItemReference()
            {
                AssetId = "c8ad4314-fd8b-4200-994c-366bfd87ae13",
                CollectabileId = "c8ad4314-fd8b-4200-994c-366bfd87ae12",
                CollectabileType = "Image"
            });
            var result = bl.ConvertAssetsToIdList(value);
            Assert.True(result.Count == 1);
            Assert.Equal(result.FirstOrDefault().AssetId, value.ImageAssets.FirstOrDefault().Data.ImageAsset.Iv.FirstOrDefault());
            Assert.Equal(result.FirstOrDefault().CollectabileId, value.ImageAssets.FirstOrDefault().Id.ToString());
            /*            Assert.Equal(result.FirstOrDefault().CollectabileType, "Image");*/
        }
    }
}
