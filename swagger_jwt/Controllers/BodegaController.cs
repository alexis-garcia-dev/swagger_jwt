using AutoMapper;
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
    public class BodegaController : ControllerBase
    {
        private readonly DataDbContext _DbContext;
        private readonly IMapper _mapper;

        public BodegaController(DataDbContext dataDbContext , IMapper mapper)
        {
            _DbContext = dataDbContext;
            _mapper = mapper;


        }
        [HttpGet]
        public async Task<ActionResult<List<Bodega>>> GetAll()
        {
            var bodegas = await _DbContext.bodega.Where(c => c.Estado == true).ToListAsync();

            var response = new
            {
                Status = "OK",
                Message = "Lista de Bodegas",
                Data = bodegas
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Bodega>>> GetById(int id) {

            var bodegas = await _DbContext.bodega.Where(c => c.BodegaId == id).FirstOrDefaultAsync();
            
            if (bodegas== null)
            {
                return NotFound("Bodega no encontrada");
            }

            var response = new
            {
                Status = "OK",
                Message = "Id Bodega",
                Data = bodegas
            };

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<BodegaDTO>> Post(BodegaDTO bodegaDTO)
        {
            var bodega = _mapper.Map<Bodega>(bodegaDTO);
            _DbContext.bodega.Add(bodega);
            await _DbContext.SaveChangesAsync();
            var bodegas = _mapper.Map<BodegaDTO>(bodega);

            var response = new
            {
                Status = "OK",
                Message = "Bodega creada",
                Data = bodegas
            };

            return Ok(response);
        }

        [HttpPut]

        public async Task<ActionResult<BodegaDTO>> Put(BodegaDTO bodegaDTO,int id)
        {
            var bodega = _mapper.Map<Bodega>(bodegaDTO);
            var categoriaconf = await _DbContext.bodega.Where(c => c.Estado == true && c.BodegaId==id).FirstOrDefaultAsync();

            if (categoriaconf == null)
            {
                return NotFound("Categoria no encontrada");
            }

            categoriaconf.Nombre = bodegaDTO.Nombre.ToUpper();
            await _DbContext.SaveChangesAsync();
            var bodegas = _mapper.Map<BodegaDTO>(bodega);

            var response = new
            {
                Status = "OK",
                Message = "Bodega actualizada",
                Data = bodegas
            };

            return Ok(response);
        }







    }
}
