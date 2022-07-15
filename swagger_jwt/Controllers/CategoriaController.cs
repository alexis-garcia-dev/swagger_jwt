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
    public class CategoriaController : ControllerBase
    {
        private readonly DataDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoriaController(DataDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> GetAll()
        {
            var categorias = await _dbContext.categoria.Where(c => c.Estado == true).ToListAsync();

            //var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);

            var response = new
            {
                Status = "OK",
                Message = "Lista de Categorias",
                Data = categorias
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            var categoria = await _dbContext.categoria.Where(c => c.CategoriaId == id).FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound("Categoria no encontrada");
            }
            var response = new
            {
                Status = "OK",
                Message = "Id Categoria",
                Data = categoria
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDTO);
                _dbContext.categoria.Add(categoria);
                await _dbContext.SaveChangesAsync();
                var categorias = _mapper.Map<CategoriaDTO>(categoria);

                var response = new
                {
                    Status = "OK",
                    Message = "Categoria creada",
                    Data = categorias
                };
            
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Error:=> " + ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDTO);
                var categoriaDb = await _dbContext.categoria.Where(c => c.CategoriaId == id).FirstOrDefaultAsync();
                if (categoriaDb == null)
                {
                    return NotFound("Categoria no encontrada");
                }
                categoriaDb.Nombre = categoria.Nombre;
                await _dbContext.SaveChangesAsync();
                var categorias = _mapper.Map<CategoriaDTO>(categoria);
                var response = new
                {
                    Status = "OK",
                    Message = "Categoria creada",
                    Data = categorias
                };

                return Ok(response);
                //return Ok(categorias);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Error:=> " + ex);
            }
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            try
            {
                var categoriaDb = await _dbContext.categoria.Where(c => c.CategoriaId == id).FirstOrDefaultAsync();
                if (categoriaDb == null)
                {
                    return NotFound("Categoria no encontrada");
                }
                categoriaDb.Estado = false;
                await _dbContext.SaveChangesAsync();
                return Ok(categoriaDb);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Error:=> " + ex);
            }
        }
    }
}
