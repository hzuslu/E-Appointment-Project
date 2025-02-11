using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eAppointmentServer.Infrastructure.Services
{
    internal class JwtProvider(IConfiguration configuration) : IJwtProvider
    {
        public string GenerateToken(AppUser appUser)
        {
            List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, appUser.FullName ?? string.Empty),
                    new Claim("UserName", appUser.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, appUser.Email ?? string.Empty)
                };

            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:SecretKey").Value!));

            SigningCredentials credentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken securityToken = new(
                issuer: configuration.GetSection("JwtSettings:Issuer").Value,
                audience: configuration.GetSection("JwtSettings:Audience").Value,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            JwtSecurityTokenHandler tokenHandler = new();
            string token = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}