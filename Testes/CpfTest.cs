using ECommerceApp.ValueObject;
using System;
using Xunit;

namespace Testes
{
    public class CpfTest
    {
        [Fact]
        public void NovoCpf_CpfValido_DeveRetornarErro()
        {
            var cpf = new Cpf("35969412880");

            Assert.True(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfValidoComCaracteres_DeveRetornarErro()
        {
            var cpf = new Cpf("359.694.128-80");

            Assert.True(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfInvalido_DeveRetornarErro()
        {
            var cpf = new Cpf("35969412888");

            Assert.False(cpf.CpfValido);
        }
        
        [Fact]
        public void NovoCpf_CpfTamanhoInvalido_DeveRetornarFalso()
        {
            var cpf = new Cpf("35969412880105");

            Assert.False(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfInvalidoComCaracteres_DeveRetornarFalso()
        {
            var cpf = new Cpf("359.694.128-88");

            Assert.False(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfNull_DeveRetornarFalso()
        {
            var cpf = new Cpf(null);

            Assert.False(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfVazio_DeveRetornarFalso()
        {
            var cpf = new Cpf("");

            Assert.False(cpf.CpfValido);
        }

        [Fact]
        public void NovoCpf_CpfStringEmpty_DeveRetornarFalso()
        {
            var cpf = new Cpf(string.Empty);

            Assert.False(cpf.CpfValido);
        }
    }
}
