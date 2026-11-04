using Microsoft.EntityFrameworkCore;
using StaySharp.Data;
using StaySharp.DTOS;
using StaySharp.Models;

namespace StaySharp.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly StaySharpContext _context;

        public PedidoService(StaySharpContext context) => _context = context;

        private static PedidoResponseDTO ParaResposta(Pedido p) => new()
        {
            Id = p.Id,
            ClienteId = p.ClienteId,
            Status = p.Status,
            Itens = p.Itens.Select(i => new ItemPedidoResponseDTO
            {
                Id = i.Id,
                ProdutoId = i.ProdutoId,
                Quantidade = i.Quantidade
            }).ToList()
        };

        public async Task<List<PedidoResponseDTO>> ListarAsync(PedidoFiltroDTO filtro)
        {
            var query = _context.Pedidos.Include(p => p.Itens).AsQueryable();

            if (filtro.ClienteId.HasValue)
                query = query.Where(p => p.ClienteId == filtro.ClienteId.Value);

            if (!string.IsNullOrWhiteSpace(filtro.Status))
                query = query.Where(p => p.Status == filtro.Status);

            var pedidos = await query.ToListAsync();
            return pedidos.Select(ParaResposta).ToList();
        }

        public async Task<PedidoResponseDTO?> BuscarPorIdAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
            return pedido == null ? null : ParaResposta(pedido);
        }

        public async Task<PedidoResponseDTO> CadastrarAsync(PedidoCreateDTO dto)
        {
            if (!await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId))
                throw new InvalidOperationException("Cliente não encontrado.");

            if (dto.Itens == null || dto.Itens.Count == 0)
                throw new InvalidOperationException("O pedido deve ter ao menos um item.");

            var pedido = new Pedido { ClienteId = dto.ClienteId, Status = "Pendente" };

            foreach (var item in dto.Itens)
            {
                if (item.Quantidade <= 0)
                    throw new InvalidOperationException("A quantidade deve ser maior que zero.");

                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto == null)
                    throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");
                if (produto.Estoque < item.Quantidade)
                    throw new InvalidOperationException($"Estoque insuficiente para o produto {produto.Descricao} (disponível: {produto.Estoque}).");

                produto.Estoque -= item.Quantidade;
                pedido.Itens.Add(new ItemPedido { ProdutoId = item.ProdutoId, Quantidade = item.Quantidade });
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return ParaResposta(pedido);
        }

        public async Task<PedidoResponseDTO?> AtualizarAsync(int id, PedidoUpdateDTO dto)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (pedido == null)
                return null;

            if (!await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId))
                throw new InvalidOperationException("Cliente não encontrado.");

            pedido.Status = dto.Status;
            pedido.ClienteId = dto.ClienteId;

            await _context.SaveChangesAsync();

            return ParaResposta(pedido);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
