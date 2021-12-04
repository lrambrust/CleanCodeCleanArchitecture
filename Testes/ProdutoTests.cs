using ECommerceApp.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ECommerceApp.Tests
{
    public class ProdutoTests
    {
        private const double ALTURA = 20;
        private const double LARGURA = 15;
        private const double PROFUNDIDADE = 10;
        private const double PESO = 1;

        [Fact]
        public void NovoProduto_InserirNovoProduto_ProdutoDeveExistir()
        {
            var produto = CriarProduto("Livro DDD", 10);
            produto.Should().NotBeNull();
        }

        [Fact]
        public void NovoProduto_InformarDimensoesDoProduto_DeveRetornarVolumeDoProduto()
        {
            var produto = CriarProduto("Livro DDD", 10);
            produto.InformarDimensoesDoProtudo(ALTURA, LARGURA, PROFUNDIDADE);
            produto.VolumeDoProduto().Should().Be(0.003);
        }

        [Fact]
        public void NovoProduto_InformarDimensoesEPesoDoProduto_DeveRetornarDensidadeDoProduto()
        {
            var produto = CriarProduto("Livro DDD", 10);
            produto.InformarDimensoesDoProtudo(ALTURA, LARGURA, PROFUNDIDADE);
            produto.InformarPesoDoProduto(PESO);
            produto.DensidadeDoProduto().Should().Be(333.33);
        }

        [Fact]
        public void NovoProduto_AlterarValorDoProduto_DeveAtualizarOValorDoProduto()
        {
            var produto = CriarProduto("Livro DDD", 10);
            produto.Valor.Should().Be(10);
            produto.AlterarValorDoProduto(15);
            produto.Valor.Should().Be(15);
        }

        private Produto CriarProduto(string descrição, double valor)
        {
            return new Produto(descrição, valor);
        }
    }
}
