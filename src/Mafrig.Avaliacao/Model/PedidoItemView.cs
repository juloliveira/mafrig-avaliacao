using PropertyChanged;
using System;

namespace Mafrig.Avaliacao.Model
{
    [AddINotifyPropertyChangedInterface]
    public class PedidoItemView
    {
        private NovoPedidoView _pedido;
        public PedidoItemView() { }
        public PedidoItemView(NovoPedidoView pedido, AnimalView animal, int quantidade)
        {
            _pedido = pedido;
            Animal = animal;
            Quantidade = quantidade;
        }

        public AnimalView Animal { get; set; }

        private int _quantidade;
        public int Quantidade 
        {
            get => _quantidade;
            set
            {
                _quantidade = value;
                _pedido.AtualizarTotal();
            }
        }

        public double Total => Animal.Preco * Quantidade;

    }
}
