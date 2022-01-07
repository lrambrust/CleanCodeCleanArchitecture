using ECommerceApp.Domain.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;

namespace ECommerceApp.Domain.ValueObject
{
    public class Cpf
    {
        public string Numero { get; }

        public Cpf(string cpf)
        {
            if (!ValidarCpf(cpf))
            {
                throw new CpfInvalidoException();
            }
            Numero = RetornarSomenteNumerosDoCpf(cpf);
        }

        private bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            if (ValidarTamanhoDaString(cpf))
            {
                return false;
            }

            cpf = RetornarSomenteNumerosDoCpf(cpf);
            if (CpfPossuiTodosNumerosIguais(cpf))
            {
                return false;
            }

            int[] primeiroMultiplicador = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] segundoMultiplicador = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string numeroCpfSemDigito;
            string digito;
            int resto;
            numeroCpfSemDigito = cpf.Substring(0, 9);
            resto = CalculoComMultiplicador(primeiroMultiplicador, numeroCpfSemDigito);
            digito = resto.ToString();
            numeroCpfSemDigito = numeroCpfSemDigito + digito;
            resto = CalculoComMultiplicador(segundoMultiplicador, numeroCpfSemDigito);
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        private bool ValidarTamanhoDaString(string cpf)
        {
            return cpf.Length < 11 || cpf.Length > 14;
        }

        private static string RetornarSomenteNumerosDoCpf(string cpf)
        {
            return Regex.Replace(cpf, "[\\D]", "");
        }

        private bool CpfPossuiTodosNumerosIguais(string cpf)
        {
            return cpf.All(c => c == cpf[0]);
        }

        private static int CalculoComMultiplicador(int[] multiplicador, string numeroCpfSemDigito)
        {
            int resto;
            int soma = 0;
            for (int i = 0; i < multiplicador.Length; i++)
            {
                soma += int.Parse(numeroCpfSemDigito[i].ToString()) * multiplicador[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            return resto;
        }
    }
}
