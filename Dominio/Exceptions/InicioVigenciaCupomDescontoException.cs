using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Exceptions
{
    public class InicioVigenciaCupomDescontoException : Exception
    {
        private const string MENSAGEM = "Início Vigência cupom não pode ser maior que o Fim Vigência";

        public InicioVigenciaCupomDescontoException() : base(MENSAGEM)
        {
        }
    }
}
