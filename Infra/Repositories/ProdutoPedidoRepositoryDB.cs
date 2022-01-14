using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces.Repositories;
using ECommerceApp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ECommerceApp.Infra.Repositories
{
    public class ProdutoPedidoRepositoryDB : IProdutoPedidoRepository
    {
        private ECommerceContext _context;

        public ProdutoPedidoRepositoryDB(ECommerceContext context)
        {
            _context = context;
        }

        public ProdutoPedido BuscarPorPedidoIdEProdutoId(int pedidoId, int produtoId)
        {
            return _context.ProdutosPedido
                            .Include(p => p.Produto)
                            .FirstOrDefault(p => p.PedidoID == pedidoId && p.ProdutoID == produtoId);
        }
    }
}
