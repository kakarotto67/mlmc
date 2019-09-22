using Microsoft.AspNetCore.SignalR;
using Mlmc.Shared.Events;
using System.Threading.Tasks;

namespace Mlmc.MGCC.Api.RealTime
{
    public sealed class MissileStatusHub : Hub
    {
        private const string HubsRoot = "/hubs";
        public static readonly string MissileStatusHubUri = $"{HubsRoot}/missileStatusHub";
        public const string MissileStatusUpdatedMessage = "MissileStatusUpdated";

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