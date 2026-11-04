using StaySharp.DTOS.Auth;

namespace StaySharp.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDTO?> Autenticar(LoginDTO dto);
    }
}
