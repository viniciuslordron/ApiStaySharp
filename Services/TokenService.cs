using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StaySharp.DTOS.Auth;
using StaySharp.Models;

namespace StaySharp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration) => _configuration = configuration;

        public TokenResponseDTO GerarToken(Usuario usuario)
        {
            var key = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Configuração Jwt:Key não encontrada.");
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var minutos = _configuration.GetValue<int?>("Jwt:ExpirationMinutes")
                ?? throw new InvalidOperationException("Configuração Jwt:ExpirationMinutes não encontrada.");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credenciais = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var expiracao = DateTime.UtcNow.AddMinutes(minutos);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiracao,
                signingCredentials: credenciais);

            return new TokenResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracao = expiracao,
                Nome = usuario.Nome,
                Perfil = usuario.Perfil
            };
        }
    }
}
