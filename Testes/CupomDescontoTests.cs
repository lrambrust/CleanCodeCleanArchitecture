using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Exceptions;
using ECommerceApp.Domain.Util;
using FluentAssertions;
using System;
using Xunit;

namespace ECommerceApp.Tests
{
    public class CupomDescontoTests
    {
        private const string MENSAGEM_FIMVIGENCIAEXCEPTION = "Data Fim Vigência não pode ser menor que o dia atual ou menor que o Início da Vigência";
        private const string MENSAGEM_INICIOVIGENCIAEXCEPTION = "Início Vigência cupom não pode ser maior que o Fim Vigência";
        private DateTime _dataHoje;
        private DateTime _inicioVigenciaAntiga;
        private DateTime _inicioVigenciaFutura;
        private DateTime _fimVigenciaAntiga;
        private DateTime _fimVigenciaFutura;

        public CupomDescontoTests()
        {
            _dataHoje = Clock.Today;
            _inicioVigenciaAntiga = new DateTime(2000, 01, 01);
            _inicioVigenciaFutura = DateTime.Now.AddDays(10);
            _fimVigenciaAntiga = new DateTime(2001, 01, 01);
            _fimVigenciaFutura = DateTime.Now.AddDays(30);
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComInicioEFimVigenciaAposDataAtual_DeveCadastrarCupom()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
            cupomDesconto.Should().NotBeNull();
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComInicioVigenciaAntigaEFimVigenciaFutura_DeveCadastrarCupom()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
            cupomDesconto.Should().NotBeNull();
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComInicioVigenciaHojeEFimVigenciaFutura_DeveCadastrarCupom()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _dataHoje, _fimVigenciaFutura);
            cupomDesconto.Should().NotBeNull();
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComInicioVigenciaFuturaEFimVigenciaFutura_DeveCadastrarCupom()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
            cupomDesconto.Should().NotBeNull();
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComFimVigenciaAnteriorAoDiaAtual_DeveLancarExcecao()
        {
            var exception = Assert.Throws<FimVigenciaCupomDescontoException>(() => new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaAntiga));
            exception.Message.Should().Be(MENSAGEM_FIMVIGENCIAEXCEPTION);
        }

        [Fact]
        public void CadastrarNovoCupomDesconto_ComFimVigenciaMenorQueInicioVVigencia_DeveLancarExcecao()
        {
            var exception = Assert.Throws<FimVigenciaCupomDescontoException>(() => new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _dataHoje));
            exception.Message.Should().Be(MENSAGEM_FIMVIGENCIAEXCEPTION);
        }

        [Fact]
        public void VerificarSeCupomEstaAtivoNaData_CupomEmDataVigente_DeveRetornarTrue()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
            cupomDesconto.CupomAtivo(_dataHoje).Should().BeTrue();
        }

        [Fact]
        public void VerificarSeCupomEstaAtivoNaData_CupomVigenciaAntiga_DeveRetornarFalse()
        {
            CupomDesconto cupomDesconto = CupomDescontoVencido();
            cupomDesconto.CupomAtivo(_dataHoje).Should().BeFalse();
        }

        [Fact]
        public void VerificarSeCupomEstaAtivoNaData_CupomVigenciaFutura_DeveRetornarFalse()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
            cupomDesconto.CupomAtivo(_dataHoje).Should().BeFalse();
        }

        [Fact]
        public void AlterarInicioVigenciaCupom_DataAnteriorAoFimVigencia_DeveAlterarInicioVigencia()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
            cupomDesconto.InicioVigencia.Should().Be(_inicioVigenciaFutura);
            cupomDesconto.AlterarInicioVigencia(_dataHoje);
            cupomDesconto.InicioVigencia.Should().Be(_dataHoje);
        }

        [Fact]
        public void AlterarInicioVigenciaCupom_DataPosteriorAoFimVigencia_DeveLancarExcecao()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _dataHoje);
            cupomDesconto.InicioVigencia.Should().Be(_inicioVigenciaAntiga);
            var exception = Assert.Throws<InicioVigenciaCupomDescontoException>(() => cupomDesconto.AlterarInicioVigencia(_inicioVigenciaFutura));
            exception.Message.Should().Be(MENSAGEM_INICIOVIGENCIAEXCEPTION);
        }

        [Fact]
        public void AlterarFimVigenciaCupom_DataAnteriorAoInicioVigencia_DeveLancarExcecao()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _dataHoje, _fimVigenciaFutura);
            cupomDesconto.FimVigencia.Should().Be(_fimVigenciaFutura);
            var exception = Assert.Throws<FimVigenciaCupomDescontoException>(() => cupomDesconto.AlterarFimVigencia(_fimVigenciaAntiga));
            exception.Message.Should().Be(MENSAGEM_FIMVIGENCIAEXCEPTION);
        }

        [Fact]
        public void ObterValorDesconto_CodigoCupom_DeveRetornarValorASerDescontado()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaFutura, _fimVigenciaFutura);
            cupomDesconto.ObterValorDesconto().Should().Be(5);
        }

        private CupomDesconto CupomDescontoVencido()
        {
            CupomDesconto cupomDesconto = new CupomDesconto("CUPOM5", _inicioVigenciaAntiga, _fimVigenciaFutura);
            cupomDesconto.AlterarFimVigencia(_fimVigenciaAntiga);
            return cupomDesconto;
        }
    }
}
