using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FLDCVisitManagerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public VisitManagerController(ILogger<VisitManagerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public void GetCollectionPoints()
        {

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
                /*                byte[] byteArr = { 0xFF0000, 0x00FF00, 0x0000FF, 0x00 };*/
                int[] test = { 0xFF0000, 0x00FF00, 0x0000FF, 0xEE82EE, 0x64 };

                worked = false;
                var cpIp = "http://192.168.1.185:8080/setLedColorSequence";
                using var client = new HttpClient();
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                var json = JsonConvert.SerializeObject(new LEDRequestParams()
                {
                    Pattern = test,
                    Timer = new List<int>() { 500, 500, 500, 500 },
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
