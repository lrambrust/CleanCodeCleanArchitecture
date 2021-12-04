using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Util;
using ECommerceApp.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Domain.Entities
{
    public class Pedido
    {
        private List<ProdutoPedido> _produtos;

        public Cpf Cpf { get; }
        public CupomDesconto CupomDesconto { get; private set; }
        public IReadOnlyCollection<ProdutoPedido> Produtos => _produtos;
        public StatusPedido Status { get; private set; }
        public double Subtotal { get; private set; }
        public double ValorFrete { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime DataPedido { get; }

        public Pedido(Cpf cpf)
        {
            Cpf = cpf;
            _produtos = new List<ProdutoPedido>();
            Status = StatusPedido.NovoPedido;
            DataPedido = Clock.Now;
        }

        public void AdicionarProdutoAoPedido(ProdutoPedido produto)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;
            _produtos.Add(produto);
            CalcularSubTotalDoPedido();
        }

        public void AdicionarCupomDeDesconto(CupomDesconto cupom)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;
            CupomDesconto = cupom;
            CalcularSubTotalDoPedido();
        }

        public void RemoverItemDoPedido(ProdutoPedido produto)
        {
            _produtos.Remove(produto);
            CalcularSubTotalDoPedido();
        }

        public void RemoverTodosItens()
        {
            _produtos.Clear();
            CalcularSubTotalDoPedido();
        }

        private void CalcularSubTotalDoPedido()
        {
            Subtotal = _produtos.Sum(p => p.ValorProdutoPedido());
            if (CupomDesconto == null || !CupomDesconto.CupomAtivo(DataPedido) || Subtotal  == 0)
            {
                return;
            }
            Subtotal -= CupomDesconto.ObterValorDesconto();
            CalcularValorTotalDoPedido();
        }

        public void ValorDoFrete(double valorFrete)
        {
            ValorFrete = valorFrete;
        }

        private void CalcularValorTotalDoPedido()
        {
            ValorTotal = Subtotal + ValorFrete;
        }
    }
}
