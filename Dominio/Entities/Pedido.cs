using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.ValueObject;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Entities
{
    public class Pedido
    {
        private List<Item> _itens;

        public Cpf Cpf { get; }
        public double PercentualCupomDesconto { get; private set; }
        public IReadOnlyCollection<Item> Itens => _itens;

        public StatusPedido Status { get; set; }
        public double ValorTotal { get; private set; }

        public Pedido(Cpf cpf)
        {
            Cpf = cpf;
            _itens = new List<Item>();
            Status = CriarStatusInicial(cpf.CpfValido);
        }

        public void AdicionarItemAoPedido(Item item)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;

            _itens.Add(item);
            ValorTotal += item.Valor;
        }

        public void AdicionarCupomDeDesconto(double cupom)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;

            PercentualCupomDesconto = cupom;
            AplicarPercentualDeDesconto();
        }

        public void RemoverItemDoPedido(Item item)
        {
            _itens.Remove(item);
        }

        public void RemoverTodosItens()
        {
            _itens.Clear();
        }

        private void AplicarPercentualDeDesconto()
        {
            ValorTotal -= ValorTotal * (PercentualCupomDesconto / 100);
        }

        private StatusPedido CriarStatusInicial(bool cpfValido)
        {
            return cpfValido ? StatusPedido.NovoPedido : StatusPedido.Rejeitado;
        }
    }
}
