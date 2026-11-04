using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StaySharp.DTOS;
using StaySharp.Services;

namespace StaySharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ProdutoFiltroDTO filtro)
        {
            var produtos = await _service.ListarAsync(filtro);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var produto = await _service.BuscarPorIdAsync(id);
            if (produto == null)
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Produto não encontrado."
                });
            return Ok(produto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoDTO dto)
        {
            var produto = await _service.CadastrarAsync(dto);
            return CreatedAtAction(nameof(ListarPorId), new { id = produto.Id }, produto);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ProdutoDTO dto)
        {
            var produto = await _service.AtualizarAsync(id, dto);
            if (produto == null)
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Produto não encontrado."
                });
            return Ok(produto);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            if (!await _service.RemoverAsync(id))
                return NotFound(new ErroResponseDTO
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Mensagem = "Produto não encontrado."
                });
            return NoContent();
        }
    }
}
