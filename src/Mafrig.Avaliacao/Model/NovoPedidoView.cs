using System;
using System.Linq;
using System.Collections.ObjectModel;
using PropertyChanged;
using System.Windows;

namespace Mafrig.Avaliacao.Model
{
    [AddINotifyPropertyChangedInterface]
    public class NovoPedidoView
    {
        public NovoPedidoView()
        {
            Itens = new ObservableCollection<PedidoItemView>();
        }

        public int? PecuaristaId { get; set; }
        public DateTime? DataEntrega { get; set; }
        public ObservableCollection<PedidoItemView> Itens { get; set; }
        
        public double Total { get; private set; }

        internal void AtualizarTotal()
        {
            Total = Itens.Sum(x => x.Total);
        }

        public void AdicionarItem(AnimalView animal, int quantidade)
        {
            if (Itens.Any(x => x.Animal.Id == animal.Id))
            {
                MessageBox.Show("Este animal já foi incluido no pedido.", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Itens.Add(new PedidoItemView(this, animal, quantidade));
            AtualizarTotal();
        }

        internal void RemoverItem(PedidoItemView item)
        {
            Itens.Remove(item);
            AtualizarTotal();
        }

        internal bool Valido()
        {
            if (!DataEntrega.HasValue || DataEntrega.Value <= DateTime.Now)
            {
                MessageBox.Show("Data da entrega deve ser maior que a data de hoje", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!PecuaristaId.HasValue)
            {
                MessageBox.Show("Selecione um pecuarista", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!Itens.Any())
            {
                MessageBox.Show("Adicione items ao pedido", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
