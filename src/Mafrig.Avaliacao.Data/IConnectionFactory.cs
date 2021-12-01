using System.Data;

namespace Mafrig.Avaliacao.Data
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
