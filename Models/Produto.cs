namespace StaySharp.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Tamanho { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int Estoque { get; set; }

        public List<ItemPedido> ItensPedido { get; set; } = new();
    }
}
