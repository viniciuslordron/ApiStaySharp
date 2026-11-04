using StaySharp.DTOS.Auth;
using StaySharp.Models;

namespace StaySharp.Services
{
    public interface ITokenService
    {
        TokenResponseDTO GerarToken(Usuario usuario);
    }
}
