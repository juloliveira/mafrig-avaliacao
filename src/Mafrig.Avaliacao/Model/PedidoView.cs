using System;
using System.Collections.Generic;
using System.Text;

namespace Mafrig.Avaliacao.Model
{
    public class PedidoView
    {
        public bool Selecionado { get; set; }
        public int PedidoId { get; set; }
        public int PecuaristaId { get; set; }
        public string Pecuarista { get; set; }
        public DateTime DataEntrega { get; set; }
        public double Total { get; set; }
    }
}
