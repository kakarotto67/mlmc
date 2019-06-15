using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Operation.Database;
using Operation.Models;

namespace Operation.Controllers
{
    [EnableCors("Operation.Missiles")]
    [Route("api/[controller]")]
    [ApiController]
    public class MissilesController : ControllerBase
    {
        private readonly DoDDataWarehouseContext dbContext;

        public MissilesController(DoDDataWarehouseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET api/missiles
        [HttpGet]
        public ActionResult<IEnumerable<Missile>> GetMissiles()
        {
            return Ok(dbContext.Missiles);
        }

        // // GET api/values/5
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // // POST api/values
        // [HttpPost]
        // public void Post([FromBody] string value)
        // {
        // }

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
