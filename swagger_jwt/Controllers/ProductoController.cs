using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swagger_jwt.Data;
using swagger_jwt.DTO;
using swagger_jwt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DataDbContext _DbContext;
        private readonly IMapper _mapper;

        public ProductoController(DataDbContext dbContext, IMapper mapper)
        {
            _DbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAll()
        {
            var productos = await _DbContext.producto.Where(c => c.Estado == true).ToListAsync();

            var response = new
            {
                Status = "OK",
                Message = "Lista de Productos",
                Data = productos
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetById(int id)
        {
            var producto = await _DbContext.producto.Where(c => c.ProductoId == id).FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }
            var response = new
            {
                Status = "OK",
                Message = "Id Producto",
                Data = producto
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Post(ProductoDTO productoDTO)
        {
            var productoDb = _mapper.Map<Producto>(productoDTO);
            _DbContext.producto.Add(productoDb);
            await _DbContext.SaveChangesAsync();
            var producto = _mapper.Map<ProductoDTO>(productoDb);
            var response = new
            {
                Status = "OK",
                Message = "Producto creado",
                Data = producto
            };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductoDTO>> Put(int id, ProductoDTO productoDTO)
        {
            var producto = _mapper.Map<Producto>(productoDTO);
            var productoDb = await _DbContext.producto.Where(c => c.ProductoId == id).FirstOrDefaultAsync();

            if (productoDb == null)
            {
                return NotFound("Producto no encontrado");
            }
            productoDb.Nombre = productoDTO.Nombre;
            productoDb.Precio = productoDTO.Precio;
            productoDb.Estado = productoDTO.Estado;
            await _DbContext.SaveChangesAsync();
            var productos = _mapper.Map<ProductoDTO>(producto);
            var response = new
            {
                Status = "OK",
                Message = "Producto actualizado",
                Data = productos
            };

            return Ok(response);
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<ProductoDTO>> Delete(int id)
        {
            var producto = await _DbContext.producto.Where(c => c.ProductoId == id).FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound("Producto no encontrado");
            }
            producto.Estado = false;
            await _DbContext.SaveChangesAsync();
            var productos = _mapper.Map<ProductoDTO>(producto);
            var response = new
            {
                Status = "OK",
                Message = "Producto eliminado",
                Data = productos
            };

            return Ok(response);
        }








    }
}
