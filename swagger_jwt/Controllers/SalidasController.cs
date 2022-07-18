using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swagger_jwt.Data;
using swagger_jwt.Models;

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly DataDbContext _context;

        public SalidasController(DataDbContext context)
        {
            _context = context;
        }

        // GET: api/Salidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salidas>>> GetSalidas()
        {
            return await _context.Salidas.ToListAsync();
        }

        // GET: api/Salidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Salidas>> GetSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);

            if (salidas == null)
            {
                return NotFound();
            }

            return salidas;
        }

        // PUT: api/Salidas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalidas(int id, Salidas salidas)
        {
            if (id != salidas.EntradaId)
            {
                return BadRequest();
            }

            _context.Entry(salidas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Salidas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Salidas>> PostSalidas(Salidas salidas)
        {
            _context.Salidas.Add(salidas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalidas", new { id = salidas.EntradaId }, salidas);
        }

        // DELETE: api/Salidas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Salidas>> DeleteSalidas(int id)
        {
            var salidas = await _context.Salidas.FindAsync(id);
            if (salidas == null)
            {
                return NotFound();
            }

            _context.Salidas.Remove(salidas);
            await _context.SaveChangesAsync();

            return salidas;
        }

        private bool SalidasExists(int id)
        {
            return _context.Salidas.Any(e => e.EntradaId == id);
        }
    }
}
