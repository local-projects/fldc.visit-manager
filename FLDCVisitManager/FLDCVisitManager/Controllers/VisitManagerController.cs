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

        public VisitManagerController(ILogger<VisitManagerController> logger, IMapper mapper,ICMSDataHelper cmsDataHelper, IOptions<AppOptionsConfiguration> cmsOptions)
        {
            _logger = logger;
            _cmsDataHelper = cmsDataHelper;
            _cmsOptions = cmsOptions.Value;
            _mapper = mapper;
            _cmsDataHelper.ConnectToCMS(_mapper.Map<AppOptions>(_cmsOptions));
        }

        [HttpGet]
        public async void GetCollectionPoints()
        {
            var result = await _cmsDataHelper.GetLedColors();
            var mappedResult = Mapper.Map<LEDRequestParams>(result);
            var cpIP = await _cmsDataHelper.GetCollectionPointsById(1);
        }

        [Route("hello")]
        public void GetCollectionPointTrigger(CPRequestParams req)
        {
            if(worked)
                SetLEDColors();
        }

        public async void SetLEDColors()
        {
            if (worked)
            {
                worked = false;
                //byte[] byteArr = { 0xFF0000, 0x00FF00, 0x0000FF, 0x00 };
                int[] test = { 0xFF0000, 0x00FF00, 0x0000FF, 0xEE82EE, 0xFF };

                var cpIp = "http://192.168.1.185:8080/setLedColorSequence";
                using var client = new HttpClient();
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var json = JsonConvert.SerializeObject(new LEDRequestParams()
                {
                    Pattern = test,
                    Timer = new List<int>() { 500, 500, 500, 500, 300 },
                    Cycles = 3,
                    Run = true
                }, serializerSettings);
                //var json = JsonConvert.SerializeObject();

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(cpIp, data);
            }

        }
    }
}
