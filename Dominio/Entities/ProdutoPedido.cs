
namespace ECommerceApp.Domain.Entities
{
    public class ProdutoPedido
    {
        public long ID { get; set; }
        public long PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public Produto Produto { get; set; }
        public double ValorProduto { get; set; }
        public int Quantidade { get; private set; }

        public ProdutoPedido(int pedidoId, int produtoId, int quantidade, double valorProduto)
        {
            PedidoID = pedidoId;
            ProdutoID = produtoId;
            Quantidade = quantidade;
            ValorProduto = valorProduto;
        }

        public void AlterarQuantidadeDoProduto(int quantidade)
        {
            Quantidade = quantidade;
        }

        public double ValorProdutoPedido()
        {
            return Quantidade * ValorProduto;
        }
    }
}
