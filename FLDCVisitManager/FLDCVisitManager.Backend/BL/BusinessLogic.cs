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
using System.Net;

namespace FLDCVisitManagerBackend.BL
{
    public class BusinessLogic : IBusinessLogic
    {
        private static ICMSDataHelper _cmsDataHelper;
        private static AppOptionsConfiguration _cmsOptions;
        private readonly IMapper _mapper;
        private static IDBManager _dBManager;

        public BusinessLogic(IMapper mapper, ICMSDataHelper cmsDataHelper,
            AppOptionsConfiguration cmsOptions, IDBManager dBManager, DatabaseOptions connectionString)
        {
            _cmsDataHelper = cmsDataHelper;
            _cmsOptions = cmsOptions;
            _mapper = mapper;
            _cmsDataHelper.ConnectToCMS(_mapper.Map<AppOptions>(_cmsOptions));
            _dBManager = dBManager;
            _dBManager.SetDBConfiguration(connectionString.DefaultConnection);
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
            if (cpAssets != null)
            {
                foreach (var asset in cpAssets.ImageAssets)
                {
                    var image = new CollectibleItemReference();
                    image.CollectabileId = asset.Id == Guid.Empty ? null : asset.Id.ToString();
                    image.CollectabileType = "Image";
                    image.AssetId = asset.Data.ImageAsset?.Iv.FirstOrDefault();
                    output.Add(image);
                }

                foreach (var asset in cpAssets.QuoteAssets)
                {
                    var quote = new CollectibleItemReference();
                    quote.CollectabileId = asset.Id == Guid.Empty ? null : asset.Id.ToString();
                    quote.CollectabileType = "Quote";
                    //quote.AssetId = asset.ImageAsset.Iv.FirstOrDefault();
                    output.Add(quote);
                }
            }
            return output;
        }
        public List<CollectibleItem> ConvertAssetsToCollectibleItems(CollectionPointAssets assets)
        {
            var output = new List<CollectibleItem>();
            if (assets != null)
            {
                foreach (var asset in assets.ImageAssets)
                {
                    var image = new CollectibleItem();
                    image.Id = asset.Id == Guid.Empty ? null : asset.Id.ToString();
                    image.ImageUrl = GenerateImageUrl(asset.Data.ImageAsset?.Iv.FirstOrDefault());
                    image.ValuePairs = Mapper.Map<Dictionary<string, int>>(asset.Data.Values?.Iv);
                    image.Caption = asset.Data.Caption?.Iv;
                    image.Credit = asset.Data.Credit?.Iv;
                    if (asset.Data?.ShopifyIcon != null)
                        image.IconUrl = GenerateImageUrl(asset.Data.ShopifyIcon?.Iv.FirstOrDefault());
                    output.Add(image);
                }

                foreach (var asset in assets.QuoteAssets)
                {
                    var quote = new CollectibleItem();
                    quote.Id = asset.Id == Guid.Empty ? null : asset.Id.ToString();
                    quote.ValuePairs = Mapper.Map<Dictionary<string, int>>(asset.Data.Values?.Iv);
                    if (asset.Data?.ShopifyIcon != null)
                        quote.IconUrl = GenerateImageUrl(asset.Data.ShopifyIcon?.Iv.FirstOrDefault());
                    output.Add(quote);
                }
            }
            return output;
        }

        public string GenerateImageUrl(string hashId)
        {
            if (string.IsNullOrEmpty(hashId))
                return null;
            return $"{_cmsOptions.Url}/api/assets/{_cmsOptions.AppName}/{hashId}";
        }

        public async Task<HttpStatusCode> SetLEDColors(CPRequestParams cpDetails)
        {
            var cpUrl = new Uri(cpDetails.CpIp + "/setLedColorSequence");
            using var client = new HttpClient();
            var json = SerializeObjectToJson<LEDRequestParams>(cpDetails.TriggerAnimation);// JsonConvert.SerializeObject(cpDetails.TriggerAnimation, serializerSettings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var result = await client.PostAsync(cpUrl, data);
                return result.StatusCode;
            }
            catch(Exception ex)
            {
                return HttpStatusCode.NotFound;
            }

        }


        public string SerializeObjectToJson<T>(T objectToSerialize)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(objectToSerialize, serializerSettings);
        }
        public async Task<HttpStatusCode> CollectionPointLamp(string cpId, string lampId)
        {
            var response = HttpStatusCode.BadRequest;
            var cpDetails = await _cmsDataHelper.GetCollectionPointDataById(cpId);
            if (cpDetails != null)
            {
                var cpLampDetails = new CPLampData()
                {
                    CPId = cpId,
                    LampId = lampId,
                    AssetId = cpDetails.Data.CollectionAssets.Iv.FirstOrDefault() //TODO - for now 1 option
                };
                _dBManager.UpdateCollectionPointLampInteraction(cpLampDetails);
                var cpRequest = Mapper.Map<CPRequestParams>(cpDetails);
                response = await Task.Factory.StartNew(() => SetLEDColors(cpRequest)).ContinueWith(t => HttpStatusCode.OK); ;
            }
            return response;
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

        public async Task<List<CollectibleItem>> GetAllCollectibleItems(bool shopify, DateTime? dateLastTaken)
        {
            var allAssets = await _cmsDataHelper.GetAllCollectibleAssets(shopify, dateLastTaken);
            return ConvertAssetsToCollectibleItems(allAssets);
        }

        public string CPLampConnectedValidate()
        {
            return _dBManager.CPLampConnectedValidate();
        }
    }
}
