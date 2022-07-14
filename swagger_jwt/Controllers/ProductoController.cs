using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using swagger_jwt.Data;
using swagger_jwt.Models;
using System.Linq;

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DataDbContext _DbContext;

        public ProductoController(DataDbContext dataDbContext)
        {
            _DbContext = dataDbContext;
        }

        [HttpGet("getAll")]
        public IActionResult GetProd()
        {
            var productos = _DbContext.producto.ToList();

            return Ok(productos);
        }

        [HttpPost("InsertProd")]
        public IActionResult InsertProducto([FromForm] Producto productoRequest)
        {
            try
            {
                _DbContext.producto.Add(productoRequest);
                _DbContext.SaveChanges();
                return Ok(productoRequest);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Error:=> " + ex);
            }
        }
    }
}
