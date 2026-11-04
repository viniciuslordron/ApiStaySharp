using System.ComponentModel.DataAnnotations;

namespace StaySharp.DTOS
{
    public class ProdutoDTO
    {
        [Required, MaxLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Tamanho { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Cor { get; set; } = string.Empty;

        [Range(0, 99999999.99)]
        public decimal Valor { get; set; }

        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }
    }

    public class ProdutoResponseDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Tamanho { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int Estoque { get; set; }
    }
}
