using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using swagger_jwt.Data;
using swagger_jwt.DTO;
using swagger_jwt.Models;
using swagger_jwt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace swagger_jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataDbContext _db;
        private readonly IJwtService _jwtService;
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<LoginController> _logger;
        private DataDbContext myDbContext;
        private object context;
        private IConfiguration configuration1;
        private object configuration2;
        private IJwtService jwtService1;
        private object jwtService2;
        private bool v;

        public LoginController(DataDbContext context, IConfiguration configuration, IJwtService jwtService, ILogger<LoginController> logger)
        {
            _db = context;
            _jwtConfig = configuration.GetSection("Jwt").Get<JwtConfig>();
            _jwtService = jwtService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LoginDTO userModel)
        {

            var userDto = await _db.usuario.FirstOrDefaultAsync(u => u.Email == userModel.Email);
            if (userDto == null)
            {
                return NotFound();
            }

            // validando
            if (!BCrypt.Net.BCrypt.Verify(userModel.Password, userDto.Password))
            {
                _logger.LogWarning("Credenciales no válidas.", userModel);
                return Unauthorized(new { message = "Credenciales no válidas." });
            }

            try
            {
                var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, userDto.Email) };
                var tokenExp = DateTime.Now.AddMinutes(30);
                var token = _jwtService.GenerarToken(_jwtConfig, authClaims, tokenExp);

                return Ok(new
                {
                    access_token = token,
                    expires_in = tokenExp
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { message = $"No se pudo iniciar sesión. Error: {ex.Message}" });
            }
        }



    }
}
