using ECommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Services
{
    public static class CalculadoraFreteService
    {
        private const int DISTANCIA = 1000;
        private const double VALOR_MINIMO_FRETE = 10;

        public static double CalcularFrete(IReadOnlyCollection<ProdutoPedido> produtos)
        {
            double valorFrete = 0;
            foreach (var produto in produtos)
            {
                valorFrete += (produto.Produto.VolumeDoProduto() * (produto.Produto.DensidadeDoProduto() / 100)) * produto.Quantidade;
            }

            valorFrete *= DISTANCIA;

            if (valorFrete < VALOR_MINIMO_FRETE)
            {
                return VALOR_MINIMO_FRETE;
            }

            return Math.Truncate(100 * valorFrete) / 100;
        }
    }
}
