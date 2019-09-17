using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mlmc.MGCC.Api.RealTime;
using Mlmc.Shared.Events;

namespace MGCC.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MgccController : ControllerBase
    {
        private readonly IHubContext<MissileStatusHub> hubContext;

        public MgccController(IHubContext<MissileStatusHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        // POST api/mgcc
        [HttpPost]
        public async Task Post([FromBody] LaunchedMissileCurrentStatusEvent eventMessage)
        {
            // Send SignalR message with current missile status
            await hubContext.Clients.All.SendAsync(MissileStatusHub.MissileStatusUpdatedMessage,
                eventMessage);
        }
    }
}