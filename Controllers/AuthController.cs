using Microsoft.AspNetCore.Mvc;
using StaySharp.DTOS.Auth;
using StaySharp.Services;

namespace StaySharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var response = await _authService.Autenticar(dto);
            if (response == null)
                return Unauthorized(new { mensagem = "Email ou senha inválidos" });

            return Ok(response);
        }
    }
}
