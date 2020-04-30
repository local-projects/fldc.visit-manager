using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLDCVisitManager.Backend.Hubs
{
    public class ClientHub : Hub
    {
        public async Task SendConnectionId(string connectionId)
        {
            await Clients.All.SendAsync("setClientMessage", "A connection with ID '" + connectionId + "' has just connected");
        }

        public async Task CPLamp(string beaconsId, string lampId)
        {
            await Clients.All.SendAsync("cpLamp", "cp in beacons id '" + beaconsId + "' we just triggered by lamp id: " + lampId);
        }
    }
}
