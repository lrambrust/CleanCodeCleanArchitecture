using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Services;
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
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(new Produto("Livro DDD", VALOR_DO_ITEM), QUANTIDADE_ITEM));
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(new Produto("Livro Clean Code", VALOR_DO_ITEM), QUANTIDADE_ITEM));
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(new Produto("Livro Clean Architecture", VALOR_DO_ITEM), QUANTIDADE_ITEM));
            pedido.Produtos.Count().Should().Be(3);
            pedido.Subtotal.Should().Be(30);
        }

        [Fact]
        public void AdicionarCupomDeDescontoVigenteAoPedido_AplicarDesconto_DeveAplicarODesconto()
        {
            var pedido = CriarPedidoComProtudos();
            pedido.AdicionarCupomDeDesconto(CupomDescontoVigenteNaDataDoPedido());
            pedido.Subtotal.Should().Be(25);
        }

        [Fact]
        public void AdicionarCupomDeExpiradoDescontoAoPedido_AplicarDesconto_NaoDeveAplicarODesconto()
        {
            var pedido = CriarPedidoComProtudos();
            pedido.AdicionarCupomDeDesconto(CupomDescontoExpiradoNaDataDoPedido());
            pedido.Subtotal.Should().Be(30);
        }

        [Fact]
        public void RemoverItemPedido_RemoverItemPedido_DeveRemoverOItemDoPedido()
        {
            var produto = new ProdutoPedido(new Produto("Livro DDD", VALOR_DO_ITEM), QUANTIDADE_ITEM);
            var pedido = CriarNovoPedido();
            pedido.AdicionarProdutoAoPedido(produto);
            pedido.Produtos.Count().Should().Be(1);
            pedido.RemoverItemDoPedido(produto);
            pedido.Produtos.Count().Should().Be(0);
        }

        [Fact]
        public void ApagarItensCarrinho_DeveRemoverTodosItensDoPedido_ItensDoPedidoDeveSerZero()
        {
            var pedido = CriarPedidoComProtudos();
            pedido.Produtos.Count().Should().Be(3);
            pedido.RemoverTodosItens();
            pedido.Produtos.Count().Should().Be(0);
        }

        [Fact]
        public void PedidoComItens_CalcularFreteMaiorQue10_DeveCalcularFrete()
        {
            var pedido = CriarPedidoComProtudos();
            var valorFrete = CalculadoraFreteService.CalcularFrete(pedido.Produtos);
            pedido.ValorDoFrete(valorFrete);
            pedido.ValorFrete.Should().Be(439.99);
        }

        [Fact]
        public void PedidoComItens_CalcularFreteMenorQue10_DeveRetornarFreteMinimo()
        {
            var camera = new Produto("Camera", VALOR_DO_ITEM);
            camera.InformarDimensoesDoProtudo(20, 15, 10);
            camera.InformarPesoDoProduto(1);
            var pedido = CriarNovoPedido();
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(camera, QUANTIDADE_ITEM));
            var valorFrete = CalculadoraFreteService.CalcularFrete(pedido.Produtos);
            pedido.ValorDoFrete(valorFrete);
            pedido.ValorFrete.Should().Be(10);
        }

        private Pedido CriarNovoPedido()
        {
            var cpf = new Cpf("359.694.128-80");
            return new Pedido(cpf);
        }

        private Pedido CriarPedidoComProtudos()
        {
            var camera = new Produto("Camera", VALOR_DO_ITEM);
            camera.InformarDimensoesDoProtudo(20, 15, 10);
            camera.InformarPesoDoProduto(1);
            var guitarra = new Produto("Guitarra", VALOR_DO_ITEM);
            guitarra.InformarDimensoesDoProtudo(100, 30, 10);
            guitarra.InformarPesoDoProduto(3);
            var geladeira = new Produto("Geladeira", VALOR_DO_ITEM);
            geladeira.InformarDimensoesDoProtudo(200, 100, 50);
            geladeira.InformarPesoDoProduto(40);
            var pedido = CriarNovoPedido();
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(camera, QUANTIDADE_ITEM));
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(guitarra, QUANTIDADE_ITEM));
            pedido.AdicionarProdutoAoPedido(new ProdutoPedido(geladeira, QUANTIDADE_ITEM));

            return pedido;
        }

        private CupomDesconto CupomDescontoVigenteNaDataDoPedido()
        {
            return new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
        }

        private CupomDesconto CupomDescontoExpiradoNaDataDoPedido()
        {
            var cupom = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
            cupom.AlterarFimVigencia(_fimVigenciaAntiga);
            return cupom;
        }
    }
}
