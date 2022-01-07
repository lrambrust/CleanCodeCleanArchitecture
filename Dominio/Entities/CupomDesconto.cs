using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Util;
using System;
using System.Text.RegularExpressions;

namespace ECommerceApp.Domain.Entities
{
    public class CupomDesconto
    {
        public long ID { get; private set; }
        public string CodigoCupom { get; private set; }
        public DateTime InicioVigencia { get; private set; }
        public DateTime FimVigencia { get; private set; }

        public CupomDesconto(string codigoCupom, DateTime inicioVigencia, DateTime fimVigencia)
        {
            CodigoCupom = codigoCupom;
            InicioVigencia = inicioVigencia;
            FimVigencia = ValidarFimVigencia(fimVigencia);
        }

        private DateTime ValidarFimVigencia(DateTime fimVigencia)
        {
            if (fimVigencia < Clock.Today || fimVigencia < InicioVigencia)
            {
                throw new FimVigenciaCupomDescontoException();
            }

            return fimVigencia;
        }

        public bool CupomAtivo(DateTime data)
        {
            return data >= InicioVigencia && data <= FimVigencia;
        }

        public void AlterarFimVigencia(DateTime fimVigencia)
        {
            if (fimVigencia < InicioVigencia)
            {
                throw new FimVigenciaCupomDescontoException();
            }

            FimVigencia = fimVigencia;
        }

        public void AlterarInicioVigencia(DateTime inicioVigencia)
        {
            if (inicioVigencia > FimVigencia)
            {
                throw new InicioVigenciaCupomDescontoException();
            }

            InicioVigencia = inicioVigencia;
        }

        public double ObterValorDesconto()
        {
            return double.Parse(Regex.Replace(CodigoCupom, "[\\D]", ""));
        }
    }
}
