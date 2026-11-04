using Microsoft.EntityFrameworkCore;
using StaySharp.Data;
using StaySharp.DTOS;
using StaySharp.Models;

namespace StaySharp.Services
{
    public class ClienteService : IClienteService
    {
        private readonly StaySharpContext _context;

        public ClienteService(StaySharpContext context) => _context = context;

        private static ClienteResponseDTO ParaResposta(Cliente c) => new()
        {
            Id = c.Id,
            Nome = c.Nome,
            Instagram = c.Instagram,
            WhatsApp = c.WhatsApp
        };

        public async Task<List<ClienteResponseDTO>> ListarAsync(ClienteFiltroDTO filtro)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                query = query.Where(c => c.Nome.Contains(filtro.Nome));

            var clientes = await query.ToListAsync();
            return clientes.Select(ParaResposta).ToList();
        }

        public async Task<ClienteResponseDTO?> BuscarPorIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente == null ? null : ParaResposta(cliente);
        }

        public async Task<ClienteResponseDTO> CadastrarAsync(ClienteDTO dto)
        {
            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Instagram = dto.Instagram,
                WhatsApp = dto.WhatsApp
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return ParaResposta(cliente);
        }

        public async Task<ClienteResponseDTO?> AtualizarAsync(int id, ClienteDTO dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return null;

            cliente.Nome = dto.Nome;
            cliente.Instagram = dto.Instagram;
            cliente.WhatsApp = dto.WhatsApp;

            await _context.SaveChangesAsync();

            return ParaResposta(cliente);
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
