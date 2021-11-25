using System.Text.RegularExpressions;

namespace ECommerceApp.ValueObject
{
    public class Cpf
    {
        public string NumeroCpf { get; }
        public bool CpfValido { get; }

        public Cpf(string cpf)
        {
            NumeroCpf = cpf;
            CpfValido = ValidarCpf(cpf);
        }

        public bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            if (ValidarTamanhoDaString(cpf))
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string numeroCpfSemDigito;
            string digito;
            int soma;
            int resto;
            cpf = Regex.Replace(cpf, "[\\D]", "");
            numeroCpfSemDigito = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(numeroCpfSemDigito[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            numeroCpfSemDigito = numeroCpfSemDigito + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(numeroCpfSemDigito[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private bool ValidarTamanhoDaString(string cpf)
        {
            return cpf.Length < 11 || cpf.Length > 14;
        }
    }
}
