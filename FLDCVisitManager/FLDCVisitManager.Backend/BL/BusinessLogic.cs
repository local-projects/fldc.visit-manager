using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FLDCVisitManager.CMSDataLayar;
using FLDCVisitManager.CMSDataLayar.DTO;
using DBManager;
using DBManager.Models;
using FLDCVisitManagerBackend.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FLDCVisitManagerBackend.BL
{
    public class BusinessLogic : IBusinessLogic
    {
        private static ICMSDataHelper _cmsDataHelper;
        private static AppOptionsConfiguration _cmsOptions;
        private readonly IMapper _mapper;
        private static IDBManager _dBManager;

        public BusinessLogic(IMapper mapper, ICMSDataHelper cmsDataHelper,
            IOptions<AppOptionsConfiguration> cmsOptions, IDBManager dBManager, IOptions<DataBaseOptions> connectionString)
        {
            _cmsDataHelper = cmsDataHelper;
            _cmsOptions = cmsOptions.Value;
            _mapper = mapper;
            _cmsDataHelper.ConnectToCMS(_mapper.Map<AppOptions>(_cmsOptions));
            _dBManager = dBManager;
            _dBManager.SetDBConfiguration(connectionString.Value.DefaultConnection);
        }
        public async Task<List<CollectibleItemReference>> GetVisitorCollectibleItems(string lampId)
        {
            var result = _dBManager.GetVisitorCollectibleItems(lampId);
            if (result != null)
            {
                var assetsData = await _cmsDataHelper.GetCollectionAssets(result);
                var output = ConvertAssetsToIdList(assetsData);
                return output;
            }
            else
                return new List<CollectibleItemReference>();
        }

        public List<CollectibleItemReference> ConvertAssetsToIdList(CollectionPointAssets cpAssets)
        {
            var output = new List<CollectibleItemReference>();
            foreach(var asset in cpAssets.ImageAssets)
            {
                var image = new CollectibleItemReference();
                image.CollectabileId = asset.Id;
                image.CollectabileType = "Image";
                image.AssetId = asset.Data.ImageAsset.Iv.FirstOrDefault();
                output.Add(image);
            }

            foreach (var asset in cpAssets.QuoteAssets)
            {
                var quote = new CollectibleItemReference();
                quote.CollectabileId = asset.Id;
                quote.CollectabileType = "Quote";
                //quote.AssetId = asset.ImageAsset.Iv.FirstOrDefault();
                output.Add(quote);
            }
            return output;
        }
        public List<CollectibleItem> ConvertAssetsToCollectibleItems(CollectionPointAssets assets)
        {
            var output = new List<CollectibleItem>();
            foreach (var asset in assets.ImageAssets)
            {
                var image = new CollectibleItem();
                image.Id = asset.Id;
                image.ImageUrl = GenerateImageUrl(asset.Data.ImageAsset.Iv.FirstOrDefault());
                image.ValuePairs = Mapper.Map<Dictionary<string, int>>(asset.Data.Values.Iv);
                output.Add(image);
            }

            foreach (var asset in assets.QuoteAssets)
            {
                var quote = new CollectibleItem();
                quote.Id = asset.Id;
                quote.ValuePairs = Mapper.Map<Dictionary<string, int>>(asset.Data.Values.Iv);
                output.Add(quote);
            }
            return output;
        }

        public string GenerateImageUrl(string hashId)
        {
            return $"{_cmsOptions.Url}/api/assets/{_cmsOptions.AppName}/{hashId}";
        }

        public async void SetLEDColors(CPRequestParams cpDetails)
        {
            var cpUrl = new Uri(cpDetails.CpIp + "/setLedColorSequence");
            using var client = new HttpClient();
            var json = SerializeObjectToJson<LEDRequestParams>(cpDetails.TriggerAnimation);// JsonConvert.SerializeObject(cpDetails.TriggerAnimation, serializerSettings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(cpUrl, data);
        }

        public string SerializeObjectToJson<T>(T objectToSerialize)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(objectToSerialize, serializerSettings);
        }

        public async void CollectionPointLamp(string cpId, string lampId)
        {
            var cpDetails = await _cmsDataHelper.GetCollectionPointDataById(cpId);
            var cpLampDetails = new CPLampData()
            {
                CPId = cpId,
                LampId = lampId,
                AssetId = cpDetails.Data.CollectionAssets.Iv.FirstOrDefault() //TODO - for now 1 option
            };
            _dBManager.UpdateCollectionPointLampInteraction(cpLampDetails);
            var cpRequest = Mapper.Map<CPRequestParams>(cpDetails);
            SetLEDColors(cpRequest);
        }

        public ResponseResult ChargerDockerLamp(ChargerDockerLampIncomingRequest cdLampReq)
        {
            return _dBManager.ChargerDockerLampRecognized(Mapper.Map<CDLampData>(cdLampReq));
        }
    
        public ResponseResult UpdateCollectionPointHeartBeat(CPHeartBeatIncomingRequestParams req)
        {
            return _dBManager.UpdateCollectionPoint(req.ID, req.FW);
        }

        public FTPDetails GetFirmwareFtpDetails()
        {
            return _dBManager.GetFirmwareFtpDetails();
        }

        public async Task<List<CollectibleItem>> GetAllCollectibleItems(DateTime? dateLastTaken)
        {
            var allAssets = await _cmsDataHelper.GetAllCollectibleAssets(dateLastTaken);
            return ConvertAssetsToCollectibleItems(allAssets);
        }
    }
}
