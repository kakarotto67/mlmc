using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Operation.Models;
using Operation.MongoDb;

namespace Operation.Controllers
{
    [EnableCors("Operation.DeploymentPlatforms")]
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