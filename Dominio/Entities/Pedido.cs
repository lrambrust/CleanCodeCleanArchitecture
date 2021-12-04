﻿using ECommerceApp.Domain.Enum;
using ECommerceApp.Domain.Util;
using ECommerceApp.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace ECommerceApp.Domain.Entities
{
    public class Pedido
    {
        private List<Produto> _itens;

        public Cpf Cpf { get; }
        public CupomDesconto CupomDesconto { get; private set; }
        public IReadOnlyCollection<Produto> Itens => _itens;
        public StatusPedido Status { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime DataPedido { get; }

        public Pedido(Cpf cpf)
        {
            Cpf = cpf;
            _itens = new List<Produto>();
            Status = StatusPedido.NovoPedido;
            DataPedido = Clock.Now;
        }

        public void AdicionarItemAoPedido(Produto item)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;

            _itens.Add(item);
            ValorTotal += item.Valor;
        }

        public void AdicionarCupomDeDesconto(CupomDesconto cupom)
        {
            if (Status.Equals(StatusPedido.Rejeitado)) return;

            CupomDesconto = cupom;
            AplicarDescontoCupom();
        }

        public void RemoverItemDoPedido(Produto item)
        {
            _itens.Remove(item);
        }

        public void RemoverTodosItens()
        {
            _itens.Clear();
        }

        private void AplicarDescontoCupom()
        {
            if (!CupomDesconto.CupomAtivo(DataPedido))
            {
                return;
            }
            ValorTotal -= CupomDesconto.ObterValorDesconto();
        }
    }
}
