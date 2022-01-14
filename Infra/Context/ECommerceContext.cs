using ECommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Infra.Context
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CupomDesconto> CupomDescontos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Pedido>().ToTable("Pedido");
            builder.Entity<Produto>().ToTable("Produto");
            builder.Entity<CupomDesconto>().ToTable("CupomDesconto");
            builder.Entity<ProdutoPedido>().ToTable("ProdutoPedido");
        }
    }
}
