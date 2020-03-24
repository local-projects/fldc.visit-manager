using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CMSDataLayer;
using CMSDataLayer.Models;
using FLDCVisitManagerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FLDCVisitManager.Controllers
{
    [ApiController]
    [Route("")]
    public class VisitManagerController : ControllerBase
    {

        private readonly ILogger<VisitManagerController> _logger;
        private static bool worked = true;
        private static ICMSDataHelper _cmsDataHelper;
        private static AppOptionsConfiguration _cmsOptions;
        private readonly IMapper _mapper;

        public VisitManagerController(ILogger<VisitManagerController> logger, IMapper mapper, ICMSDataHelper cmsDataHelper, IOptions<AppOptionsConfiguration> cmsOptions)
        {
            _logger = logger;
            _cmsDataHelper = cmsDataHelper;
            _cmsOptions = cmsOptions.Value;
            _mapper = mapper;
            _cmsDataHelper.ConnectToCMS(_mapper.Map<AppOptions>(_cmsOptions));
        }

        [HttpGet]
        public async void GetCollectionPointDetails()//(CPLampIncomingRequest req)
        {
            var cpDetails = await _cmsDataHelper.GetCollectionPointsById(1); //req.LampId
            var cpRequest = Mapper.Map<CPRequestParams>(cpDetails);
            SetLEDColors(cpRequest);
        }

        [Route("hello")]
        public void GetCollectionPointHeartBeat(CPHeartBeatIncomingRequestParams req)
        {
            /*            if(worked)
                            SetLEDColors();*/
        }

        public async void SetLEDColors(CPRequestParams cpDetails)
        {
            //int[] test = { 0xFF0000, 0x00FF00, 0x0000FF, 0xEE82EE, 0xFF };
            var cpUrl = new Uri(cpDetails.CpIp + "/setLedColorSequence");
            using var client = new HttpClient();
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(cpDetails.TriggerAnimation, serializerSettings);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(cpUrl, data);
        }
    }
}
