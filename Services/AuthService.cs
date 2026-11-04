using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaySharp.Data;
using StaySharp.DTOS.Auth;
using StaySharp.Models;

namespace StaySharp.Services
{
    public class AuthService : IAuthService
    {
        private readonly StaySharpContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(StaySharpContext context, IPasswordHasher<Usuario> passwordHasher, ITokenService tokenService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDTO?> Autenticar(LoginDTO dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null || !usuario.Ativo)
                return null;

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.SenhaHash, dto.Senha);
            if (resultado == PasswordVerificationResult.Failed)
                return null;

            return _tokenService.GerarToken(usuario);
        }
    }
}
