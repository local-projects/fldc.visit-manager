using FLDCVisitManagerBackend.Models;
using MessagePack;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManager.Backend.Hubs
{
    public class ClientHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendConnectionId(string connectionId)
        {
            await Clients.All.SendAsync("setClientMessage", "A connection with ID '" + connectionId + "' has just connected");
        }

        public async Task CPCollectedAsset(string beaconID, string lampID, string assetsID)
            //(DataModel req)
        {
            /*await Clients.All.SendAsync("CPCollectedAssetRecived", req);
*/            await Clients.All.SendAsync("CPCollectedAssetSuccess");
        }

/*        [MessagePackObject]
        public class DataModel
        {
            [Key("beaconId")]
            int BeaconID { get; set; }
            [Key("lampId")]
            int LampID { get; set; }
            [Key("assetsId")]
            int AssetsID { get; set; }
        }*/
    }
}
