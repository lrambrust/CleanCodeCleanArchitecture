using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces.Repositories;
using ECommerceApp.Infra.Context;
using System.Linq;

namespace ECommerceApp.Infra.Repositories
{
    public class ProdutoPedidoRepository : IProdutoPedidoRepository
    {
        private ECommerceContext _context;

        public ProdutoPedidoRepository(ECommerceContext context)
        {
            _context = context;
        }

        public ProdutoPedido BuscarPorPedidoIdEProdutoId(int pedidoId, int produtoId)
        {
            return _context.ProdutosPedido
                            .Include("Produtos")
                            .FirstOrDefault(p => p.PedidoID == pedidoId && p.ProdutoID == produtoId);
        }
    }
}
