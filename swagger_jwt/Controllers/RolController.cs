using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using swagger_jwt.Data;
using swagger_jwt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly DataDbContext _dbContext;

        public RolController(DataDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Roles>>> GetAll()
        {
            var roles = await _dbContext.roles.Where(c => c.Estado == true).ToListAsync();

            var response = new
            {
                Status = "OK",
                Message = "Lista de Roles",
                Data = roles
            };

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetById(int id)
        {
            var rol = await _dbContext.roles.Where(c => c.RolesId == id).FirstOrDefaultAsync();

            if (rol == null)
            {
                return NotFound("Rol no encontrado");
            }
            var response = new
            {
                Status = "OK",
                Message = "Id Rol",
                Data = rol
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Roles>> Post(Roles rol)
        {
            var nombre = _dbContext.roles.Where(c => c.Nombre == rol.Nombre).FirstOrDefault();
            if (nombre != null)
            {
                return BadRequest("El nombre del Rol ya existe");
            }
            rol.Nombre = rol.Nombre.ToUpper();
            _dbContext.roles.Add(rol);
            await _dbContext.SaveChangesAsync();
            var response = new
            {
                Status = "OK",
                Message = "Rol creado",
                Data = rol
            };

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Roles>> Put(Roles rol, int id)
        {
            var nombre = _dbContext.roles.Where(c => c.Nombre == rol.Nombre).FirstOrDefault();
            if (nombre != null)
            {
                return BadRequest("El nombre del Rol ya existe");
            }

            var rolActualizar = await _dbContext.roles.Where(c => c.RolesId == id).FirstOrDefaultAsync();
            if (rolActualizar == null)
            {
                return NotFound("Rol no encontrado");
            }
            rolActualizar.Nombre = rol.Nombre.ToUpper();
            _dbContext.Entry(rolActualizar).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            var response = new
            {
                Status = "OK",
                Message = "Rol Actualizado",
                Data = rol
            };

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Roles>> Delete(int id)
        {
            var rol = await _dbContext.roles.Where(c => c.RolesId == id).FirstOrDefaultAsync();
            if (rol == null)
            {
                return NotFound("Rol no encontrado");
            }
            rol.Estado = false;
            _dbContext.Entry(rol).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            var response = new
            {
                Status = "OK",
                Message = "Rol Eliminado",
                Data = rol
            };

            return Ok(response);
        }



    }






}
