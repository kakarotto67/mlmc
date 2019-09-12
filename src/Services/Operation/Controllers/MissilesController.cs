using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Operation.Models;
using Operation.MongoDb;

namespace Operation.Controllers
{
    [EnableCors("Operation.Missiles")]
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

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}