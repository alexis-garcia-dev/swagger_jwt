
using swagger_jwt.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace swagger_jwt.Services
{
    public interface IJwtService
    {
        string GenerarToken(JwtConfig configToken, IEnumerable<Claim> claims, DateTime expiration);

    }
}
