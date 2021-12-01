using Mafrig.Avaliacao.Core.DTO;
using Mafrig.Avaliacao.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mafrig.Avaliacao.Mock
{
    public class MainWindowMock
    {
        public MainWindowMock()
        {
            Pedidos = new List<PedidoView>();
            Pedidos.Add(new PedidoView { PedidoId = 1, DataEntrega = DateTime.Now, Pecuarista = "Fulano da Silva", Total = 123.15 });
            Pedidos.Add(new PedidoView { PedidoId = 2, DataEntrega = DateTime.Now, Pecuarista = "Ciclano de SOuza", Total = 2223.15 });
            Pedidos.Add(new PedidoView { PedidoId = 3, DataEntrega = DateTime.Now, Pecuarista = "Beltrano da Cruz", Total = 2223.15 });
            Pedidos.Add(new PedidoView { PedidoId = 4, DataEntrega = DateTime.Now, Pecuarista = "Teste de Nome", Total = 2223.15 });
            Pedidos.Add(new PedidoView { PedidoId = 5, DataEntrega = DateTime.Now, Pecuarista = "Outro Nome", Total = 2223.15 });

            Pecuaristas = new List<EntidadeView>();
            Pecuaristas.Add(new EntidadeView { Nome = "Pecuarista A" });
            Pecuaristas.Add(new EntidadeView { Nome = "Pecuarista B" });
        }

        public List<EntidadeView> Pecuaristas { get; set; }
        public List<PedidoView> Pedidos { get; set; }
    }
}
