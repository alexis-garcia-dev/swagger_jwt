using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swagger_jwt.Data;
using swagger_jwt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<List<Bodega>>> GetAll()
        {
            var roles = await _DbContext.bodega.Where(c => c.Estado == true).ToListAsync();

            var response = new
            {
                Status = "OK",
                Message = "Lista de Bodegas",
                Data = roles
            };

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Bodega>> Put(Bodega bodega, int id)
        {
            var nombre = _DbContext.bodega.Where(c => c.Nombre == bodega.Nombre).FirstOrDefault();
            if (nombre != null)
            {
                return BadRequest("Bodega ya Existe");
            }

            var rolActualizar = await _DbContext.bodega.Where(c => c.BodegaId == id).FirstOrDefaultAsync();
            if (rolActualizar == null)
            {
                return NotFound("Bodega no Encontrada");
            }
            rolActualizar.Nombre = bodega.Nombre.ToUpper();
            _DbContext.Entry(rolActualizar).State = EntityState.Modified;
            await _DbContext.SaveChangesAsync();
            var response = new
            {
                Status = "OK",
                Message = "Bodega Actualizada",
                Data = bodega
            };

            return Ok(response);
        }
    }
}
