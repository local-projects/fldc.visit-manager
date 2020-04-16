using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManagerBackend.Models
{
    public class CollectibleItemReference
    {
        public string CollectabileId { get; set; }
        public string CollectabileType { get; set; }
        public string AssetId { get; set; }
    }

    public class CollectibleItem
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public Dictionary<string, int> ValuePairs { get; set; }
        public string IconUrl { get; set; }
        public string Caption { get; set; }
        public string Credit { get; set; }
    }
}
