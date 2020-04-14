using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class LEDRequestParams
    {
        public int[] Pattern { get; set; }
        public List<int> Timer { get; set; }
        public long Cycles { get; set; }
        public bool Run { get; set; } = true;
        public string SeqName { get; set; }
    }
}
