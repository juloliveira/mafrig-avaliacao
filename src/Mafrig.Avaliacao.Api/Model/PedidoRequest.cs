using System;
using System.Collections.Generic;

namespace Mafrig.Avaliacao.Api.Model
{
    public class PedidoRequest
    {
        public int PecuaristaId { get; set; }
        public DateTime DataEntrega { get; set; }
        public IEnumerable<PedidoItensPost> Itens { get; set; }
    }
}
