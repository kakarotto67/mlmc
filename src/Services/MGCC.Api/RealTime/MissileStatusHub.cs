using Microsoft.AspNetCore.SignalR;
using Mlmc.Shared.Events;
using System.Threading.Tasks;

namespace Mlmc.MGCC.Api.RealTime
{
    public sealed class MissileStatusHub : Hub
    {
        public const string MissileStatusUpdatedMessage = "MissileStatusUpdated";
        public const string MissileStatusHubUri = "/missileStatusHub";

        public async Task SendMissileStatusUpdatedMessage(
            LaunchedMissileCurrentStatusEvent missileStatusEvent)
        {
            if (missileStatusEvent == null)
            {
                return;
            }

            // Send message to all connected clients
            await Clients.All.SendAsync(MissileStatusUpdatedMessage, missileStatusEvent);
        }
    }
}