using System;

namespace ECommerceApp.Domain.Exceptions
{
    public class FimVigenciaCupomDescontoException : Exception
    {
        private const string MENSAGEM = "Data Fim Vigência não pode ser menor que o dia atual ou menor que o Início da Vigência";

        public FimVigenciaCupomDescontoException() : base(MENSAGEM)
        {
        }
    }
}