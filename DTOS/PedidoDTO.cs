using System.ComponentModel.DataAnnotations;

namespace StaySharp.DTOS
{
    public class ItemPedidoCreateDTO
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class PedidoCreateDTO
    {
        public int ClienteId { get; set; }
        public List<ItemPedidoCreateDTO> Itens { get; set; } = new();
    }

    public class PedidoUpdateDTO
    {
        public int ClienteId { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }

    public class ItemPedidoResponseDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<ItemPedidoResponseDTO> Itens { get; set; } = new();
    }
}
