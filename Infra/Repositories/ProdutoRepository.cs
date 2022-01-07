using ECommerceApp.Domain.Entities;
using ECommerceApp.Infra.Context;
using System.Linq;

namespace ECommerceApp.Infra.Repositories
{
    public class ProdutoRepository
    {
        private ECommerceContext _context;

        public ProdutoRepository(ECommerceContext context)
        {
            _context = context;
        }

        public Produto BuscarProdutoPorId(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.ID == id);
        }
    }
}
