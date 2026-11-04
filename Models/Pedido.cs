namespace StaySharp.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Status { get; set; } = "Pendente";

        public Cliente? Cliente { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }
}
