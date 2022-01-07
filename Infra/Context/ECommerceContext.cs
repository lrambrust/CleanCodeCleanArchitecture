using ECommerceApp.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ECommerceApp.Infra.Context
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext() : base("ECommerceContext")
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CupomDesconto> CupomDescontos { get; set; }
        public DbSet<ProdutoPedido> ProdutosPedido { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
