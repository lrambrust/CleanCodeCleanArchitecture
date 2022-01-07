using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Domain.Interfaces.Repositories
{
    public interface IProdutoPedidoRepository
    {
        ProdutoPedido BuscarPorPedidoIdEProdutoId(int pedidoId, int produtoId);
    }
}
