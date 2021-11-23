using ECommerceApp.ValueObject;
using System.Collections.Generic;

namespace ECommerceApp.Model
{
    public class Pedido
    {
        private List<Item> _itens;

        public Cpf Cpf { get; }
        public string CupomDesconto { get; private set; }
        public IReadOnlyCollection<Item> Itens => _itens;
        public Pedido(Cpf cpf)
        {
            Cpf = cpf;
            _itens = new List<Item>();
        }

        public void AdicionarCupomDeDesconto(string cupom)
        {
            CupomDesconto = cupom;
        }

        public void AdicionarItemAoPedido(Item item)
        {
            _itens.Add(item);
        }
    }
}
