using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.DTO
{
    public class PedidoDto
    {
        public int ID { get; set; }
        public Cpf Cpf { get; set; }
        public CupomDesconto CupomDesconto { get; set; }
        public IReadOnlyCollection<ProdutoPedido> Produtos { get; set; }
        public StatusPedido Status { get; set; }
        public double Subtotal { get; set; }
        public double ValorFrete { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataPedido { get; set; }
        public string CodigoPedido { get; set; }
    }
}
