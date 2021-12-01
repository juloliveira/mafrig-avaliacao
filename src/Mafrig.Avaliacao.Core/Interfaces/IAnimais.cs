using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mafrig.Avaliacao.Core.Interfaces
{
    public interface IAnimais
    {
        Task<IEnumerable<Animal>> ObterTodos();
        Task<Animal> ObterPorId(int id);
    }
}
