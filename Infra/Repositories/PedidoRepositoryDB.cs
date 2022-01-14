using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces.Repositories;
using ECommerceApp.Infra.Context;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Infra.Repositories
{
    public class PedidoRepositoryDB : IPedidoRepository
    {
        private ECommerceContext _context;

        public PedidoRepositoryDB(ECommerceContext context)
        {
            _context = context;
        }

        public Pedido BuscarPorId(int id)
        {
            return _context.Pedidos.FirstOrDefault(p => p.ID == id);
        }

        public List<Pedido> BuscarTodos()
        {
            return _context.Pedidos.ToList();
        }
    }
}
