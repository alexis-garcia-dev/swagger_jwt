using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class UsuarioController : ControllerBase
    {
        private readonly DataDbContext _dbcontext;
        private readonly IMapper _mapper;

        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(DataDbContext dbcontext, IMapper mapper ,ILogger<UsuarioController> logger)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize]

        [HttpGet]

        public async Task<ActionResult<List<UsuarioDTO>>> GetAll()
        {
            var usuarios = await _dbcontext.usuario.ToListAsync();
            var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
            return usuariosDTO;
        }
        [Authorize]

        [HttpGet("{id}")]
        
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var us = await _dbcontext.usuario.Where(c => c.UsuarioId== id ).FirstOrDefaultAsync();

            if (us == null)
            {
                return NotFound("Usuario no encontrado");
            }
            var usuarioDTO = _mapper.Map<UsuarioDTO>(us);

            var response = new
            {
                Status = "OK",
                Message = "Id Usuario",
                Data = usuarioDTO

            };
            return Ok(response);

        }

        [HttpPost]
        public async Task<ActionResult<CreateUsuario>> Post(CreateUsuario usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            var confirmname = await _dbcontext.usuario.Where(c => c.Name== usuario.Name && c.LastName == usuario.LastName && c.Email == usuario.Email).FirstOrDefaultAsync();
            if (confirmname != null)
            {
                return NotFound("Usuario ya existe");
            }

                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                _dbcontext.usuario.Add(usuario);
                await _dbcontext.SaveChangesAsync();
                var usuariodtp = _mapper.Map<CreateUsuario>(usuario);
                return usuariodtp;

        }
        [Authorize]

        [HttpPut]
            public async Task<ActionResult<UsuarioDTO>> Put(int id ,UsuarioDTO usuarioDTO)
            {
                var usuario = _mapper.Map<Usuario>(usuarioDTO);
                var confirmname = await _dbcontext.usuario.Where(c => c.Name == usuario.Name && c.LastName == usuario.LastName && c.Email == usuario.Email).FirstOrDefaultAsync();
                if (confirmname != null)
                {
                    return NotFound("Usuario ya existe");
                }

                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                _dbcontext.Entry(usuario).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();
                var usuariodtp = _mapper.Map<UsuarioDTO>(usuario);
                return Ok(usuariodtp);
            }

    }
}
