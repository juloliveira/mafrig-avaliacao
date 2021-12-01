using Mafrig.Avaliacao.Core.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PropertyChanged;
using RestSharp;
using Mafrig.Avaliacao.Model;
using static Mafrig.Avaliacao.PedidoWindow;

namespace Mafrig.Avaliacao
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Pecuaristas = new ObservableCollection<EntidadeView>();
            Pedidos = new ObservableCollection<PedidoView>();

            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        public int? PedidoId { get; set; }
        public EntidadeView Pecuarista { get; set; }
        public DateTime? DataEntregaDe { get; set; }
        public DateTime? DataEntregaAte { get; set; }

        public ObservableCollection<EntidadeView> Pecuaristas { get; set; }
        public ObservableCollection<PedidoView> Pedidos { get; set; }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("http://localhost:13861");
            var pecuaristasRequest = new RestRequest("/api/pecuaristas");
            var pecuaristas = await client.GetAsync<IEnumerable<EntidadeView>>(pecuaristasRequest);

            Pecuaristas = new ObservableCollection<EntidadeView>(pecuaristas);
            await AtualizaPedidos();
        }

        private async Task AtualizaPedidos()
        {
            var client = new RestClient("http://localhost:13861");
            var pedidosRequest = new RestRequest("/api/pedido");
            var pedidos = await client.GetAsync<IEnumerable<PedidoView>>(pedidosRequest);

            Pedidos = new ObservableCollection<PedidoView>(pedidos);
        }

        private async void NovoPedido_Click(object sender, RoutedEventArgs e)
        {
            new PedidoWindow().ShowDialog();
            await AtualizaPedidos();
        }

        private async void Pesquisar_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("http://localhost:13861");
            var pesquisarRequest = new RestRequest("/api/pedido/pesquisar");
            pesquisarRequest.AddQueryParameter("PedidoId", Convert.ToString(PedidoId));
            pesquisarRequest.AddQueryParameter("PecuaristaId", Pecuarista != null ? Convert.ToString(Pecuarista.Id) : null);
            pesquisarRequest.AddQueryParameter("DataEntregaDe", DataEntregaDe.HasValue ? DataEntregaDe.Value.ToString("yyyy-MM-dd") : null);
            pesquisarRequest.AddQueryParameter("DataEntregaAte", DataEntregaAte.HasValue ? DataEntregaAte.Value.ToString("yyyy-MM-dd") : null);

            var pedidos = await client.GetAsync<IEnumerable<PedidoView>>(pesquisarRequest);

            Pedidos = new ObservableCollection<PedidoView>(pedidos);
        }

        private void LimparPesquisa_Click(object sender, RoutedEventArgs e)
        {
            PedidoId = null;
            Pecuarista = null;
            DataEntregaDe = null;
            DataEntregaAte = null;
        }
    }
}
