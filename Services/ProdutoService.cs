using Microsoft.EntityFrameworkCore;
using StaySharp.Data;
using StaySharp.DTOS;
using StaySharp.Models;

namespace StaySharp.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly StaySharpContext _context;

        public ProdutoService(StaySharpContext context) => _context = context;

        private static ProdutoResponseDTO ParaResposta(Produto p) => new()
        {
            Id = p.Id,
            Descricao = p.Descricao,
            Tamanho = p.Tamanho,
            Cor = p.Cor,
            Valor = p.Valor,
            Estoque = p.Estoque
        };

        public async Task<List<ProdutoResponseDTO>> ListarAsync(ProdutoFiltroDTO filtro)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Descricao))
                query = query.Where(p => p.Descricao.Contains(filtro.Descricao));

            var produtos = await query.ToListAsync();
            return produtos.Select(ParaResposta).ToList();
        }

        public async Task<ProdutoResponseDTO?> BuscarPorIdAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto == null ? null : ParaResposta(produto);
        }

        public async Task<ProdutoResponseDTO> CadastrarAsync(ProdutoDTO dto)
        {
            var produto = new Produto
            {
                Descricao = dto.Descricao,
                Tamanho = dto.Tamanho,
                Cor = dto.Cor,
                Valor = dto.Valor,
                Estoque = dto.Estoque
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return ParaResposta(produto);
        }

        public async Task<ProdutoResponseDTO?> AtualizarAsync(int id, ProdutoDTO dto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return null;

            produto.Descricao = dto.Descricao;
            produto.Tamanho = dto.Tamanho;
            produto.Cor = dto.Cor;
            produto.Valor = dto.Valor;
            produto.Estoque = dto.Estoque;

            await _context.SaveChangesAsync();

            return ParaResposta(produto);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                return false;

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
