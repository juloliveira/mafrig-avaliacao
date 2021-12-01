using Mafrig.Avaliacao.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PropertyChanged;
using System.Linq;

namespace Mafrig.Avaliacao
{
    [AddINotifyPropertyChangedInterface]
    public partial class PedidoWindow : Window
    {
        private static readonly Regex _regexInputNumber = new Regex("[^0-9]+");

        public PedidoWindow()
        {
            Pedido = new NovoPedidoView();

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        public NovoPedidoView Pedido { get; set; }
        public ObservableCollection<EntidadeView> Pecuaristas { get; set; }
        public ObservableCollection<AnimalView> Animais { get; set; }

        public AnimalView Animal { get; set; }
        public int Quantidade { get; set; }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("http://localhost:13861");
            var pecuaristasRequest = new RestRequest("/api/pecuaristas");
            var animaisRequest = new RestRequest("/api/animais");
            var pecuaristas = await client.GetAsync<IEnumerable<EntidadeView>>(pecuaristasRequest);
            var animais = await client.GetAsync<IEnumerable<AnimalView>>(animaisRequest);

            Animais = new ObservableCollection<AnimalView>(animais);
            Pecuaristas = new ObservableCollection<EntidadeView>(pecuaristas);
        }

        private void OnlyNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regexInputNumber.IsMatch(e.Text);
        }

        private void CancelPast(object sender, DataObjectPastingEventArgs e)
        {
            e.CancelCommand();
        }

        private async void EfetuarPedido_Click(object sender, RoutedEventArgs e)
        {
            if (Pedido.Valido())
            {
                try
                {
                    var client = new RestClient("http://localhost:13861");
                    var pedidoRequest = new RestRequest("/api/pedido");

                    var pedidoBody = new PedidoRequest(Pedido.PecuaristaId.Value, Pedido.DataEntrega.Value);
                    pedidoBody.Itens = Pedido.Itens.Select(x => new PedidoItensPost(x.Animal.Id, x.Quantidade));

                    pedidoRequest.AddJsonBody(pedidoBody);

                    var result = await client.PostAsync<ResultadoApi>(pedidoRequest);

                    if (result.Codigo == 200)
                    {
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Não foi possivel processar o pedido.", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
        }

        public class ResultadoApi
        {
            public int Codigo { get; set; }
            public string Mensagem { get; set; }
        }

        public class PedidoRequest
        {
            public PedidoRequest(int pecuaristaId, DateTime dataEntrega)
            {
                PecuaristaId = pecuaristaId;
                DataEntrega = dataEntrega;
            }

            public int PecuaristaId { get; set; }
            public DateTime DataEntrega { get; set; }
            public IEnumerable<PedidoItensPost> Itens { get; set; }
        }

        public class PedidoItensPost
        {
            public PedidoItensPost(int animalId, int quantidade)
            {
                AnimalId = animalId;
                Quantidade = quantidade;
            }

            public int AnimalId { get; set; }
            public int Quantidade { get; set; }
        }

        private void AdicionarItem_Click(object sender, RoutedEventArgs e)
        {
            if (Animal == null)
            {
                MessageBox.Show("Selecione um animal para incluir no pedido", "Mafrig", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Quantidade <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que 0", "Magrif", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Pedido.AdicionarItem(Animal, Quantidade);

            Animal = null;
            Quantidade = 0;
        }

        private void RemoverItem_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var item = (PedidoItemView)button.DataContext;

            Pedido.RemoverItem(item);
        }
    }
}
