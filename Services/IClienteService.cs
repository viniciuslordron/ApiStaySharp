using StaySharp.DTOS;

namespace StaySharp.Services
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDTO>> ListarAsync(ClienteFiltroDTO filtro);
        Task<ClienteResponseDTO?> BuscarPorIdAsync(int id);
        Task<ClienteResponseDTO> CadastrarAsync(ClienteDTO dto);
        Task<ClienteResponseDTO?> AtualizarAsync(int id, ClienteDTO dto);
        Task<bool> RemoverAsync(int id);
    }
}
