using System;

namespace ECommerceApp.Domain.Entities
{
    public class Produto
    {
        public string Descricao { get; set; }
        public double Valor { get; private set; }
        public double Altura { get; private set; }
        public double Largura { get; private set; }
        public double Profundidade { get; private set; }
        public double Peso { get; private set; }

        public Produto(string descricao, double valor)
        {
            Descricao = descricao;
            Valor = valor;
        }

        public void AlterarValorDoProduto(double novoValor)
        {
            Valor = novoValor;
        }

        public void InformarDimensoesDoProtudo(double altura, double largura, double profundidade)
        {
            Altura = Math.Round(altura / 100, 2);
            Largura = Math.Round(largura / 100, 2);
            Profundidade = Math.Round(profundidade / 100, 2);
        }

        public void InformarPesoDoProduto(double peso)
        {
            Peso = peso;
        }

        public double VolumeDoProduto()
        {
            return Altura * Largura * Profundidade;
        }

        public double DensidadeDoProduto()
        {
            return Math.Round(Peso / VolumeDoProduto(), 2);
        }
    }
}