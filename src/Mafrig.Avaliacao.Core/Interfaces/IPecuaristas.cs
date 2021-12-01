using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Core.Interfaces
{
    public interface IPecuaristas
    {
        Task<IEnumerable<Pecuarista>> ObterTodos();
        Task<Pecuarista> ObterPorId(int id);
    }
}
