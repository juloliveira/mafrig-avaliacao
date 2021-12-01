using Mafrig.Avaliacao.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace Mafrig.Avaliacao.Core
{
    public class Pedido : Entidade
    {
        private IList<PedidoItem> _items;
        public Pedido(Pecuarista pecuarista, DateTime dataEntrega)
        {
            Pecuarista = pecuarista;
            DataEntrega = dataEntrega;
            _items = new List<PedidoItem>();
        }

        public Pecuarista Pecuarista { get; set; }
        public DateTime DataEntrega { get; private set; }

        public IEnumerable<PedidoItem> Items => _items;

        public void AdicionarItem(Animal animal, int quantidade)
        {
            if (animal == null) throw new ModeloInvalidoException(animal);
            if (quantidade <= 0) throw new ModeloInvalidoException(animal);

            _items.Add(new PedidoItem(animal, quantidade));
        }
    }
}
