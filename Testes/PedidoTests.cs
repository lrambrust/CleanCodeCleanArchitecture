using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Util;
using ECommerceApp.Domain.ValueObject;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ECommerceApp.Tests
{
    public class PedidoTests
    {
        private const double VALOR_DO_ITEM = 10;
        private const int QUANTIDADE_ITEM = 1;
        private DateTime _dataHoje;
        private DateTime _inicioVigenciaAntiga;
        private DateTime _inicioVigenciaFutura;
        private DateTime _fimVigenciaAntiga;
        private DateTime _fimVigenciaFutura;

        public PedidoTests()
        {
            _dataHoje = Clock.Today;
            _inicioVigenciaAntiga = new DateTime(2000, 01, 01);
            _inicioVigenciaFutura = DateTime.Now.AddDays(10);
            _fimVigenciaAntiga = new DateTime(2001, 01, 01);
            _fimVigenciaFutura = DateTime.Now.AddDays(30);
        }

        [Fact]
        public void NovoPedido_ComCpfValido_DeveCriarPedidoComStatusNovoPedido()
        {
            var pedido = CriarNovoPedido();
            pedido.Cpf.NumeroCpf.Should().Be("35969412880");
            pedido.Status.Should().Be(StatusPedido.NovoPedido);
        }

        [Fact]
        public void NovoPedido_ComCpfInvalido_NaoDeveCriarPedido()
        {
            Assert.Throws<CpfInvalidoException>(() => new Pedido(new Cpf("111.111.111-11")));
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
        public void AdicionarCupomDeDescontoVigenteAoPedido_AplicarDesconto_DeveAplicarODesconto()
        {
            var pedido = CriarPedidoComItens();
            pedido.AdicionarCupomDeDesconto(CupomDescontoVigenteNaDataDoPedido());
            pedido.ValorTotal.Should().Be(25);
        }

        [Fact]
        public void AdicionarCupomDeExpiradoDescontoAoPedido_AplicarDesconto_NaoDeveAplicarODesconto()
        {
            var pedido = CriarPedidoComItens();
            pedido.AdicionarCupomDeDesconto(CupomDescontoExpiradoNaDataDoPedido());
            pedido.ValorTotal.Should().Be(30);
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

        private CupomDesconto CupomDescontoVigenteNaDataDoPedido()
        {
            return new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
        }

        private CupomDesconto CupomDescontoExpiradoNaDataDoPedido()
        {
            var cupom = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
            cupom.AlterarFimVigencia(_fimVigenciaAntiga);
            return cupom;
        }
    }
}
