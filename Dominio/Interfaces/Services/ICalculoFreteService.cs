using ECommerceApp.Domain.Entities;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Interfaces.Services
{
    public interface ICalculoFreteService
    {
        double CalcularFrete(IReadOnlyCollection<ProdutoPedido> produtos);
    }
}
