using System;
using System.Collections.Generic;
using System.Text;

namespace Mafrig.Avaliacao.Core.DTO
{
    public class PedidoCompletoDTO : PedidoDTO
    {
        public IEnumerable<PedidoItemDTO> Itens { get; set; }
    }

    public class PedidoItemDTO
    {
        public int AnimalId { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
    }

    public class PedidoDTO
    {
        public int PedidoId { get; set; }
        public int PecuaristaId { get; set; }
        public string Pecuarista { get; set; }
        public DateTime DataEntrega { get; set; }
        public double Total { get; set; }
    }
}
