using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Infra.Repositories
{
    public class PedidoRepositoryMemory : IPedidoRepository
    {
        private List<Pedido> Pedidos;

        public PedidoRepositoryMemory()
        {
            CriarPedidos();
        }

        public Pedido BuscarPorId(int id)
        {
            return Pedidos.FirstOrDefault(p => p.ID == id);
        }

        public List<Pedido> BuscarTodos()
        {
            return Pedidos;
        }

        private void CriarPedidos()
        {
            List<Pedido> pedidos = new List<Pedido>();
            for (int i = 0; i < 5; i++)
            {
                var pedido = new Pedido("35969412880");
                pedido.AdicionarProdutoAoPedido(new ProdutoPedido(pedido.ID, i, 1, 10));
                pedidos.Add(pedido);
            }
            Pedidos = pedidos;
        }
    }
}
