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
        public async Task<List<CollectibleItem>> GetVisitorCollectabileItems(string lampId)
        {
            var result = _dBManager.GetVisitorCollectabileItems(lampId);
            if (result != null)
            {
                var assetsData = await _cmsDataHelper.GetCollectionAssets(result);
                var output = ConvertAssetsToIdList(assetsData);
                return output;
            }
            else
                return new List<CollectibleItem>();
        }

        public List<CollectibleItem> ConvertAssetsToIdList(CollectionPointAssets cpAssets)
        {
            var output = new List<CollectibleItem>();
            foreach(var asset in cpAssets.ImageAssets)
            {
                var image = new CollectibleItem();
                image.CollectabileId = asset.Iv;
                image.CollectabileType = "Image";
                image.AssetId = asset.ImageAsset.Iv.FirstOrDefault();
                output.Add(image);
            }

            foreach (var asset in cpAssets.QuoteAssets)
            {
                var quote = new CollectibleItem();
                quote.CollectabileId = asset.Iv;
                quote.CollectabileType = "Quote";
                //quote.AssetId = asset.ImageAsset.Iv.FirstOrDefault();
                output.Add(quote);
            }
            return output;
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

        public void GetAllCollectabileItems(DateTime? dateLastTaken)
        {

        }
    }
}
