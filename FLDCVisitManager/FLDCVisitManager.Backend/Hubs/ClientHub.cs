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

        public async Task CPCollectedAsset(BeaconsCPLampIncomingRequest data)//(string beaconID, string lampID, string assetsID)
        {
            await Clients.All.SendAsync("CPCollectedAssetRecived", data);
            await Clients.All.SendAsync("CPCollectedAssetSuccess");
        }
    }
}
