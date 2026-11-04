using Microsoft.EntityFrameworkCore;
using StaySharp.Models;

namespace StaySharp.Data
{
    public class StaySharpContext : DbContext
    {
        public StaySharpContext(DbContextOptions<StaySharpContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(e =>
            {
                e.ToTable("Clientes");
                e.HasKey(c => c.Id);
                e.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                e.Property(c => c.Instagram).HasMaxLength(100);
                e.Property(c => c.WhatsApp).HasMaxLength(20);
            });

            modelBuilder.Entity<Produto>(e =>
            {
                e.ToTable("Produtos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Descricao).IsRequired().HasMaxLength(200);
                e.Property(p => p.Tamanho).HasMaxLength(10);
                e.Property(p => p.Cor).HasMaxLength(50);
                e.Property(p => p.Valor).HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<Pedido>(e =>
            {
                e.ToTable("Pedidos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Status).IsRequired().HasMaxLength(50);
                e.HasOne(p => p.Cliente)
                 .WithMany(c => c.Pedidos)
                 .HasForeignKey(p => p.ClienteId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ItemPedido>(e =>
            {
                e.ToTable("ItensPedido");
                e.HasKey(i => i.Id);
                e.HasOne(i => i.Pedido)
                 .WithMany(p => p.Itens)
                 .HasForeignKey(i => i.PedidoId)
                 .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(i => i.Produto)
                 .WithMany(p => p.ItensPedido)
                 .HasForeignKey(i => i.ProdutoId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(e =>
            {
                e.ToTable("Usuarios");
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.Email).IsUnique();
            });
        }
    }
}
