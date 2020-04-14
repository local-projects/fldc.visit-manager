using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Squidex.ClientLibrary;

namespace FLDCVisitManager.CMSDataLayar.DTO
{
    public class LedColorsSeq : SquidexEntityBase<LedColorsSeqData>
    {

    }

    public partial class LedColorsSeqData
    {
        [JsonProperty("ColorSeq")]
        public RSeq ColorSeq { get; set; }

        [JsonProperty("ColorSeqBrightness")]
        public ColorSeqBrightness ColorSeqBrightness { get; set; }

        [JsonProperty("Cycles")]
        public Cycles Cycles { get; set; }

        [JsonProperty("Name")]
        public Name Name { get; set; }

        [JsonProperty("TimerSeq")]
        public TimerSeq TimerSeq { get; set; }
    }

    public partial class RSeq
    {
        [JsonProperty("iv")]
        public List<ColorSeqIv> Iv { get; set; }
    }

    public partial class ColorSeqIv
    {
        [JsonProperty("Colors")]
        public string Colors { get; set; }
    }

    public partial class ColorSeqBrightness
    {
        [JsonProperty("iv")]
        public List<ColorSeqBrightnessIv> Iv { get; set; }
    }

    public partial class ColorSeqBrightnessIv
    {
        [JsonProperty("Brightness")]
        public long Brightness { get; set; }

        [JsonProperty("BrightnessTimer")]
        public long BrightnessTimer { get; set; }
    }

    public partial class Cycles
    {
        [JsonProperty("iv")]
        public long Iv { get; set; }
    }

    public partial class Name
    {
        [JsonProperty("iv")]
        public string Iv { get; set; }
    }
    public partial class TimerSeq
    {
        [JsonProperty("iv")]
        public List<TimerSeqIv> Iv { get; set; }
    }

    public partial class TimerSeqIv
    {
        [JsonProperty("TimerForEachColor")]
        public long TimerForEachColor { get; set; }
    }
}
