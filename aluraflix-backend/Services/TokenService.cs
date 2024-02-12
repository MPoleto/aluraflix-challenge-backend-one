using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using aluraflix_backend.Models;
using aluraflix_backend.Services.IServices;
using Microsoft.IdentityModel.Tokens;

namespace aluraflix_backend.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim("email", usuario.Email)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration["SymmetricSecurityKey"]));

            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}