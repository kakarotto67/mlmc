using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mlmc.Operation.Models;
using Mlmc.Operation.MongoDb;

namespace Mlmc.Operation.Controllers
{
    [EnableCors("Mlmc.Operation.DeploymentPlatforms")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeploymentPlatformsController : ControllerBase
    {
        private readonly DeploymentPlatformService deploymentPlatformService;

        public DeploymentPlatformsController(DeploymentPlatformService deploymentPlatformService)
        {
            this.deploymentPlatformService = deploymentPlatformService;
        }

        // GET api/deploymentplatforms
        [HttpGet]
        public ActionResult<IEnumerable<DeploymentPlatform>> Get()
        {
            var deploymentPlatforms = deploymentPlatformService.Get();

            return Ok(deploymentPlatforms);
        }
    }
}