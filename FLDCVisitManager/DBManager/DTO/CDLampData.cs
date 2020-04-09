using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBManager.Models
{
    public class CDLampData
    {
        [JsonProperty("lamp_id")]
        public string LampId { get; set; }
        public int Port { get; set; }
        public string Status { get; set; }
        public int Battery { get; set; }
        [JsonProperty("fw_version")]
        public string FwVersion { get; set; }
        [JsonProperty("sq_version")]
        public string SeqVersion { get; set; }
    }
}
