using StaySharp.DTOS;

namespace StaySharp.Services
{
    public interface IPedidoService
    {
        Task<List<PedidoResponseDTO>> ListarAsync(PedidoFiltroDTO filtro);
        Task<PedidoResponseDTO?> BuscarPorIdAsync(int id);
        Task<PedidoResponseDTO> CadastrarAsync(PedidoCreateDTO dto);
        Task<PedidoResponseDTO?> AtualizarAsync(int id, PedidoUpdateDTO dto);
        Task<bool> RemoverAsync(int id);
    }
}
