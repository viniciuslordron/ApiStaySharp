namespace StaySharp.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Instagram { get; set; }
        public string? WhatsApp { get; set; }

        public List<Pedido> Pedidos { get; set; } = new();
    }
}
