using System;

namespace ECommerceApp.Domain.Exceptions
{
    public class CpfInvalidoException : Exception
    {
        private const string MENSAGEM = "CPF Inválido";

        public CpfInvalidoException() : base(MENSAGEM)
        {
        }
    }
}
