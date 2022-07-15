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
    public class EntradasController : ControllerBase
    {
        private readonly DataDbContext _context;

        public EntradasController(DataDbContext context)
        {
            _context = context;
        }

        // GET: api/Entradas
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Entradas>>> Getentradas()
        {
            return await _context.entradas.ToListAsync();
        }

        // GET: api/Entradas/5
        [HttpGet("GetById")]
        public async Task<ActionResult<Entradas>> GetEntradas(int id)
        {
            var entradas = await _context.entradas.FindAsync(id);

            if (entradas == null)
            {
                return NotFound();
            }

            return entradas;
        }

        // PUT: api/Entradas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("Update")]
        public async Task<IActionResult> PutEntradas(int id, Entradas entradas)
        {
            if (id != entradas.EntradaId)
            {
                return BadRequest();
            }

            _context.Entry(entradas).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradasExists(id))
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

        // POST: api/Entradas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost ("Create")]
        public IActionResult PostEntradas([FromBody] Entradas entradas)
        {            

            var existeProd = _context.producto.Where(t => t.ProductoId == entradas.ProductoId).FirstOrDefault();
            var existeBodega = _context.bodega.Where(t => t.BodegaId == entradas.BodegaId).FirstOrDefault();
            var existeUsuario = _context.usuario.Where(t => t.UsuarioId == entradas.UsuarioId).FirstOrDefault();

            var precio = existeProd.Precio;

            entradas.EntradaTotal = precio * entradas.Cantidad;

            if (existeProd != null && existeBodega != null && existeUsuario != null)
            {
                _context.entradas.Add(entradas);

                _context.SaveChangesAsync();
            }          

            return CreatedAtAction("GetEntradas", new { id = entradas.EntradaId }, entradas);

            /**** PostCantidadEntradas(entradas, existeProd);*/
        }

        // DELETE: api/Entradas/5

        /**
        public IActionResult PostCantidadEntradas(Entradas entParam, Producto existeProd)
        {
            var existeInv = _context.inventario.Where(t => t.InventarioId == entParam.).FirstOrDefault();
            
             * estos campos se llenaran con los datos y luego se persistiran en cada tabla
             
            var inv = _context.inventario.Where(t => t.InventarioId == idInv).FirstOrDefault();
            
            if (inv != null)
            {
                inv.Cantidad = inv.Cantidad + entParam.Cantidad;
                _context.inventario.Update(inv);
                _context.SaveChanges();
            }

            return Ok("ok");

        }
            */






        // DELETE: api/Entradas/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<Entradas>> DeleteEntradas(int id)
        {
            var entradas = await _context.entradas.FindAsync(id);
            if (entradas == null)
            {
                return NotFound();
            }

            _context.entradas.Remove(entradas);
            await _context.SaveChangesAsync();

            return entradas;
        }

        private bool EntradasExists(int id)
        {
            return _context.entradas.Any(e => e.EntradaId == id);
        }
    }
}
