using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaySharp.DTOS;
using StaySharp.Services;

namespace StaySharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ClienteFiltroDTO filtro)
        {
            var clientes = await _service.ListarAsync(filtro);
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var cliente = await _service.BuscarPorIdAsync(id);
            if (cliente == null)
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Cliente não encontrado."
                });
            return Ok(cliente);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ClienteDTO dto)
        {
            var cliente = await _service.CadastrarAsync(dto);
            return CreatedAtAction(nameof(ListarPorId), new { id = cliente.Id }, cliente);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ClienteDTO dto)
        {
            var cliente = await _service.AtualizarAsync(id, dto);
            if (cliente == null)
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Cliente não encontrado."
                });
            return Ok(cliente);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            if (!await _service.RemoverAsync(id))
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Cliente não encontrado."
                });
            return NoContent();
        }
    }
}
