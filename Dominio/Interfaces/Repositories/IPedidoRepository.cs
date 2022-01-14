using ECommerceApp.Domain.Entities;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Pedido BuscarPorId(int id);
        List<Pedido> BuscarTodos();
    }
}
