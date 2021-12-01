using Mafrig.Avaliacao.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Core.Interfaces
{
    public interface IPedidos
    {
        Task<IEnumerable<PedidoDTO>> ObterTodos();
        Task<PedidoCompletoDTO> ObterPorId(int id);
        Task Incluir(Pedido pedido);
        Task<IEnumerable<PedidoItemDTO>> ObterItens(int id);
        Task<IEnumerable<PedidoDTO>> Pesquisar(int? pedidoId, int? pecuaristaId, DateTime? dataEntregaDe, DateTime? dataEntregaAte);
    }
}
