using System.ComponentModel.DataAnnotations;

namespace StaySharp.DTOS
{
    public class ClienteDTO
    {
        [Required, MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Instagram { get; set; }

        [MaxLength(20)]
        public string? WhatsApp { get; set; }
    }

    public class ClienteResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public string? WhatsApp { get; set; }
    }
}
