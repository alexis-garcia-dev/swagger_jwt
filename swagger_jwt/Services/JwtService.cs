
using Microsoft.IdentityModel.Tokens;
using swagger_jwt.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace swagger_jwt.Services
{
    public class JwtService : IJwtService
    {
        public string GenerarToken(JwtConfig configToken, IEnumerable<Claim> claims, DateTime expiration)
        {
            var token = new JwtSecurityToken(
               expires: expiration,
               claims: claims,
               issuer: configToken.Issuer,
               audience: configToken.Audience,
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configToken.Key)), SecurityAlgorithms.HmacSha256)
               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
