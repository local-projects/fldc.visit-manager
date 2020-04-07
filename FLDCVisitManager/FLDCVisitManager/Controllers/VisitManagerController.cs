using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CMSDataLayer;
using CMSDataLayer.Models;
using DBManager;
using DBManager.Models;
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
        private static IDBManager _dBManager;

        public VisitManagerController(ILogger<VisitManagerController> logger, IMapper mapper, ICMSDataHelper cmsDataHelper,
            IOptions<AppOptionsConfiguration> cmsOptions, IDBManager dBManager, IOptions<DataBaseOptions> connectionString)
        {
            _logger = logger;
            _cmsDataHelper = cmsDataHelper;
            _cmsOptions = cmsOptions.Value;
            _mapper = mapper;
            _cmsDataHelper.ConnectToCMS(_mapper.Map<AppOptions>(_cmsOptions));
            _dBManager = dBManager;
            _dBManager.SetDBConfiguration(connectionString.Value.DefaultConnection);
        }

        [Route("cpLamp")]
        [HttpGet]
        public async void GetCollectionPointDetails()//(CPLampIncomingRequest req)
        {
            var req = new CPLampIncomingRequest() { Id = "1", LampId = "1" };
            _dBManager.UpdateCollectionPointLampInteraction(Mapper.Map<CPLampData>(req));
            var cpDetails = await _cmsDataHelper.GetCollectionPointsById(req.Id);
            var cpRequest = Mapper.Map<CPRequestParams>(cpDetails);
            SetLEDColors(cpRequest);
        }

        [Route("cd_lamp")]
        public IActionResult ChargerDockerLamp(ChargerDockerLampIncomingRequest cdLampReq)
        {
            var result = _dBManager.ChargerDockerLampRecognized(Mapper.Map<CDLampData>(cdLampReq));
            if (result.Status != 200)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Message);
        }

        [Route("hello")]
        public IActionResult GetCollectionPointHeartBeat(CPHeartBeatIncomingRequestParams req)
        {
            var response = _dBManager.UpdateCollectionPoint(req.ID, req.FW);
            if (response.Status != 200)
            {
                return NotFound(response.Message);
            }
            return Ok(response.Message);
        }

        [Route("fwUpdate")]
        public IActionResult GetFirmwareFtpDetails()
        {
            var result = _dBManager.GetFirmwareFtpDetails();
            return new JsonResult(result);
        }

        public async void SetLEDColors(CPRequestParams cpDetails)
        {
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
