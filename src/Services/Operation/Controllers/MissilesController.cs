﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Mlmc.Shared.Models;
using Mlmc.Operation.MongoDb;
using Mlmc.Operation.Models;

namespace Mlmc.Operation.Controllers
{
    [EnableCors("Mlmc.Operation.Missiles")]
    [Route("api/[controller]")]
    [ApiController]
    public class MissilesController : ControllerBase
    {
        private readonly MissileService missileService;

        public MissilesController(MissileService missileService)
        {
            this.missileService = missileService;
        }

        // GET api/missiles
        [HttpGet]
        public ActionResult<IEnumerable<Missile>> Get(MissileStatus? status = null)
        {
            var missiles = status != null ?
                missileService.Get(status.Value)
                : missileService.Get();

            return Ok(missiles);
        }

        // POST api/missiles
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] Missile missile)
        {
            var result = missileService.Insert(missile);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/missiles
        [HttpDelete]
        [AllowAnonymous]
        public IActionResult Delete([FromBody] Missile missile)
        {
            var result = missileService.Delete(missile.ServiceIdentityNumber);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}