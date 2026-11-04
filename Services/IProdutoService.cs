using StaySharp.DTOS;

namespace StaySharp.Services
{
    public interface IProdutoService
    {
        Task<List<ProdutoResponseDTO>> ListarAsync(ProdutoFiltroDTO filtro);
        Task<ProdutoResponseDTO?> BuscarPorIdAsync(int id);
        Task<ProdutoResponseDTO> CadastrarAsync(ProdutoDTO dto);
        Task<ProdutoResponseDTO?> AtualizarAsync(int id, ProdutoDTO dto);
        Task<bool> RemoverAsync(int id);
    }
}
