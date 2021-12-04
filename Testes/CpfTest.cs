using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.ValueObject;
using FluentAssertions;
using Xunit;

namespace ECommerceApp.Tests
{
    public class CpfTest
    {
        private const string MENSAGEM_CPFINVALIDOEXCEPTION = "CPF Inválido";

        [Fact]
        public void NovoCpf_CpfValido_DeveRetornarCpf()
        {
            var cpf = new Cpf("35969412880");
            cpf.NumeroCpf.Should().Be("35969412880");
        }

        [Fact]
        public void NovoCpf_CpfValidoComCaracteres_DeveRetornarCpf()
        {
            var cpf = new Cpf("359.694.128-80");
            cpf.NumeroCpf.Should().Be("35969412880");
        }

        [Theory]
        [InlineData("35969412888")]
        [InlineData("35969412880105")]
        [InlineData("359.694.128-88")]
        [InlineData("111.111.111-11")]
        [InlineData(null)]
        [InlineData("")]
        public void NovoCpf_CpfInvalido_DeveRetornarErro(string cpf)
        {
            var mensagemCpfInvalidoException = Assert.Throws<CpfInvalidoException>(() => new Cpf(cpf));
            mensagemCpfInvalidoException.Message.Should().Be(MENSAGEM_CPFINVALIDOEXCEPTION);
        }
    }
}
