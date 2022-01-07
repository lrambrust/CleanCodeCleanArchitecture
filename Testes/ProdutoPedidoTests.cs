using ECommerceApp.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ECommerceApp.Tests
{
    public class ProdutoPedidoTests
    {
        [Fact]
        public void ProdutoPedido_AlterarQuantidadeProduto_DeveAlterarQuantidade()
        {
            var produto = new Produto("Livro DDD", 10);
            var produtoPedido = new ProdutoPedido(1, produto.ID, 1, 10);
            produtoPedido.Quantidade.Should().Be(1);
            produtoPedido.AlterarQuantidadeDoProduto(5);
            produtoPedido.Quantidade.Should().Be(5);
        }

        [Fact]
        public void ProdutoPedido_ObterValorTotal_DeveRetornarValorTotal()
        {
            var produto = new Produto("Livro DDD", 10);
            var produtoPedido = new ProdutoPedido(1, produto.ID, 1, 10);
            produtoPedido.ValorProdutoPedido().Should().Be(10);
        }
    }
}
