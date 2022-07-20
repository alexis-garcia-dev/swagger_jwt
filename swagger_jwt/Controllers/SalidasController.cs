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
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Salidas>>> Getsalidas()
        {
            return await _context.salida.ToListAsync();
        }

        // GET: api/Salidas/5
        [HttpGet("GetById")]
        public async Task<ActionResult<Salidas>> GetSalidas(int id)
        {
            var salidas = await _context.salida.FindAsync(id);

            if (salidas == null)
            {
                return NotFound();
            }

            return salidas;
        }

        // PUT: api/Salidas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /**[HttpPut("Update")]
        public async Task<IActionResult> PutSalidas(int id, Salidas salida)
        {
            if (id != salida.EntradaId)
            {
                return BadRequest();
            }

            _context.Entry(salida).State = EntityState.Modified;

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
        } ***/

        // POST: api/Salidas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("Create")]
        public IActionResult PostSalidas([FromBody] Salidas salidas)
        {

            var existeProd = _context.producto.Where(t => t.ProductoId == salidas.ProductoId).FirstOrDefault();
            var existeBodega = _context.bodega.Where(t => t.BodegaId == salidas.BodegaId).FirstOrDefault();
            var existeUsuario = _context.usuario.Where(t => t.UsuarioId == salidas.UsuarioId).FirstOrDefault();
            var existeinv = _context.inventario.Where(t => t.ProductoId == salidas.ProductoId).FirstOrDefault();

            //precio del producto
            var precio = existeProd.Precio;
            //ganancia del 75%
            double ganancia = (precio * salidas.Cantidad) * 0.75;
            //iva del 13% y se recarga al consumidor
            double iva = (precio * salidas.Cantidad) * 0.13;
            
            salidas.VentaTotal = precio * salidas.Cantidad + ganancia + iva;


            //se obtiene la la cantidad de producto en inventario
            var existencia = existeinv.Cantidad;

            if (existeProd != null && existeBodega != null && existeUsuario != null && salidas.Cantidad <= existencia)
            {
                _context.salida.Add(salidas);
                _context.SaveChanges();
            }
            try
            {
                if (existeinv != null && salidas.Cantidad <= existencia)
                {
                    existeinv.Cantidad = existeinv.Cantidad - salidas.Cantidad;
                    existeinv.ProductoId = salidas.ProductoId;
                    existeinv.UsuarioId = salidas.UsuarioId;

                    _context.Update(existeinv);

                    _context.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error de validacion", e);
            }



            return CreatedAtAction("GetSalidas", new { id = salidas.SalidaId }, salidas);
        }

        // DELETE: api/Salidas/5
        [HttpDelete("Delete")]
        public async Task<ActionResult<Salidas>> DeleteSalidas(int id)
        {
            var salidas = await _context.salida.FindAsync(id);
            if (salidas == null)
            {
                return NotFound();
            }

            _context.salida.Remove(salidas);
            await _context.SaveChangesAsync();

            return salidas;
        }

        private bool SalidasExists(int id)
        {
            return _context.salida.Any(e => e.SalidaId == id);
        }
    }
}
