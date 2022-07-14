using Microsoft.AspNetCore.Mvc;
using swagger_jwt.Data;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly DataDbContext _DbContext;

        public BodegaController(DataDbContext dataDbContext)
        {
            _DbContext = dataDbContext;

        }
        // GET: api/<BodegaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BodegaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BodegaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BodegaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BodegaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
