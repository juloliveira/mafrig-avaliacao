using Mafrig.Avaliacao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mafrig.Avaliacao.Mock
{
    public class PedidoWindowMock
    {
        public PedidoWindowMock()
        {
            Pedido = new NovoPedidoView { DataEntrega = DateTime.Now };
            Pedido.Itens.Add(new PedidoItemView 
            { 
                Animal = new AnimalView
                {
                    Nome = "Gado",
                    Preco = 189.77
                },
                Quantidade = 123
            });

            Pedido.Itens.Add(new PedidoItemView
            {
                Animal = new AnimalView
                {
                    Nome = "Zebu",
                    Preco = 289.99
                },
                Quantidade = 127
            });

            Pedido.Itens.Add(new PedidoItemView
            {
                Animal = new AnimalView
                {
                    Nome = "Bufalo",
                    Preco = 89.33
                },
                Quantidade = 523
            });

            Pedido.Itens.Add(new PedidoItemView
            {
                Animal = new AnimalView { Nome = "Nelori", Preco = 892.73 },
                Quantidade = 823
            });

        }

        public NovoPedidoView Pedido { get; set; }
        public List<EntidadeView> Pecuaristas { get; set; }
    }
}
