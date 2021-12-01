using System;

namespace Mafrig.Avaliacao.Api.Model
{
    public class PesquisarRequest
    {
        public int? PedidoId { get; set; }
        public int? PecuaristaId { get; set; }
        public DateTime? DataEntregaDe { get; set; }
        public DateTime? DataEntregaAte { get; set; }
    }
}
