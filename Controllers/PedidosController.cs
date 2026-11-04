using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaySharp.DTOS;
using StaySharp.Services;

namespace StaySharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] PedidoFiltroDTO filtro)
        {
            var pedidos = await _service.ListarAsync(filtro);
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var pedido = await _service.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Pedido não encontrado."
                });
            return Ok(pedido);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(PedidoCreateDTO dto)
        {
            try
            {
                var pedido = await _service.CadastrarAsync(dto);
                return CreatedAtAction(nameof(ListarPorId), new { id = pedido.Id }, pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, PedidoUpdateDTO dto)
        {
            try
            {
                var pedido = await _service.AtualizarAsync(id, dto);
                if (pedido == null)
                    return NotFound(new ErroResponseDTO
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Mensagem = "Pedido não encontrado."
                    });
                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            if (!await _service.RemoverAsync(id))
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Pedido não encontrado."
                });
            return NoContent();
        }
    }
}
