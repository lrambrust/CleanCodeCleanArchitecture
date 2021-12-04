namespace ECommerceApp.Domain.Entities
{
    public class Produto
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }

        public Produto(string descricao, double valor, int quantidade)
        {
            Descricao = descricao;
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}