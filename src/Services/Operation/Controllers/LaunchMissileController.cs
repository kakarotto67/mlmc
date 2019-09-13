using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mlmc.EnterpriseServiceBus.RabbitMq.MessageBus;
using Mlmc.Operation.Models;
using Mlmc.Operation.MongoDb;
using Mlmc.Shared.Events;
using System;

namespace Mlmc.Operation.Controllers
{
    [EnableCors("Mlmc.Operation.LaunchMissile")]
    [Route("api/[controller]")]
    [ApiController]
    public class LaunchMissileController : ControllerBase
    {
        private readonly MissileService missileService;
        private readonly MessageBus messageBus;
        private readonly IConfiguration configuration;

        public LaunchMissileController(MissileService missileService,
            MessageBus messageBus,
            IConfiguration configuration)
        {
            this.missileService = missileService;
            this.messageBus = messageBus;
            this.configuration = configuration;
        }

        // POST api/launchmissile
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Missile missile)
        {
            if (missile?.ServiceIdentityNumber == null)
            {
                return BadRequest();
            }

            var missileFromDatabase = missileService.Get(missile.ServiceIdentityNumber);
            if (missileFromDatabase == null)
            {
                return BadRequest();
            }

            var eventMessage = new LaunchMissileEvent
            {
                MissileServiceIdentityNumber = missile.ServiceIdentityNumber,
                MissileName = missileFromDatabase.Name,
                LaunchDate = DateTime.UtcNow,
                TargetLocation = missile.TargetLocation
            };

            var launchMissileQueue = configuration.GetValue<String>("MessageBusConfiguration:Queues:LaunchCenterQueue");

            // Publish launch missile message
            messageBus.PublishMessage(launchMissileQueue, eventMessage);

            // Update status of the missile to 'Launched'
            var updateStatus = missileService.Update(missile.ServiceIdentityNumber, MissileStatus.Launched);
            if (updateStatus)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}