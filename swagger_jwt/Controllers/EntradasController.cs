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
            return await _context.entrada.ToListAsync();
        }

        // GET: api/Entradas/5
        [HttpGet("GetById")]
        public async Task<ActionResult<Entradas>> GetEntradas(int id)
        {
            var entradas = await _context.entrada.FindAsync(id);

            if (entradas == null)
            {
                return NotFound();
            }

            return entradas;
        }

        // PUT: api/Entradas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /**[HttpPut("Update")]
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
        }*/

        // POST: api/Entradas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost ("Create")]
        public IActionResult PostEntradas([FromBody] Entradas entradas)
        {            

            var existeProd = _context.producto.Where(t => t.ProductoId == entradas.ProductoId).FirstOrDefault();
            var existeBodega = _context.bodega.Where(t => t.BodegaId == entradas.BodegaId).FirstOrDefault();
            var existeUsuario = _context.usuario.Where(t => t.UsuarioId == entradas.UsuarioId).FirstOrDefault();
            
            //var entradaexist = _context.entrada.Where(t => t.ProductoId == entradas.ProductoId).FirstOrDefault();

            var precio = existeProd.Precio;
            entradas.EntradaTotal = precio * entradas.Cantidad;
            

            if (existeProd != null && existeBodega != null && existeUsuario != null)
            {
                _context.entrada.Add(entradas);

                _context.SaveChanges();
            }

            var existeInventario = _context.inventario.Where(t => t.ProductoId == entradas.ProductoId).FirstOrDefault();


            try
            {              
            if (existeInventario != null)
            {
                existeInventario.Cantidad = existeInventario.Cantidad + entradas.Cantidad;
                existeInventario.ProductoId = entradas.ProductoId;
                existeInventario.UsuarioId = entradas.UsuarioId;

                _context.Update(existeInventario);

                _context.SaveChanges();
            }

            if (existeInventario == null && existeProd != null)
            {
                Inventario inv = new Inventario();

                inv.Cantidad = entradas.Cantidad;
                inv.ProductoId = entradas.ProductoId;
                inv.UsuarioId = entradas.UsuarioId;

                _context.inventario.Add(inv);

                _context.SaveChanges();
            }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error de validacion", e);
            }


            return CreatedAtAction("GetEntradas", new { id = entradas.EntradaId }, entradas);

            /**** PostCantidadEntradas(entradas, existeProd);*/
        }

        // DELETE: api/Entradas/5


        




        // DELETE: api/Entradas/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<Entradas>> DeleteEntradas(int id)
        {
            var entradas = await _context.entrada.FindAsync(id);
            if (entradas == null)
            {
                return NotFound();
            }

            _context.entrada.Remove(entradas);
            await _context.SaveChangesAsync();

            return entradas;
        }

        private bool EntradasExists(int id)
        {
            return _context.entrada.Any(e => e.EntradaId == id);
        }
    }
}
