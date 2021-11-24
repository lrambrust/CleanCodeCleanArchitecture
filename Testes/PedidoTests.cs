using ECommerceApp.Enum;
using ECommerceApp.Model;
using ECommerceApp.ValueObject;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace Testes
{
    public class PedidoTests
    {
        private const double VALOR_DO_ITEM = 10;
        private const int QUANTIDADE_ITEM = 1;

        [Fact]
        public void NovoPedido_ComCpfInvalido_DeveCriarPedidoComStatusRejeitado()
        {
            var pedido = CriarPedidoRejeitado();
            pedido.Status.Should().Be(StatusPedido.Rejeitado);
        }

        [Fact]
        public void NovoPedido_ComCpfValido_DeveCriarPedidoComStatusNovoPedido()
        {
            var pedido = CriarNovoPedido();
            pedido.Status.Should().Be(StatusPedido.NovoPedido);
        }

        [Fact]
        public void AdicionarItemAoPedido_AdicionarItens_DeveAdicionarOsItens()
        {
            var pedido = CriarNovoPedido();
            pedido.AdicionarItemAoPedido(new Item("Livro DDD", VALOR_DO_ITEM, QUANTIDADE_ITEM));
            pedido.AdicionarItemAoPedido(new Item("Livro Clean Code", VALOR_DO_ITEM, QUANTIDADE_ITEM));
            pedido.AdicionarItemAoPedido(new Item("Livro Clean Architecture", VALOR_DO_ITEM, QUANTIDADE_ITEM));
            pedido.Itens.Count().Should().Be(3);
            pedido.ValorTotal.Should().Be(30);
        }

        [Fact]
        public void AdicionarItemAoPedido_AdicionarItemPedidoStatusRejeitado_NaoDeveAdicionarItem()
        {
            var pedido = CriarPedidoRejeitado();
            pedido.AdicionarItemAoPedido(new Item("Livro DDD", VALOR_DO_ITEM, QUANTIDADE_ITEM));
            pedido.Itens.Count().Should().Be(0);
        }

        [Fact]
        public void AdicionarCupomDeDescontoAoPedido_AplicarDesconto_DeveAplicarODesconto()
        {
            var pedido = CriarPedidoComItens();
            pedido.AdicionarCupomDeDesconto(10);
            pedido.ValorTotal.Should().Be(27);
        }

        [Fact]
        public void AdicionarCupomDeDescontoAoPedido_AdicionarCupomPedidoStatusRejeitado_NaoDeveAdicionarCupom()
        {
            var pedido = CriarPedidoRejeitado();
            pedido.AdicionarCupomDeDesconto(10);
            pedido.PercentualCupomDesconto.Should().Be(0);
        }

        [Fact]
        public void RemoverItemPedido_RemoverItemPedido_DeveRemoverOItemDoPedido()
        {
            var item = new Item("Livro DDD", VALOR_DO_ITEM, QUANTIDADE_ITEM);
            var pedido = CriarNovoPedido();
            pedido.AdicionarItemAoPedido(item);
            pedido.Itens.Count().Should().Be(1);
            pedido.RemoverItemDoPedido(item);
            pedido.Itens.Count().Should().Be(0);
        }

        [Fact]
        public void ApagarItensCarrinho_DeveRemoverTodosItensDoPedido_ItensDoPedidoDeveSerZero()
        {
            var pedido = CriarPedidoComItens();
            pedido.Itens.Count().Should().Be(3);
            pedido.RemoverTodosItens();
            pedido.Itens.Count().Should().Be(0);
        }

        public Pedido CriarPedidoRejeitado()
        {
            var cpf = new Cpf("359.694.128-88");
            return new Pedido(cpf);
        }

        private Pedido CriarNovoPedido()
        {
            var cpf = new Cpf("359.694.128-80");
            return new Pedido(cpf);
        }

        private Pedido CriarPedidoComItens()
        {
            var pedido = CriarNovoPedido();
            pedido.AdicionarItemAoPedido(new Item("Livro DDD", 10, 1));
            pedido.AdicionarItemAoPedido(new Item("Livro Clean Code", 10, 1));
            pedido.AdicionarItemAoPedido(new Item("Livro Clean Architecture", 10, 1));

            return pedido;
        }
    }
}
