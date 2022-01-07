using ECommerceApp.Domain.Interfaces;

namespace ECommerceApp.Domain.Entities
{
    public class EntidadeBase : IEntidade
    {
        public long Id { get; private set; }
    }
}
