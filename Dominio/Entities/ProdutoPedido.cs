
namespace ECommerceApp.Domain.Entities
{
    public class ProdutoPedido
    {
        public Produto Produto { get; }
        public int Quantidade { get; private set; }

        public ProdutoPedido(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public void AlterarQuantidadeDoProduto(int quantidade)
        {
            Quantidade = quantidade;
        }

        public double ValorProdutoPedido()
        {
            return Produto.Valor * Quantidade;
        }
    }
}
