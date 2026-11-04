using System.ComponentModel.DataAnnotations;

namespace StaySharp.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public string Perfil { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;
    }
}
